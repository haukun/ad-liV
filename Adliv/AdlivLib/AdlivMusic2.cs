using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdlivMusic2
{

    public class CHORD
    {
        //  調
        public enum KEY
        {
            x, C, D, E, F, G, A, B, _MAX,
        }

        //  調の属性
        public enum EXT
        {
            x,
            //dim ,
            //aug,
            //sus4,
            //x6,
            x7,
            //M7,
            _MAX,
        }

        KEY mKey;           //  調
        bool mKeySharp;     //  調が#かどうか
        bool mMinor;        //  マイナーコードかどうか
        EXT mExt;           //  コードの属性
        KEY mOn;            //  コードのOn部
        int mOct;           //  オクターブ

        public CHORD(KEY _key)
        {
            if(_key > KEY._MAX)
            {
                _key -= 7;
            }
            mKey = _key;
            mKeySharp = false;
            mMinor = false;
            mExt = EXT.x;
            mOn = _key;
            mOct = 0;
        }

        public CHORD(CHORD _chord)
        {
            mKey = _chord.mKey;
            mKeySharp = _chord.mKeySharp;
            mMinor = _chord.mMinor;
            mExt = _chord.mExt;
            mOn = _chord.mOn;
            mOct = _chord.mOct;
        }

        public CHORD setMajor()
        {
            mMinor = false;
            return this;
        }
        public CHORD setMinor()
        {
            mMinor = true;
            return this;
        }

        public CHORD set7th()
        {
            mExt = EXT.x7;
            return this;
        }

        public CHORD setOct(int _oct)
        {
            CHORD c = new CHORD(this);
            c.mOct = _oct;
            return c;
        }

        static PITCH[] getNull()
        {
            return new PITCH[0];
        }

        PITCH K2P(KEY key)
        {
            switch(key)
            {
                case KEY.C:
                    return PITCH.C3;
                case KEY.D:
                    return PITCH.D3;
                case KEY.E:
                    return PITCH.E3;
                case KEY.F:
                    return PITCH.F3;
                case KEY.G:
                    return PITCH.G2;
                case KEY.A:
                    return PITCH.A2;
                case KEY.B:
                    return PITCH.B2;
            }
            return PITCH.C3;
        }
        PITCH OUP(PITCH pitch)
        {
            return pitch + 12;
        }
        PITCH ODN(PITCH pitch)
        {
            return pitch - 12;
        }

        public PITCH[] getChord()
        {
            List<PITCH> pitches = new List<PITCH>();

            PITCH basePitch = K2P(mKey);

            switch (mExt)
            {
                case EXT.x:
                    pitches.Add(basePitch);
                    pitches.Add(basePitch + (mMinor ? 3 : 4));
                    pitches.Add(basePitch + 7);
                    break;
                case EXT.x7:
                    pitches.Add(basePitch);
                    pitches.Add(basePitch + (mMinor ? 3 : 4));
                    pitches.Add(basePitch + 7);
                    pitches.Add(basePitch + 10);
                    break;
            }

            for(int i = 0; i < pitches.Count; i++)
            {
                if(pitches[i] > PITCH.F3)
                {
                    pitches[i] = ODN(pitches[i]);
                }
            }

            for(int i = 0; i < pitches.Count; i++)
            {
                pitches[i] += mOct * 12;
            }

            pitches.Sort();
            return pitches.ToArray();
        }

        public PITCH[] getTonic(int index = 0)
        {
            switch(index)
            {
                case 0:
                    return getChord();
                case 1:
                    return new CHORD(mKey + 2).setMinor().set7th().getChord();
                case 2:
                    return new CHORD(mKey + 5).setMinor().set7th().getChord();
            }
            return CHORD.getNull();
        }

        public PITCH[] getDominant()
        {
            return new CHORD(mKey + 4).set7th().getChord();
        }

        public PITCH[] getSubDominant(int index = 0)
        {
            switch(index)
            {
                case 0:
                    return new CHORD(mKey + 3).getChord();
                case 1:
                    return new CHORD(mKey + 1).setMinor().set7th().getChord();
            }
            return CHORD.getNull();
        }

        public PITCH getTonicBase(int index = 0)
        {
            switch(index)
            {
                case 0:
                    return K2P(mKey) + mOct * 12;
                case 1:
                    return K2P(mKey + 2) + mOct * 12;
                case 2:
                    return K2P(mKey + 5) + mOct * 12;
            }
            return K2P(mKey) + mOct * 12;
        }

        public PITCH getSubDominantBase(int index =0)
        {
            switch (index)
            {
                case 0:
                    return K2P(mKey + 3) + mOct * 12;
                case 1:
                    return K2P(mKey + 1) + mOct * 12;
            }
            return K2P(mKey) + mOct * 12;
        }

        public PITCH getDominantBase()
        {
            return K2P(mKey + 4) + mOct * 12;
        }

    }

    public class NOTE
    {
        public LENGTH mLength;
        public PITCH mPitch;
        public int mVel;
        public NOTE(LENGTH _length, PITCH _pitch)
        {
            mLength = _length;
            mPitch = _pitch;
            mVel = 100;
        }
        public NOTE(LENGTH _length, PITCH _pitch, int _vel)
        {
            mLength = _length;
            mPitch = _pitch;
            mVel = _vel;
        }

        public static NOTE[] Create(LENGTH _length, PITCH[] _pitch)
        {
            List<NOTE> notes = new List<NOTE>();
            foreach(PITCH pitch in _pitch)
            {
                notes.Add(new NOTE(_length, pitch));
            }
            return notes.ToArray();
        }
    }

    /// <summary>
    /// LENGTH
    /// 音の長さ
    /// </summary>
    public enum LENGTH
    {
        N1x2 = 3840,    //  全音符×二小節
        N1 = 1920,      //  全音符
        N2 = 960,       //  二分音符
        N4d = 720,      //  符点四分音符
        N3 = 640,       //  三連二部音符
        N4 = 480,       //  四分音符
        N8d = 360,      //  符点八分音符
        N6 = 320,       //  三連四部音符
        N8 = 240,       //  八分音符
        N16d = 180,     //  符点十六分音符
        N16 = 120,      //  十六分音符
        Nx = 0,
    }

    /// <summary>
    /// PITCH
    /// 音の高さ
    /// </summary>
    public enum PITCH
    {
        x = 35,
        C1, Cs1, D1, Ds1, E1, F1, Fs1, G1, Gs1, A1, As1, B1,
        C2, Cs2, D2, Ds2, E2, F2, Fs2, G2, Gs2, A2, As2, B2,
        C3, Cs3, D3, Ds3, E3, F3, Fs3, G3, Gs3, A3, As3, B3,
        C4, Cs4, D4, Ds4, E4, F4, Fs4, G4, Gs4, A4, As4, B4,
        C5, Cs5, D5, Ds5, E5, F5, Fs5, G5, Gs5, A5, As5, B5,
        C6,
        MAX,
    }
    class AdlivMusic2
    {
    }
}
