using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

///##################################################
///     Adlivメインクラス
///                                     2018/07/28
///                                     はぅ君
///##################################################
///
namespace AdlivMusic
{
    public class CHORDM
    {
        public enum KEY
        {
            x, C, D, E, F, G, A, B, MAX,
        }
        public enum EXT
        {
            x, dim, aug, sus4, x6, x7, M7
        }

        KEY mKey;           //  キー
        bool mKeySharp;     //  キーが#かどうか
        bool mMinor;        //  マイナーコードかどうか
        EXT mExt;           //  コードの拡張部
        KEY mOn;            //  コードのOn部
        NOTE.TONE mAnchor;  //  アンカー（和音を得る際の最大値）
        NOTE.TONE mOnAnchor;//  Onアンカー

        public CHORDM(KEY _key, bool sharp = false, bool minor = false, EXT _ext = EXT.x, KEY _on = KEY.x, NOTE.TONE anchor = NOTE.TONE.MAX, NOTE.TONE onAnchor = NOTE.TONE.MAX)
        {
            mKey = _key;
            mKeySharp = sharp;
            mMinor = minor;
            mExt = _ext;
            mOn = _on == KEY.x ? mKey : _on;
            mAnchor = anchor;
            mOnAnchor = onAnchor;
        }
        public CHORDM New(int roma, int onRoma)
        {
            return New(roma, false, false, EXT.x, onRoma);
        }
        public CHORDM New(int roma, EXT ext)
        {
            return New(roma, false, false, ext);
        }
        public CHORDM New(int roma, bool sharp = false, bool minor = false, EXT _ext = EXT.x, int _onRoma = -1, NOTE.TONE anchor = NOTE.TONE.MAX, NOTE.TONE onAnchor = NOTE.TONE.MAX)
        {
            int tempKey = (int)mKey + (roma - 1);
            while(tempKey >= (int)KEY.MAX)
            {
                tempKey -= 7;
            }

            int tempOn = tempKey;
            if (_onRoma != -1)
            {
                tempOn = (int)mKey + (_onRoma - 1);
                while (tempOn >= (int)KEY.MAX)
                {
                    tempOn -= 7;
                }
            }

            return new CHORDM((KEY)tempKey, sharp, minor, _ext, (KEY)tempOn, anchor == NOTE.TONE.MAX ? mAnchor : anchor, onAnchor == NOTE.TONE.MAX ? mOnAnchor : onAnchor);
        }

        public void SetAnchor(NOTE.TONE anchor, NOTE.TONE on)
        {
            mAnchor = anchor;
            mOnAnchor = on;
        }

        /// <summary>
        /// 音階の音を得る
        /// </summary>
        /// <returns></returns>
        public NOTE.TONE[] GetScaleTones()
        {
            List<NOTE.TONE> tones = new List<NOTE.TONE>();

            NOTE temp = new NOTE(GetBaseTone());
            tones.Add(temp.GetTone());

            if(! mMinor)
            {
                temp.Add(2);tones.Add(temp.GetTone());
                temp.Add(2);tones.Add(temp.GetTone());
                temp.Add(1);tones.Add(temp.GetTone());
                temp.Add(2);tones.Add(temp.GetTone());
                temp.Add(2);tones.Add(temp.GetTone());
                temp.Add(2);tones.Add(temp.GetTone());
                temp.Add(1);tones.Add(temp.GetTone());
            }
            else
            {
                temp.Add(2); tones.Add(temp.GetTone());
                temp.Add(1); tones.Add(temp.GetTone());
                temp.Add(2); tones.Add(temp.GetTone());
                temp.Add(2); tones.Add(temp.GetTone());
                temp.Add(1); tones.Add(temp.GetTone());
                temp.Add(2); tones.Add(temp.GetTone());
                temp.Add(2); tones.Add(temp.GetTone());
            }

            while (tones.Last() > mAnchor)
            {
                List<NOTE.TONE> tempTones = new List<NOTE.TONE>();
                foreach (NOTE.TONE tone in tones)
                {
                    NOTE tempNote = new NOTE(tone);
                    tempNote.Add(-12);
                    tempTones.Add(tempNote.GetTone());
                }
                tones = tempTones;
            }
            return tones.ToArray();
        }

        public NOTE.TONE GetOffsetTone(NOTE.TONE target, int offsetWidth)
        {
            NOTE.TONE[] scaleTones = GetScaleTones();
            while (offsetWidth < 0)
            {
                offsetWidth += 8;
            }
            while (offsetWidth >= 8)
            {
                offsetWidth -= 8;
            }

            NOTE.TONE resultTone = scaleTones[offsetWidth];
            while (resultTone - target < 7)
            {
                resultTone += 12;
            }
            while (resultTone - target < 7)
            {
                resultTone += 12;
            }
            while (resultTone - target > 7)
            {
                resultTone -= 12;
            }

            return resultTone;
        }

        /// <summary>
        /// 根音を得る
        /// </summary>
        /// <returns></returns>
        public NOTE.TONE GetBaseTone(NOTE.TONE _anchor = NOTE.TONE.x)
        {
            NOTE.TONE tempTone = KeyToTone(mKey);

            NOTE.TONE anchor = _anchor == NOTE.TONE.x ? mAnchor : _anchor;
            while (tempTone > anchor)
            {
                NOTE tempNote = new NOTE(tempTone);
                tempNote.Add(-12);
                tempTone = tempNote.GetTone();
            }
            return tempTone;
        }

        /// <summary>
        /// Onを得る
        /// </summary>
        /// <returns></returns>
        public NOTE.TONE GetOnTone()
        {
            NOTE.TONE tempTone = KeyToTone(mOn);
            while (tempTone > mOnAnchor)
            {
                NOTE tempNote = new NOTE(tempTone);
                tempNote.Add(-12);
                tempTone = tempNote.GetTone();
            }
            return tempTone;
        }

        /// <summary>
        /// Onを得る
        /// </summary>
        /// <returns></returns>
        private NOTE.TONE KeyToTone(KEY key)
        {
            switch (key)
            {
                case KEY.C:
                    return !mKeySharp ? NOTE.TONE.C5 : NOTE.TONE.Cs5;
                case KEY.D:
                    return !mKeySharp ? NOTE.TONE.D5 : NOTE.TONE.Ds5;
                case KEY.E:
                    return !mKeySharp ? NOTE.TONE.E5 : NOTE.TONE.F5;
                case KEY.F:
                    return !mKeySharp ? NOTE.TONE.F5 : NOTE.TONE.Fs5;
                case KEY.G:
                    return !mKeySharp ? NOTE.TONE.G5 : NOTE.TONE.Gs5;
                case KEY.A:
                    return !mKeySharp ? NOTE.TONE.A5 : NOTE.TONE.As5;
                case KEY.B:
                    return !mKeySharp ? NOTE.TONE.B5 : NOTE.TONE.C6;
                default:
                    return NOTE.TONE.x;
            }
        }

        /// <summary>
        /// 和音を得る
        /// </summary>
        /// <returns></returns>
        public NOTE.TONE[] GetChordTones()
        {
            List<NOTE.TONE> tones = new List<NOTE.TONE>();
            
            NOTE temp = new NOTE(GetBaseTone());
            tones.Add(temp.GetTone());

            switch (mExt)
            {
                case EXT.x:
                    if(!mMinor)
                    {
                        temp.Add(4);tones.Add(temp.GetTone());
                        temp.Add(3);tones.Add(temp.GetTone());
                    }
                    else
                    {
                        temp.Add(3);tones.Add(temp.GetTone());
                        temp.Add(4);tones.Add(temp.GetTone());
                    }
                    break;
                case EXT.sus4:
                        temp.Add(5); tones.Add(temp.GetTone());
                        temp.Add(2); tones.Add(temp.GetTone());
                    break;
                case EXT.dim:
                    if(!mMinor)
                    {
                        temp.Add(3);tones.Add(temp.GetTone());
                        temp.Add(3);tones.Add(temp.GetTone());
                    }
                    else
                    {
                        temp.Add(2);tones.Add(temp.GetTone());
                        temp.Add(4);tones.Add(temp.GetTone());
                    }
                    break;
                case EXT.aug:
                    if (!mMinor)
                    {
                        temp.Add(4); tones.Add(temp.GetTone());
                        temp.Add(4); tones.Add(temp.GetTone());
                    }
                    else
                    {
                        temp.Add(3); tones.Add(temp.GetTone());
                        temp.Add(5); tones.Add(temp.GetTone());
                    }
                    break;
                case EXT.x6:
                    if (!mMinor)
                    {
                        temp.Add(4); tones.Add(temp.GetTone());
                        temp.Add(3); tones.Add(temp.GetTone());
                        temp.Add(2); tones.Add(temp.GetTone());
                    }
                    else
                    {
                        temp.Add(3); tones.Add(temp.GetTone());
                        temp.Add(4); tones.Add(temp.GetTone());
                        temp.Add(2); tones.Add(temp.GetTone());
                    }
                    break;
                case EXT.M7:
                    if (!mMinor)
                    {
                        temp.Add(4); tones.Add(temp.GetTone());
                        temp.Add(3); tones.Add(temp.GetTone());
                        temp.Add(4); tones.Add(temp.GetTone());
                    }
                    else
                    {
                        temp.Add(3); tones.Add(temp.GetTone());
                        temp.Add(4); tones.Add(temp.GetTone());
                        temp.Add(4); tones.Add(temp.GetTone());
                    }
                    break;
                case EXT.x7:
                    if (!mMinor)
                    {
                        temp.Add(4); tones.Add(temp.GetTone());
                        temp.Add(3); tones.Add(temp.GetTone());
                        temp.Add(3); tones.Add(temp.GetTone());
                    }
                    else
                    {
                        temp.Add(3); tones.Add(temp.GetTone());
                        temp.Add(4); tones.Add(temp.GetTone());
                        temp.Add(3); tones.Add(temp.GetTone());
                    }
                    break;
            }

            while (tones.Last() > mAnchor)
            {
                NOTE.TONE tempTone = tones.Last();
                tones.Remove(tempTone);
                NOTE tempNote = new NOTE(tempTone);
                tempNote.Add(-12);
                tones.Insert(0, tempNote.GetTone());
            }

            return tones.ToArray();
        }
        
    }


    /// <summary>
    /// CHORD
    /// 調
    /// </summary>
    public class CHORD
    {
        private static Random r = new Random();

        private const int IS_RANDOM = -1;
        
        /// <summary>
        /// 調名
        /// </summary>
        public enum NAME
        {
            C, C7,
            D, D7, Dm, Dm7,
            E, E7, Em, Em7,
            F, FM7, Fm,
            G, Gsus4, G7,
            A, Am, Am7,
            B,
        }

        private NAME mName;

        public CHORD(NAME name)
        {
            mName = name;
        }

        /// <summary>
        /// 調上の音程を得る
        /// </summary>
        /// <returns></returns>
        public NOTE.TONE[] GetScaleTones()
        {
            switch (mName)
            {
                case CHORD.NAME.C:
                case CHORD.NAME.C7:
                    return SCALE.SCALE_C;
                case CHORD.NAME.D:
                case CHORD.NAME.D7:
                    return SCALE.SCALE_D;
                case CHORD.NAME.Dm:
                case CHORD.NAME.Dm7:
                    return SCALE.SCALE_Dm1;
                case CHORD.NAME.E:
                case CHORD.NAME.E7:
                    return SCALE.SCALE_E;
                case CHORD.NAME.Em:
                case CHORD.NAME.Em7:
                    return SCALE.SCALE_Em1;
                case CHORD.NAME.F:
                case CHORD.NAME.FM7:
                    return SCALE.SCALE_F;
                case CHORD.NAME.Fm:
                    return SCALE.SCALE_Fm1;
                case CHORD.NAME.G:
                case CHORD.NAME.Gsus4:
                case CHORD.NAME.G7:
                    return SCALE.SCALE_G;
                case CHORD.NAME.A:
                    return SCALE.SCALE_A;
                case CHORD.NAME.Am:
                case CHORD.NAME.Am7:
                    return SCALE.SCALE_Am1;
                case CHORD.NAME.B:
                    return SCALE.SCALE_B;
                default:
                    return SCALE.SCALE_x;
            }
        }

        /// <summary>
        /// 調上のオフセット音程を得る
        /// </summary>
        /// <param name="tone"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public NOTE GetOffsetNote(NOTE.TONE tone, int offset)
        {
            NOTE.TONE[] scaleTones = GetScaleTones();

            //  同音を探す
            int saveIndex = -1;
            for (int i = 0; i < 8; i++)
            {
                if ((int)scaleTones[i] % 12 == (int)tone % 12)
                {
                    saveIndex = i;
                    break;
                }
            }
            if (saveIndex == -1)
            {
                return new NOTE(NOTE.TONE.x);
            }

            //  オクターブ調整
            int adjuster = (int)tone - (int)scaleTones[saveIndex];

            //  音階内に収まるように調整
            saveIndex += offset;
            while (saveIndex < 0)
            {
                saveIndex += 7;
                adjuster -= 12;
            }
            while (saveIndex > 7)
            {
                saveIndex -= 7;
                adjuster += 12;
            }

            return new NOTE(scaleTones[saveIndex] + adjuster);
        }

        /// <summary>
        /// 音程を取得
        /// </summary>
        /// <param name="oct"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public NOTE GetNote(int oct = 0, int index = IS_RANDOM)
        {
            NOTE.TONE resultTone = NOTE.TONE.x;
            if (index == IS_RANDOM)
            {
                index = r.Next(4);
            }
            switch (mName)
            {
                case CHORD.NAME.C:
                    resultTone = new NOTE.TONE[] { NOTE.TONE.G2, NOTE.TONE.C3, NOTE.TONE.E3, NOTE.TONE.E2 }[index];
                    break;
                case CHORD.NAME.C7:
                    resultTone = new NOTE.TONE[] { NOTE.TONE.G2, NOTE.TONE.C3, NOTE.TONE.E3, NOTE.TONE.As2 }[index];
                    break;
                case CHORD.NAME.D:
                    resultTone = new NOTE.TONE[] { NOTE.TONE.A2, NOTE.TONE.D3, NOTE.TONE.Fs3, NOTE.TONE.A3 }[index];
                    break;
                case CHORD.NAME.D7:
                    resultTone = new NOTE.TONE[] { NOTE.TONE.A2, NOTE.TONE.D3, NOTE.TONE.Fs3, NOTE.TONE.C3 }[index];
                    break;
                case CHORD.NAME.Dm:
                    resultTone = new NOTE.TONE[] { NOTE.TONE.A2, NOTE.TONE.D3, NOTE.TONE.F3, NOTE.TONE.A2 }[index];
                    break;
                case CHORD.NAME.Dm7:
                    resultTone = new NOTE.TONE[] { NOTE.TONE.A2, NOTE.TONE.D3, NOTE.TONE.F3, NOTE.TONE.C3 }[index];
                    break;
                case CHORD.NAME.E:
                    resultTone = new NOTE.TONE[] { NOTE.TONE.Gs2, NOTE.TONE.B2, NOTE.TONE.E3, NOTE.TONE.Gs2 }[index];
                    break;
                case CHORD.NAME.E7:
                    resultTone = new NOTE.TONE[] { NOTE.TONE.Gs2, NOTE.TONE.B2, NOTE.TONE.E3, NOTE.TONE.D3 }[index];
                    break;
                case CHORD.NAME.Em:
                    resultTone = new NOTE.TONE[] { NOTE.TONE.G2, NOTE.TONE.B2, NOTE.TONE.E3, NOTE.TONE.G3 }[index];
                    break;
                case CHORD.NAME.Em7:
                    resultTone = new NOTE.TONE[] { NOTE.TONE.G2, NOTE.TONE.B2, NOTE.TONE.E3, NOTE.TONE.D3 }[index];
                    break;
                case CHORD.NAME.F:
                    resultTone = new NOTE.TONE[] { NOTE.TONE.A2, NOTE.TONE.C3, NOTE.TONE.F3, NOTE.TONE.A2 }[index];
                    break;
                case CHORD.NAME.FM7:
                    resultTone = new NOTE.TONE[] { NOTE.TONE.A2, NOTE.TONE.C3, NOTE.TONE.F3, NOTE.TONE.E3 }[index];
                    break;
                case CHORD.NAME.Fm:
                    resultTone = new NOTE.TONE[] { NOTE.TONE.Gs2, NOTE.TONE.C3, NOTE.TONE.F3, NOTE.TONE.Gs2 }[index];
                    break;
                case CHORD.NAME.G:
                    resultTone = new NOTE.TONE[] { NOTE.TONE.G2, NOTE.TONE.B2, NOTE.TONE.D3, NOTE.TONE.G2 }[index];
                    break;
                case CHORD.NAME.Gsus4:
                    resultTone = new NOTE.TONE[] { NOTE.TONE.G2, NOTE.TONE.C3, NOTE.TONE.D3, NOTE.TONE.G2 }[index];
                    break;
                case CHORD.NAME.G7:
                    resultTone = new NOTE.TONE[] { NOTE.TONE.G2, NOTE.TONE.B2, NOTE.TONE.D3, NOTE.TONE.F3 }[index];
                    break;
                case CHORD.NAME.A:
                    resultTone = new NOTE.TONE[] { NOTE.TONE.A2, NOTE.TONE.Cs3, NOTE.TONE.E3, NOTE.TONE.A2 }[index];
                    break;
                case CHORD.NAME.Am:
                    resultTone = new NOTE.TONE[] { NOTE.TONE.A2, NOTE.TONE.C3, NOTE.TONE.E3, NOTE.TONE.A2 }[index];
                    break;
                case CHORD.NAME.Am7:
                    resultTone = new NOTE.TONE[] { NOTE.TONE.A2, NOTE.TONE.C3, NOTE.TONE.E3, NOTE.TONE.G2 }[index];
                    break;
                case CHORD.NAME.B:
                    resultTone = new NOTE.TONE[] { NOTE.TONE.B2, NOTE.TONE.Cs3, NOTE.TONE.Fs3, NOTE.TONE.B3 }[index];
                    break;
            }

            resultTone += oct * 12;
            return new NOTE(resultTone);
        }
        public NOTE GetNoteAdjusted(NOTE baseNote)
        {
            NOTE note = new NOTE(GetNote());
            note.Adjust(baseNote);
            return note;
        }

        /// <summary>
        /// オフセット音程を得る
        /// </summary>
        /// <param name="range"></param>
        /// <param name="isFix"></param>
        /// <returns></returns>
        public NOTE GetOffsetNote(int range = 0, bool isFix = false)
        {
            NOTE.TONE resultTone = NOTE.TONE.x;
            int index = isFix ? range : r.Next(Math.Abs(range) * 2 + 1) - Math.Abs(range);

            int oct = 0;
            while (index < 0)
            {
                index += 7;
                oct -= 1;
            }
            while (index > 7)
            {
                index -= 7;
                oct += 1;
            }

            NOTE.TONE[] scaleTones = GetScaleTones();
            resultTone = scaleTones[index] + (oct * 12);
            return new NOTE(resultTone);
        }
    }

    /// <summary>
    /// VOICE
    /// 声
    /// </summary>
    public class VOICE
    {
        static Random r = new Random();
        public static string[] VOICES = new string[] { "a", "ka", "sa", "ta", "na", "ha", "ma", "ya",  "ra", "wa", "ga", "ba", "za", "da", "pa",
                                                    "i", "ki", "si", "ti", "ni", "hi", "mi",        "ri",       "gi", "bi", "zi", "di", "pi",
                                                    "u", "ku", "su", "tu", "nu", "hu", "mu", "yu",  "ru",       "gu", "bu", "zu", "du", "pu",
                                                    "e", "ke", "se", "te", "ne", "he", "me",        "re",       "ge", "be", "ze", "de", "pe",
                                                    "o", "ko", "so", "to", "no", "ho", "mo", "yo",  "ro", "wo", "go", "bo", "zo", "do", "po",
                                                    "n",
                                                    "kya", "kyu", "kyo",
                                                    "gya", "gyu", "gyo",
                                                    "sya", "syu", "she", "syo",
                                                    "tya", "tyu", "the", "tyo",
                                                    "zya", "zyu", "zhe", "zyo",
                                                    "hya", "hyu", "hhe", "hyo",
                                                    "bya", "byu", "bhe", "byo",
                                                    "pya", "pyu", "phe", "pyo",
                                                    "fa", "fu", "fe", "fo",
        };

        public static string GetVoice()
        {
            return VOICES[r.Next(VOICES.Length)];
        }
    }

    /// <summary>
    /// SCALE
    /// 音階
    /// </summary>
    public class SCALE
    {
        public static NOTE.TONE[] SCALE_x = new NOTE.TONE[] { NOTE.TONE.x};
        public static NOTE.TONE[] SCALE_C = new NOTE.TONE[] { NOTE.TONE.C3, NOTE.TONE.D3, NOTE.TONE.E3, NOTE.TONE.F3, NOTE.TONE.G3, NOTE.TONE.A3, NOTE.TONE.B3, NOTE.TONE.C4 };
        public static NOTE.TONE[] SCALE_D = new NOTE.TONE[] { NOTE.TONE.D3, NOTE.TONE.E3, NOTE.TONE.Fs3, NOTE.TONE.G3, NOTE.TONE.A3, NOTE.TONE.B3, NOTE.TONE.Cs4, NOTE.TONE.D4 };
        public static NOTE.TONE[] SCALE_Dm1 = new NOTE.TONE[] { NOTE.TONE.D3, NOTE.TONE.E3, NOTE.TONE.F3, NOTE.TONE.G3, NOTE.TONE.A3, NOTE.TONE.As3, NOTE.TONE.C4, NOTE.TONE.D4 };
        public static NOTE.TONE[] SCALE_E = new NOTE.TONE[] { NOTE.TONE.E3, NOTE.TONE.Fs3, NOTE.TONE.Gs3, NOTE.TONE.A3, NOTE.TONE.B3, NOTE.TONE.Cs4, NOTE.TONE.Ds4, NOTE.TONE.E4 };
        public static NOTE.TONE[] SCALE_Em1 = new NOTE.TONE[] { NOTE.TONE.E3, NOTE.TONE.Fs3, NOTE.TONE.G3, NOTE.TONE.A3, NOTE.TONE.B3, NOTE.TONE.C4, NOTE.TONE.D4, NOTE.TONE.E4 };
        public static NOTE.TONE[] SCALE_F = new NOTE.TONE[] { NOTE.TONE.F3, NOTE.TONE.G3, NOTE.TONE.A3, NOTE.TONE.As3, NOTE.TONE.C4, NOTE.TONE.D4, NOTE.TONE.E4, NOTE.TONE.F4 };
            public static NOTE.TONE[] SCALE_Fm1 = new NOTE.TONE[] { NOTE.TONE.F3, NOTE.TONE.G3, NOTE.TONE.Gs3, NOTE.TONE.As3, NOTE.TONE.C4, NOTE.TONE.Cs4, NOTE.TONE.Ds4, NOTE.TONE.F4 };
        public static NOTE.TONE[] SCALE_G = new NOTE.TONE[] { NOTE.TONE.G3, NOTE.TONE.A3, NOTE.TONE.B3, NOTE.TONE.C4, NOTE.TONE.D4, NOTE.TONE.E4, NOTE.TONE.Fs4, NOTE.TONE.G4 };
        public static NOTE.TONE[] SCALE_A = new NOTE.TONE[] { NOTE.TONE.A3, NOTE.TONE.B3, NOTE.TONE.Cs4, NOTE.TONE.D4, NOTE.TONE.E4, NOTE.TONE.Fs4, NOTE.TONE.Gs4, NOTE.TONE.A4 };
        public static NOTE.TONE[] SCALE_Am1 = new NOTE.TONE[] { NOTE.TONE.A3, NOTE.TONE.B3, NOTE.TONE.C4, NOTE.TONE.D4, NOTE.TONE.E4, NOTE.TONE.F4, NOTE.TONE.G4, NOTE.TONE.A4 };
        public static NOTE.TONE[] SCALE_B = new NOTE.TONE[] { NOTE.TONE.B3, NOTE.TONE.Cs4, NOTE.TONE.Ds4, NOTE.TONE.E4, NOTE.TONE.Fs4, NOTE.TONE.Gs4, NOTE.TONE.As4, NOTE.TONE.B4 };
    }

    /// <summary>
    /// NOTE
    /// 音程
    /// </summary>
    public class NOTE
    {
        public enum TONE
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

        public static explicit operator int(NOTE note)
        {
            return (int)note.GetTone();
        }
        public static explicit operator TONE(NOTE note)
        {
            return note.GetTone();
        }

        private TONE mTone;
        public NOTE(NOTE note)
        {
            mTone = note.GetTone();
        }
        public NOTE(TONE tone)
        {
            mTone = tone;
        }
        public NOTE(int tone)
        {
            mTone = (TONE)tone;
        }

        public void Add(int offset)
        {
            mTone += offset;
        }
        public TONE GetTone()
        {
            return mTone;
        }
        public void Adjust(NOTE baseNote)
        {
            Adjust((int)baseNote.GetTone());
        }
        public void Adjust(int baseTone)
        {
            while ((int)mTone - (int)baseTone > 7)
            {
                mTone -= 12;
            }
            while ((int)mTone - (int)baseTone < -7)
            {
                mTone += 12;
            }
        }

        public static TONE GetNearNote(TONE[] tones, TONE target)
        {
            TONE toneResult = TONE.x;
            int minDist = 99;

            foreach (NOTE.TONE tone in tones)
            {
                int temp = tone - target;
                if (Math.Abs(temp) < minDist)
                {
                    toneResult = tone;
                    minDist = temp;
                }
            }

            while (minDist >= 7)
            {
                minDist -= 12;
                toneResult += 12;
            }
            while (minDist <= -7)
            {
                minDist += 12;
                toneResult -= 12;
            }

            return toneResult;
        }

        public static TONE Adjust(TONE tone, TONE target)
        {
            int temp = tone - target;

            while (temp >= 7)
            {
                temp -= 12;
                tone -= 12;
            }
            while (temp <= -7)
            {
                temp += 12;
                tone += 12;
            }

            return tone;
        }
    }

    /// <summary>
    /// LENGTH
    /// 音調
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
}
