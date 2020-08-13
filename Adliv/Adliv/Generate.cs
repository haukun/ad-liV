using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using AdlivMusic;
using System.Windows.Forms;
using System.IO;
using System.Data.SQLite;

namespace Adliv
{
    class Generate
    {
        /*
        static Random r = new Random();

        public static void Exec()
        {
            if (!Directory.Exists("output"))
            {
                Directory.CreateDirectory("output");
            }

            string vsq4Path = String.Format("{0}\\{1}{2}", "output", DateTime.Now.ToString("yyyyMMdd-HHmmss"), "-adliv.vsqx");
            string midiPath = String.Format("{0}\\{1}{2}", "output", DateTime.Now.ToString("yyyyMMdd-HHmmss"), "-adliv.midi");

            AdlivVsq4 vsq4 = new AdlivVsq4(vsq4Path, "BCNFCY43LB2LZCD4", "MIKU_V4X_Original_EVEC", "BFPL93T7GE3RWFC9", "ACA9C502-A04B-42b5-B2EB-5CEA36D16FCE", "VlNDSwAAAAADAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA=");
            AdlivMidi midi = new AdlivMidi(midiPath);

            midi.SetTempo(120);

            //  生成
            //Test(midi, vsq4);
            //Test2(midi, vsq4);
            Exp2(midi, vsq4);
            

            //  出力
            midi.Write();
            vsq4.Write();
        }
        */
        /*
        public static void Exp2(AdlivMidi midi, AdlivVsq4 vsq4)
        {
            AdlivVsq4.vsTrack[] vTrack = new AdlivVsq4.vsTrack[5];
            vTrack[0] = vsq4.CreateTrack();

            AdlivMidi.MidiTrack[] mTrack = new AdlivMidi.MidiTrack[16];
            mTrack[0] = midi.CreateTrack(AdlivMidi.TONE.T001_PIANO_ACOUSTIC_PIANO);

            

            List<CHORDM> chordms = new List<CHORDM>();
            chordms.Add(new CHORDM(CHORDM.KEY.C));
            chordms.Add(new CHORDM(CHORDM.KEY.F));
            chordms.Add(new CHORDM(CHORDM.KEY.G, false, false, CHORDM.EXT.x7));
            chordms.Add(new CHORDM(CHORDM.KEY.C));

            PatternManager pm = new PatternManager();
            pm.Generate(chordms);
            PatternManager.PATTERN pattern =  pm.patterns[0];
            foreach(PatternManager.SOUND sound in pattern.sounds)
            {
                mTrack[0].AddNote(sound.length, sound.tone);
            }

            vTrack[0].AddNote(LENGTH.N1, NOTE.TONE.C3, "ra");
            vTrack[0].AddNote(LENGTH.N1, NOTE.TONE.F3, "ra");
            vTrack[0].AddNote(LENGTH.N1, NOTE.TONE.G3, "ra");
            vTrack[0].AddNote(LENGTH.N1, NOTE.TONE.C3, "ra");
        }

        private class PatternManager
        {
            static Random r = new Random();
            public struct SOUND
            {
                public SOUND(NOTE.TONE _tone, LENGTH _length)
                {
                    tone = _tone;
                    length = _length;
                }
                public NOTE.TONE tone;
                public LENGTH length;
            }

            public enum TYPE
            {
                MAIN,
                SUB,
                ACCENT,
                BASE,
                DRUM,
            }

            public class PATTERN
            {
                public List<SOUND> sounds;
                public int violance;
                public TYPE type;
                public PATTERN()
                {
                    sounds = new List<SOUND>();
                    violance = 0;
                }
            }
            public PATTERN[] patterns = new PATTERN[16];
            NOTE.TONE mainAnchor;
            NOTE.TONE onAnchor;

            List<CHORDSTYPE> chordstypes = new List<CHORDSTYPE>();

            public PatternManager()
            {
                for (int i = 0; i < patterns.Length; i++)
                {
                    patterns[i] = new PATTERN();
                }
                patterns[0].type = TYPE.MAIN;
                patterns[1].type = TYPE.ACCENT;
                patterns[2].type = TYPE.ACCENT;
                patterns[3].type = TYPE.ACCENT;
                patterns[4].type = TYPE.SUB;
                patterns[5].type = TYPE.MAIN;
                patterns[6].type = TYPE.SUB;
                patterns[7].type = TYPE.SUB;
                patterns[8].type = TYPE.BASE;
                patterns[9].type = TYPE.DRUM;
                patterns[10].type = TYPE.BASE;
                patterns[11].type = TYPE.MAIN;
                patterns[12].type = TYPE.SUB;
                patterns[13].type = TYPE.SUB;
                patterns[14].type = TYPE.SUB;
                patterns[15].type = TYPE.MAIN;

                mainAnchor = NOTE.TONE.G4;
                onAnchor = NOTE.TONE.C2;
            }

            public void AddChordsType(CHORDSTYPE type)
            {
                chordstypes.Add(type);
            }
            public CHORDSTYPE GetChordsType()
            {
                CHORDSTYPE type = CHORDSTYPE.MAX;
                if (chordstypes.Count > 0)
                {
                    type = chordstypes.First();
                    chordstypes.RemoveAt(0);
                }
                return type;
            }

            /// <summary>
            /// パターン生成
            /// </summary>
            /// <param name="chordm"></param>
            public void Generate(List<CHORDM> chordms)
            {
                foreach (PATTERN p in patterns)
                {
                    p.sounds.Clear();
                }
                foreach(CHORDM chordm in chordms)  //  8小節分生成する
                {
                    NOTE.TONE[] chordTones;
                    NOTE.TONE[] scaleTones;
                    NOTE.TONE baseTones;

                    //  最初と最後の音は和音上のものにする
                    NOTE.TONE firstTone;
                    NOTE.TONE lastTone;

                    foreach (PATTERN p in patterns)
                    {
                        switch (p.type)
                        {
                            case TYPE.MAIN:
                                chordm.SetAnchor(mainAnchor, onAnchor);
                                chordTones = chordm.GetChordTones();
                                scaleTones = chordm.GetScaleTones();
                                baseTones = chordm.GetBaseTone();

                                //  最初と最後の音は和音上のものにする
                                firstTone = chordTones[r.Next(chordTones.Length)];
                                lastTone = chordTones[r.Next(chordTones.Length)];

                                if (r.Next(100) < 10)   //  全音符
                                {
                                    p.sounds.Add(new SOUND(firstTone, LENGTH.N1));
                                }
                                else if (r.Next(100) < 20)   //  二・四部分割
                                {
                                    switch (r.Next(4))
                                    {
                                        case 0:
                                            p.sounds.Add(new SOUND(firstTone, LENGTH.N2));
                                            p.sounds.Add(new SOUND(lastTone, LENGTH.N2));
                                            break;
                                        case 1:
                                            p.sounds.Add(new SOUND(firstTone, LENGTH.N2));
                                            p.sounds.Add(new SOUND(chordTones[r.Next(chordTones.Length)], LENGTH.N4));
                                            p.sounds.Add(new SOUND(lastTone, LENGTH.N4));
                                            break;
                                        case 2:
                                            p.sounds.Add(new SOUND(firstTone, LENGTH.N4));
                                            p.sounds.Add(new SOUND(chordTones[r.Next(chordTones.Length)], LENGTH.N2));
                                            p.sounds.Add(new SOUND(lastTone, LENGTH.N4));
                                            break;
                                        case 3:
                                            p.sounds.Add(new SOUND(firstTone, LENGTH.N4));
                                            p.sounds.Add(new SOUND(chordTones[r.Next(chordTones.Length)], LENGTH.N4));
                                            p.sounds.Add(new SOUND(lastTone, LENGTH.N2));
                                            break;
                                    }
                                }
                                else
                                {    //  四・八部分割
                                    if (r.Next(100) < 50)   //  最初の音
                                    {
                                        p.sounds.Add(new SOUND(firstTone, LENGTH.N4));
                                    }
                                    else
                                    {
                                        p.sounds.Add(new SOUND(firstTone, LENGTH.N8));
                                        p.sounds.Add(new SOUND(chordTones[r.Next(chordTones.Length)], LENGTH.N8));
                                    }
                                    switch (r.Next(3))
                                    {
                                        case 0:
                                            p.sounds.Add(new SOUND(chordTones[r.Next(chordTones.Length)], LENGTH.N4));
                                            p.sounds.Add(new SOUND(chordTones[r.Next(chordTones.Length)], LENGTH.N4));
                                            break;
                                        case 1:
                                            p.sounds.Add(new SOUND(chordTones[r.Next(chordTones.Length)], LENGTH.N4));
                                            p.sounds.Add(new SOUND(chordTones[r.Next(chordTones.Length)], LENGTH.N8));
                                            p.sounds.Add(new SOUND(chordTones[r.Next(chordTones.Length)], LENGTH.N8));
                                            break;
                                        case 2:
                                            p.sounds.Add(new SOUND(chordTones[r.Next(chordTones.Length)], LENGTH.N8));
                                            p.sounds.Add(new SOUND(chordTones[r.Next(chordTones.Length)], LENGTH.N4));
                                            p.sounds.Add(new SOUND(chordTones[r.Next(chordTones.Length)], LENGTH.N8));
                                            break;
                                        case 3:
                                            p.sounds.Add(new SOUND(chordTones[r.Next(chordTones.Length)], LENGTH.N8));
                                            p.sounds.Add(new SOUND(chordTones[r.Next(chordTones.Length)], LENGTH.N8));
                                            p.sounds.Add(new SOUND(chordTones[r.Next(chordTones.Length)], LENGTH.N4));
                                            break;
                                        case 4:
                                            p.sounds.Add(new SOUND(chordTones[r.Next(chordTones.Length)], LENGTH.N8));
                                            p.sounds.Add(new SOUND(chordTones[r.Next(chordTones.Length)], LENGTH.N8));
                                            p.sounds.Add(new SOUND(chordTones[r.Next(chordTones.Length)], LENGTH.N8));
                                            p.sounds.Add(new SOUND(chordTones[r.Next(chordTones.Length)], LENGTH.N8));
                                            break;
                                    }
                                    if (r.Next(100) < 50)   //  最後の音
                                    {
                                        p.sounds.Add(new SOUND(lastTone, LENGTH.N4));
                                    }
                                    else
                                    {
                                        p.sounds.Add(new SOUND(chordTones[r.Next(chordTones.Length)], LENGTH.N8));
                                        p.sounds.Add(new SOUND(lastTone, LENGTH.N8));
                                    }
                                }
                                break;
                            case TYPE.SUB:
                                chordm.SetAnchor(mainAnchor - 12, onAnchor);
                                chordTones = chordm.GetChordTones();
                                scaleTones = chordm.GetScaleTones();
                                baseTones = chordm.GetBaseTone();

                                //  最初と最後の音は和音上のものにする
                                firstTone = chordTones[r.Next(chordTones.Length)];
                                lastTone = chordTones[r.Next(chordTones.Length)];

                                if (r.Next(100) < 10)   //  全音符
                                {
                                    p.sounds.Add(new SOUND(firstTone, LENGTH.N1));
                                }
                                else if (r.Next(100) < 20)   //  二・四部分割
                                {
                                    switch (r.Next(4))
                                    {
                                        case 0:
                                            p.sounds.Add(new SOUND(firstTone, LENGTH.N2));
                                            p.sounds.Add(new SOUND(lastTone, LENGTH.N2));
                                            break;
                                        case 1:
                                            p.sounds.Add(new SOUND(firstTone, LENGTH.N2));
                                            p.sounds.Add(new SOUND(chordTones[r.Next(chordTones.Length)], LENGTH.N4));
                                            p.sounds.Add(new SOUND(lastTone, LENGTH.N4));
                                            break;
                                        case 2:
                                            p.sounds.Add(new SOUND(firstTone, LENGTH.N4));
                                            p.sounds.Add(new SOUND(chordTones[r.Next(chordTones.Length)], LENGTH.N2));
                                            p.sounds.Add(new SOUND(lastTone, LENGTH.N4));
                                            break;
                                        case 3:
                                            p.sounds.Add(new SOUND(firstTone, LENGTH.N4));
                                            p.sounds.Add(new SOUND(chordTones[r.Next(chordTones.Length)], LENGTH.N4));
                                            p.sounds.Add(new SOUND(lastTone, LENGTH.N2));
                                            break;
                                    }
                                }
                                else
                                {    //  四・八部分割
                                    if (r.Next(100) < 50)   //  最初の音
                                    {
                                        p.sounds.Add(new SOUND(firstTone, LENGTH.N4));
                                    }
                                    else
                                    {
                                        p.sounds.Add(new SOUND(firstTone, LENGTH.N8));
                                        p.sounds.Add(new SOUND(chordTones[r.Next(chordTones.Length)], LENGTH.N8));
                                    }
                                    switch (r.Next(3))
                                    {
                                        case 0:
                                            p.sounds.Add(new SOUND(chordTones[r.Next(chordTones.Length)], LENGTH.N4));
                                            p.sounds.Add(new SOUND(chordTones[r.Next(chordTones.Length)], LENGTH.N4));
                                            break;
                                        case 1:
                                            p.sounds.Add(new SOUND(chordTones[r.Next(chordTones.Length)], LENGTH.N4));
                                            p.sounds.Add(new SOUND(chordTones[r.Next(chordTones.Length)], LENGTH.N8));
                                            p.sounds.Add(new SOUND(chordTones[r.Next(chordTones.Length)], LENGTH.N8));
                                            break;
                                        case 2:
                                            p.sounds.Add(new SOUND(chordTones[r.Next(chordTones.Length)], LENGTH.N8));
                                            p.sounds.Add(new SOUND(chordTones[r.Next(chordTones.Length)], LENGTH.N4));
                                            p.sounds.Add(new SOUND(chordTones[r.Next(chordTones.Length)], LENGTH.N8));
                                            break;
                                        case 3:
                                            p.sounds.Add(new SOUND(chordTones[r.Next(chordTones.Length)], LENGTH.N8));
                                            p.sounds.Add(new SOUND(chordTones[r.Next(chordTones.Length)], LENGTH.N8));
                                            p.sounds.Add(new SOUND(chordTones[r.Next(chordTones.Length)], LENGTH.N4));
                                            break;
                                        case 4:
                                            p.sounds.Add(new SOUND(chordTones[r.Next(chordTones.Length)], LENGTH.N8));
                                            p.sounds.Add(new SOUND(chordTones[r.Next(chordTones.Length)], LENGTH.N8));
                                            p.sounds.Add(new SOUND(chordTones[r.Next(chordTones.Length)], LENGTH.N8));
                                            p.sounds.Add(new SOUND(chordTones[r.Next(chordTones.Length)], LENGTH.N8));
                                            break;
                                    }
                                    if (r.Next(100) < 50)   //  最後の音
                                    {
                                        p.sounds.Add(new SOUND(lastTone, LENGTH.N4));
                                    }
                                    else
                                    {
                                        p.sounds.Add(new SOUND(chordTones[r.Next(chordTones.Length)], LENGTH.N8));
                                        p.sounds.Add(new SOUND(lastTone, LENGTH.N8));
                                    }
                                }
                                break;
                            case TYPE.ACCENT:
                                chordm.SetAnchor(mainAnchor, onAnchor);
                                chordTones = chordm.GetChordTones();
                                scaleTones = chordm.GetScaleTones();
                                baseTones = chordm.GetBaseTone();
                                for (int i = 0; i < 8; i++)
                                {
                                    if (r.Next(100) < 5)
                                    {
                                        p.sounds.Add(new SOUND(chordTones[0], LENGTH.N8));
                                        p.sounds.Add(new SOUND(chordTones[1], LENGTH.N8));
                                        p.sounds.Add(new SOUND(chordTones[2], LENGTH.N8));
                                        p.sounds.Add(new SOUND(chordTones[0], LENGTH.N8));
                                    }
                                    else
                                    {
                                        p.sounds.Add(new SOUND(NOTE.TONE.x, LENGTH.N1));
                                    }
                                }
                                break;
                            case TYPE.BASE:
                                chordm.SetAnchor(mainAnchor - 24, onAnchor - 24);
                                chordTones = chordm.GetChordTones();
                                scaleTones = chordm.GetScaleTones();
                                baseTones = chordm.GetBaseTone();
                                for(int i = 0; i < 8; i++)
                                {
                                    p.sounds.Add(new SOUND(baseTones, LENGTH.N8));
                                }
                                break;
                            case TYPE.DRUM:
                                break;
                        }
                    }
                }
            }
        }

        public static void Exp(AdlivMidi midi, AdlivVsq4 vsq4)
        {
            midi.SetTempo(150);

            AdlivVsq4.vsTrack[] vTrack = new AdlivVsq4.vsTrack[5];
            vTrack[0] = vsq4.CreateTrack();
            vTrack[1] = vsq4.CreateTrack();

            AdlivMidi.MidiTrack[] mTrack = new AdlivMidi.MidiTrack[16];
            mTrack[0] = midi.CreateTrack(AdlivMidi.TONE.T001_PIANO_ACOUSTIC_PIANO);
            mTrack[1] = midi.CreateTrack(AdlivMidi.TONE.T007_PIANO_HARPSICHORD);
            mTrack[2] = midi.CreateTrack(AdlivMidi.TONE.T011_PERCUSSION_MUSICAL_BOX);
            mTrack[3] = midi.CreateTrack(AdlivMidi.TONE.T013_PERCUSSION_MARIMBA);
            mTrack[4] = midi.CreateTrack(AdlivMidi.TONE.T020_ORGAN_CHURCH_ORGAN);
            mTrack[5] = midi.CreateTrack(AdlivMidi.TONE.T023_ORGAN_HARMONICA);
            mTrack[6] = midi.CreateTrack(AdlivMidi.TONE.T026_GUITAR_ACOUSTIC_GUITAR_STEEL);
            mTrack[7] = midi.CreateTrack(AdlivMidi.TONE.T028_GUITAR_ELECTRIC_GUITAR_CLEAN);
            mTrack[8] = midi.CreateTrack(AdlivMidi.TONE.T037_BASS_SLAP_BASS1);
            mTrack[9] = midi.CreateTrack(AdlivMidi.TONE.T001_PIANO_ACOUSTIC_PIANO);
            mTrack[10] = midi.CreateTrack(AdlivMidi.TONE.T040_BASS_SYNTH_BASS2);
            mTrack[11] = midi.CreateTrack(AdlivMidi.TONE.T041_STRINGS_VIOLIN);
            mTrack[12] = midi.CreateTrack(AdlivMidi.TONE.T041_STRINGS_VIOLIN);
            mTrack[13] = midi.CreateTrack(AdlivMidi.TONE.T042_STRINGS_VIOLA);
            mTrack[14] = midi.CreateTrack(AdlivMidi.TONE.T074_PIPE_FLUTE);
            mTrack[15] = midi.CreateTrack(AdlivMidi.TONE.T072_REED_CLARINET);

            PatternManager pm = new PatternManager();

            List<CHORDM> chordsList = new List<CHORDM>();

            List<CHORDSTYPE> types = new List<CHORDSTYPE>();
            types.Add(CHORDSTYPE.C018_CANON);
            types.Add(CHORDSTYPE.C070_KIMI_WO_NOSETE);
            types.Add(CHORDSTYPE.C011_LET_IT_GO);
            types.Add(CHORDSTYPE.C009_ZANKOKU_NA_TENSHI_NO_TAZE);


            while (types.Count() > 0)
            {
                ResetChords(ref chordsList, types.First());
                types.RemoveAt(0);

                pm.Generate(chordsList);

                for (int i = 0; i < 16; i++)
                {
                    PatternManager.PATTERN pattern = pm.patterns[i];
                    foreach (PatternManager.SOUND sound in pattern.sounds)
                    {
                        mTrack[i].AddNote(sound.length, sound.tone, 64);
                    }
                }

            }
        }

        public enum CHORDSTYPE
        {
            C001_SAZAE_SAN_NO_UTA = 0,
            C002_DORAEMON_NO_UTA,
            C003_PUBLIC_SANKA,
            C004_TEGAMI,
            C005_SAKURA_DOKU,
            C006_SAKURA,
            C007_KANASIMI_HA_YUKINOYOUNI,
            C008_GAKUEN_TENGOKU,
            C009_ZANKOKU_NA_TENSHI_NO_TAZE,
            C010_SAKURANBO,
            C011_LET_IT_GO,
            C012_WA_NI_NATTE_ODORO,
            C013_DEPARTURES,
            C014_TENTAI_KANSOKU,
            C015_KANDAGAWA,
            C016_AMAGIGOE,
            C017_CHERRY,
            C018_CANON,
            C019_AI_HA_KATSU,
            C020_MIKADUKI,
            //C021_MAKENAIDE,
            //C022_KOKORONO_TABI,
            //C023_KAZE_NI_NARITAI,
            C024_KOKODE_KISUSHITE,
            C025_GEKKO,
            C026_WINTER_AGAIN,
            C030_AOI_INAZUMA,
            C031_FUTARI_DE_OTYA_WO,
            C033_TETUWAN_ATOM,
            C034_OYOGE_TAIYAKI_KUN,
            C035_AMAIRO_NO_KAMINO_OTOME,
            C036_TSUBOMI,
            C037_NADA_SOUSOU,
            C038_HITACHI_NO_KI,
            C039_GREEN_SLEEVES,
            C040_AKAI_KAWA_NO_TANIMA,
            C041_GEGEGE_NO_KITAROU,
            C042_UCHUSENKAN_YAMATO,
            C043_TENTOU_MUSHI_NO_SAMBA,
            C044_SYONEN_JIDAI,
            C045_IHOJIN,
            C046_YELL,
            C048_NAMIDAGA_KIRARI,
            C049_ZURUI_ONNA,
            //C051_MANATSU_NO_KAJITSU,
            C052_IKUZE_KAITOUSHOJO,
            C053_MANATSUNO_YORUNO_YUME,
            C054_I_GOT_RHYTHM,
            C070_KIMI_WO_NOSETE,
            C072_TSUBASA_WO_KUDASAI,
            //C123_ALONE_AGAIN,
            //C125_WAVE,
            MAX
        }
        */
        /*
        public static void ResetChords(ref List<CHORDM> chordsList, CHORDSTYPE type = CHORDSTYPE.MAX)
        {
            CHORDM baseChordm = new CHORDM(CHORDM.KEY.C);
            baseChordm.SetAnchor(NOTE.TONE.E4, NOTE.TONE.E3);

            chordsList.Clear();

            int select;
            if(type == CHORDSTYPE.MAX)
            {
                select = r.Next((int)CHORDSTYPE.MAX);
            }
            else
            {
                select = (int)type;
            }

            switch(select)
            {
                case (int)CHORDSTYPE.:
                    chordsList.Add(baseChordm.New());
                    chordsList.Add(baseChordm.New());
                    chordsList.Add(baseChordm.New());
                    chordsList.Add(baseChordm.New());
                    chordsList.Add(baseChordm.New());
                    chordsList.Add(baseChordm.New());
                    chordsList.Add(baseChordm.New());
                    chordsList.Add(baseChordm.New());
                    break;

                case (int)CHORDSTYPE.C001_SAZAE_SAN_NO_UTA:
                    chordsList.Add(baseChordm.New(1));
                    chordsList.Add(baseChordm.New(1));
                    chordsList.Add(baseChordm.New(2, false, true));
                    chordsList.Add(baseChordm.New(2, false, true));
                    chordsList.Add(baseChordm.New(3, false, true));
                    chordsList.Add(baseChordm.New(2, false, true));
                    chordsList.Add(baseChordm.New(5));
                    chordsList.Add(baseChordm.New(1));
                    break;
                case (int)CHORDSTYPE.C002_DORAEMON_NO_UTA:
                    chordsList.Add(baseChordm.New(1));
                    chordsList.Add(baseChordm.New(1));
                    chordsList.Add(baseChordm.New(1));
                    chordsList.Add(baseChordm.New(5, false, false, CHORDM.EXT.x7));
                    chordsList.Add(baseChordm.New(2, false, true));
                    chordsList.Add(baseChordm.New(5, false, false, CHORDM.EXT.x7));
                    chordsList.Add(baseChordm.New(4));
                    chordsList.Add(baseChordm.New(5, false, false, CHORDM.EXT.x7));
                    break;
                case (int)CHORDSTYPE.C003_PUBLIC_SANKA:
                    chordsList.Add(baseChordm.New(1));
                    chordsList.Add(baseChordm.New(1));
                    chordsList.Add(baseChordm.New(4));
                    chordsList.Add(baseChordm.New(1));
                    chordsList.Add(baseChordm.New(1));
                    chordsList.Add(baseChordm.New(3));
                    chordsList.Add(baseChordm.New(5));
                    chordsList.Add(baseChordm.New(1));
                    break;
                case (int)CHORDSTYPE.C004_TEGAMI:
                    chordsList.Add(baseChordm.New(1));
                    chordsList.Add(baseChordm.New(5));
                    chordsList.Add(baseChordm.New(6, false ,true));
                    chordsList.Add(baseChordm.New(4));
                    chordsList.Add(baseChordm.New(1));
                    chordsList.Add(baseChordm.New(5));
                    chordsList.Add(baseChordm.New(4));
                    chordsList.Add(baseChordm.New(1));
                    break;
                case (int)CHORDSTYPE.C005_SAKURA_DOKU:
                    chordsList.Add(baseChordm.New(1));
                    chordsList.Add(baseChordm.New(5));
                    chordsList.Add(baseChordm.New(6, false, true));
                    chordsList.Add(baseChordm.New(5));
                    chordsList.Add(baseChordm.New(4));
                    chordsList.Add(baseChordm.New(1, false, false , CHORDM.EXT.x, 3));
                    chordsList.Add(baseChordm.New(2, false, true, CHORDM.EXT.x7));
                    chordsList.Add(baseChordm.New(5, false, false, CHORDM.EXT.x7));
                    break;
                case (int)CHORDSTYPE.C006_SAKURA:
                    chordsList.Add(baseChordm.New(4));
                    chordsList.Add(baseChordm.New(5, false, false, CHORDM.EXT.x7));
                    chordsList.Add(baseChordm.New(1));
                    chordsList.Add(baseChordm.New(1));
                    chordsList.Add(baseChordm.New(4));
                    chordsList.Add(baseChordm.New(5, false, false, CHORDM.EXT.x7));
                    chordsList.Add(baseChordm.New(1));
                    chordsList.Add(baseChordm.New(1));
                    break;
                case (int)CHORDSTYPE.C007_KANASIMI_HA_YUKINOYOUNI:
                    chordsList.Add(baseChordm.New(1));
                    chordsList.Add(baseChordm.New(1));
                    chordsList.Add(baseChordm.New(4, false, true));
                    chordsList.Add(baseChordm.New(4, false, true));
                    chordsList.Add(baseChordm.New(2, false, true));
                    chordsList.Add(baseChordm.New(2, false, true));
                    chordsList.Add(baseChordm.New(4));
                    chordsList.Add(baseChordm.New(5, false, false, CHORDM.EXT.x7));
                    break;
                case (int)CHORDSTYPE.C008_GAKUEN_TENGOKU:
                    chordsList.Add(baseChordm.New(1));
                    chordsList.Add(baseChordm.New(5));
                    chordsList.Add(baseChordm.New(4));
                    chordsList.Add(baseChordm.New(1));
                    chordsList.Add(baseChordm.New(1));
                    chordsList.Add(baseChordm.New(5));
                    chordsList.Add(baseChordm.New(4));
                    chordsList.Add(baseChordm.New(1));
                    break;
                case (int)CHORDSTYPE.C009_ZANKOKU_NA_TENSHI_NO_TAZE:
                    chordsList.Add(baseChordm.New(1, false, true));
                    chordsList.Add(baseChordm.New(4, false, true));
                    chordsList.Add(baseChordm.New(6, true));
                    chordsList.Add(baseChordm.New(2, true));
                    chordsList.Add(baseChordm.New(1, false, true));
                    chordsList.Add(baseChordm.New(4, false, true));
                    chordsList.Add(baseChordm.New(5, true));
                    chordsList.Add(baseChordm.New(2, true));
                    break;
                case (int)CHORDSTYPE.C010_SAKURANBO:
                    chordsList.Add(baseChordm.New(1));
                    chordsList.Add(baseChordm.New(5));
                    chordsList.Add(baseChordm.New(6, false, true));
                    chordsList.Add(baseChordm.New(3, false, true));
                    chordsList.Add(baseChordm.New(4));
                    chordsList.Add(baseChordm.New(3, false, true));//
                    chordsList.Add(baseChordm.New(2, false, true));//
                    chordsList.Add(baseChordm.New(5));
                    break;
                case (int)CHORDSTYPE.C011_LET_IT_GO:
                    chordsList.Add(baseChordm.New(1));
                    chordsList.Add(baseChordm.New(5));
                    chordsList.Add(baseChordm.New(6, false, true));
                    chordsList.Add(baseChordm.New(4));
                    chordsList.Add(baseChordm.New(1));
                    chordsList.Add(baseChordm.New(5));
                    chordsList.Add(baseChordm.New(6, false, true));
                    chordsList.Add(baseChordm.New(4));
                    break;
                case (int)CHORDSTYPE.C012_WA_NI_NATTE_ODORO:
                    chordsList.Add(baseChordm.New(1, false, false, CHORDM.EXT.x, 3));
                    chordsList.Add(baseChordm.New(4));
                    chordsList.Add(baseChordm.New(5));
                    chordsList.Add(baseChordm.New(6, false, true));
                    chordsList.Add(baseChordm.New(1, false, false, CHORDM.EXT.x, 3));
                    chordsList.Add(baseChordm.New(4));
                    chordsList.Add(baseChordm.New(5));
                    chordsList.Add(baseChordm.New(6, false, true));
                    break;
                case (int)CHORDSTYPE.C013_DEPARTURES:
                    chordsList.Add(baseChordm.New(1, false, true));
                    chordsList.Add(baseChordm.New(5, true));
                    chordsList.Add(baseChordm.New(6, true));
                    chordsList.Add(baseChordm.New(1, false, true));
                    chordsList.Add(baseChordm.New(1, false, true));
                    chordsList.Add(baseChordm.New(5, true));
                    chordsList.Add(baseChordm.New(6, true));
                    chordsList.Add(baseChordm.New(1, false, true));
                    break;
                case (int)CHORDSTYPE.C014_TENTAI_KANSOKU:
                    chordsList.Add(baseChordm.New(1, false, false, CHORDM.EXT.x, 5));
                    chordsList.Add(baseChordm.New(1, false, false, CHORDM.EXT.x, 5));
                    chordsList.Add(baseChordm.New(6, false, true));
                    chordsList.Add(baseChordm.New(1, false, false, CHORDM.EXT.x, 3));
                    chordsList.Add(baseChordm.New(4));
                    chordsList.Add(baseChordm.New(4));
                    chordsList.Add(baseChordm.New(6, false, true));
                    chordsList.Add(baseChordm.New(5));
                    break;
                case (int)CHORDSTYPE.C015_KANDAGAWA:
                    chordsList.Add(baseChordm.New(1, false, true));
                    chordsList.Add(baseChordm.New(5, false, false, CHORDM.EXT.x7));
                    chordsList.Add(baseChordm.New(1, false, true));
                    chordsList.Add(baseChordm.New(1, false, true));
                    chordsList.Add(baseChordm.New(5, true));
                    chordsList.Add(baseChordm.New(6, true, false, CHORDM.EXT.x7));
                    chordsList.Add(baseChordm.New(2, true));
                    chordsList.Add(baseChordm.New(5, false, false, CHORDM.EXT.x7));
                    break;
                case (int)CHORDSTYPE.C017_CHERRY:
                    chordsList.Add(baseChordm.New(6, false, true));
                    chordsList.Add(baseChordm.New(3, false, true, CHORDM.EXT.x, 5));
                    chordsList.Add(baseChordm.New(4));
                    chordsList.Add(baseChordm.New(1));
                    chordsList.Add(baseChordm.New(6, false, true));
                    chordsList.Add(baseChordm.New(3, false, true));
                    chordsList.Add(baseChordm.New(4));
                    chordsList.Add(baseChordm.New(1));
                    break;
                case (int)CHORDSTYPE.C018_CANON:
                    chordsList.Add(baseChordm.New(1));
                    chordsList.Add(baseChordm.New(5));
                    chordsList.Add(baseChordm.New(6, false, true));
                    chordsList.Add(baseChordm.New(3, false, true));
                    chordsList.Add(baseChordm.New(4));
                    chordsList.Add(baseChordm.New(1));
                    chordsList.Add(baseChordm.New(4));
                    chordsList.Add(baseChordm.New(5));
                    break;
                case (int)CHORDSTYPE.C019_AI_HA_KATSU:
                    chordsList.Add(baseChordm.New(1));
                    chordsList.Add(baseChordm.New(5, false, false, CHORDM.EXT.x, 7));
                    chordsList.Add(baseChordm.New(6, false, true, CHORDM.EXT.x7));
                    chordsList.Add(baseChordm.New(3, false, true, CHORDM.EXT.x7));
                    chordsList.Add(baseChordm.New(4));
                    chordsList.Add(baseChordm.New(1, false, false, CHORDM.EXT.x, 3));
                    chordsList.Add(baseChordm.New(2, false, true, CHORDM.EXT.x7));
                    chordsList.Add(baseChordm.New(5));
                    break;
                case (int)CHORDSTYPE.C020_MIKADUKI:
                    chordsList.Add(baseChordm.New(6, false, true, CHORDM.EXT.x7));
                    chordsList.Add(baseChordm.New(6, false, true, CHORDM.EXT.x7));
                    chordsList.Add(baseChordm.New(1));
                    chordsList.Add(baseChordm.New(1));
                    chordsList.Add(baseChordm.New(2, false, true));
                    chordsList.Add(baseChordm.New(2, false, true));
                    chordsList.Add(baseChordm.New(5, false, false, CHORDM.EXT.sus4));
                    chordsList.Add(baseChordm.New(5));
                    break;
                case (int)CHORDSTYPE.C021_MAKENAIDE:
                    chordsList.Add(baseChordm.New(1));
                    chordsList.Add(baseChordm.New(6, false, true)); //
                    chordsList.Add(baseChordm.New(4));
                    chordsList.Add(baseChordm.New(2, false, true, CHORDM.EXT.x7));
                    chordsList.Add(baseChordm.New(1));
                    chordsList.Add(baseChordm.New(6, false, true));
                    chordsList.Add(baseChordm.New(4));
                    chordsList.Add(baseChordm.New(2, false, true, CHORDM.EXT.x7));//
                    break;
                case (int)CHORDSTYPE.C022_KOKORONO_TABI:
                    chordsList.Add(baseChordm.New(1));
                    chordsList.Add(baseChordm.New(1, false, false, CHORDM.EXT.M7));
                    chordsList.Add(baseChordm.New(6, false, true)); //
                    chordsList.Add(baseChordm.New(3, false, true, CHORDM.EXT.x7));  //
                    chordsList.Add(baseChordm.New(4));
                    chordsList.Add(baseChordm.New(5, false, false, CHORDM.EXT.x7));//
                    chordsList.Add(baseChordm.New(1));
                    chordsList.Add(baseChordm.New(5, false, false, CHORDM.EXT.x7));
                    break;
                case (int)CHORDSTYPE.C023_KAZE_NI_NARITAI:
                    chordsList.Add(baseChordm.New(1));
                    chordsList.Add(baseChordm.New(3, false, true, CHORDM.EXT.x7));//
                    chordsList.Add(baseChordm.New(5, false, true, CHORDM.EXT.x7));//
                    chordsList.Add(baseChordm.New(4, false, false, CHORDM.EXT.M7));//
                    chordsList.Add(baseChordm.New(2, false, true, CHORDM.EXT.x7));//
                    chordsList.Add(baseChordm.New(3, false, true, CHORDM.EXT.x7));//
                    chordsList.Add(baseChordm.New(4, false, true, CHORDM.EXT.x7));//
                    chordsList.Add(baseChordm.New(5, false, false, CHORDM.EXT.x7));
                    break;
                case (int)CHORDSTYPE.C024_KOKODE_KISUSHITE:
                    chordsList.Add(baseChordm.New(1));
                    chordsList.Add(baseChordm.New(1));
                    chordsList.Add(baseChordm.New(6, true));
                    chordsList.Add(baseChordm.New(6, true));
                    chordsList.Add(baseChordm.New(1));
                    chordsList.Add(baseChordm.New(1));
                    chordsList.Add(baseChordm.New(6, true));
                    chordsList.Add(baseChordm.New(6, true));
                    break;
                case (int)CHORDSTYPE.C025_GEKKO:
                    chordsList.Add(baseChordm.New(6, false, true));
                    chordsList.Add(baseChordm.New(3, false, true, CHORDM.EXT.x7));
                    chordsList.Add(baseChordm.New(4));
                    chordsList.Add(baseChordm.New(1));
                    chordsList.Add(baseChordm.New(6, false, true));
                    chordsList.Add(baseChordm.New(5));
                    chordsList.Add(baseChordm.New(4));
                    chordsList.Add(baseChordm.New(4));
                    break;
                case (int)CHORDSTYPE.C026_WINTER_AGAIN:
                    chordsList.Add(baseChordm.New(5, true));
                    chordsList.Add(baseChordm.New(6, true));
                    chordsList.Add(baseChordm.New(5, true));
                    chordsList.Add(baseChordm.New(6, true));
                    chordsList.Add(baseChordm.New(5, true));
                    chordsList.Add(baseChordm.New(5, false, false, CHORDM.EXT.x, 7));
                    chordsList.Add(baseChordm.New(5, true));
                    chordsList.Add(baseChordm.New(1, false, true));//
                    break;
                case (int)CHORDSTYPE.C030_AOI_INAZUMA:
                    chordsList.Add(baseChordm.New(4, CHORDM.EXT.M7));
                    chordsList.Add(baseChordm.New(4, CHORDM.EXT.M7));
                    chordsList.Add(baseChordm.New(3, CHORDM.EXT.sus4));
                    chordsList.Add(baseChordm.New(3, CHORDM.EXT.x7));
                    chordsList.Add(baseChordm.New(6, false, true));
                    chordsList.Add(baseChordm.New(6, false, true));
                    chordsList.Add(baseChordm.New(1, CHORDM.EXT.sus4));
                    chordsList.Add(baseChordm.New(1, CHORDM.EXT.x7));
                    break;
                case (int)CHORDSTYPE.C031_FUTARI_DE_OTYA_WO:
                    chordsList.Add(baseChordm.New(2, false, true, CHORDM.EXT.x7));
                    chordsList.Add(baseChordm.New(2, false, true, CHORDM.EXT.x7));
                    chordsList.Add(baseChordm.New(1, CHORDM.EXT.M7));
                    chordsList.Add(baseChordm.New(1, false, false, CHORDM.EXT.x7));
                    chordsList.Add(baseChordm.New(2, false, true, CHORDM.EXT.x7));
                    chordsList.Add(baseChordm.New(2, false, true, CHORDM.EXT.x7));
                    chordsList.Add(baseChordm.New(1));
                    chordsList.Add(baseChordm.New(1));
                    break;
                case (int)CHORDSTYPE.C033_TETUWAN_ATOM:
                    chordsList.Add(baseChordm.New(1));
                    chordsList.Add(baseChordm.New(1));
                    chordsList.Add(baseChordm.New(4));
                    chordsList.Add(baseChordm.New(1));
                    chordsList.Add(baseChordm.New(5));
                    chordsList.Add(baseChordm.New(1));
                    chordsList.Add(baseChordm.New(2, CHORDM.EXT.x7));
                    chordsList.Add(baseChordm.New(5, CHORDM.EXT.x7));
                    break;
                case (int)CHORDSTYPE.C034_OYOGE_TAIYAKI_KUN:
                    chordsList.Add(baseChordm.New(1, false, true));
                    chordsList.Add(baseChordm.New(2, true));
                    chordsList.Add(baseChordm.New(1, false, true));
                    chordsList.Add(baseChordm.New(2, true));
                    chordsList.Add(baseChordm.New(4, false, true));
                    chordsList.Add(baseChordm.New(1, false, true));
                    chordsList.Add(baseChordm.New(2, CHORDM.EXT.x7));
                    chordsList.Add(baseChordm.New(1, false, true));
                    break;
                case (int)CHORDSTYPE.C035_AMAIRO_NO_KAMINO_OTOME:
                    chordsList.Add(baseChordm.New(1));
                    chordsList.Add(baseChordm.New(5));
                    chordsList.Add(baseChordm.New(6, false, true));
                    chordsList.Add(baseChordm.New(3, false, true));
                    chordsList.Add(baseChordm.New(4));
                    chordsList.Add(baseChordm.New(1));
                    chordsList.Add(baseChordm.New(2, CHORDM.EXT.x7));
                    chordsList.Add(baseChordm.New(5, CHORDM.EXT.x7));
                    break;
                case (int)CHORDSTYPE.C036_TSUBOMI:
                    chordsList.Add(baseChordm.New(4, CHORDM.EXT.M7));
                    chordsList.Add(baseChordm.New(5));
                    chordsList.Add(baseChordm.New(6, false, true));
                    chordsList.Add(baseChordm.New(3, false, true));
                    chordsList.Add(baseChordm.New(4));
                    chordsList.Add(baseChordm.New(5, CHORDM.EXT.x7));
                    chordsList.Add(baseChordm.New(1));
                    chordsList.Add(baseChordm.New(1, CHORDM.EXT.x7));
                    break;
                case (int)CHORDSTYPE.C037_NADA_SOUSOU:
                    chordsList.Add(baseChordm.New(1));
                    chordsList.Add(baseChordm.New(5, 7));
                    chordsList.Add(baseChordm.New(4, 6));
                    chordsList.Add(baseChordm.New(1, 5));
                    chordsList.Add(baseChordm.New(4));
                    chordsList.Add(baseChordm.New(1, true, false, CHORDM.EXT.dim)); //
                    chordsList.Add(baseChordm.New(2, false, true, CHORDM.EXT.x7));  //
                    chordsList.Add(baseChordm.New(5, CHORDM.EXT.x7));
                    break;
                case (int)CHORDSTYPE.C038_HITACHI_NO_KI:
                    chordsList.Add(baseChordm.New(1));
                    chordsList.Add(baseChordm.New(4));
                    chordsList.Add(baseChordm.New(5, CHORDM.EXT.x7));
                    chordsList.Add(baseChordm.New(1));
                    chordsList.Add(baseChordm.New(1, CHORDM.EXT.x7));
                    chordsList.Add(baseChordm.New(4));
                    chordsList.Add(baseChordm.New(5, CHORDM.EXT.x7));
                    chordsList.Add(baseChordm.New(1));
                    break;
                case (int)CHORDSTYPE.C039_GREEN_SLEEVES:
                    chordsList.Add(baseChordm.New(1, false, true));
                    chordsList.Add(baseChordm.New(1, false, true));
                    chordsList.Add(baseChordm.New(6, true));
                    chordsList.Add(baseChordm.New(5, false, true));
                    chordsList.Add(baseChordm.New(1, false, true));
                    chordsList.Add(baseChordm.New(2, CHORDM.EXT.x7));
                    chordsList.Add(baseChordm.New(5, CHORDM.EXT.x7));
                    chordsList.Add(baseChordm.New(5, CHORDM.EXT.x7));
                    break;
                case (int)CHORDSTYPE.C040_AKAI_KAWA_NO_TANIMA:
                    chordsList.Add(baseChordm.New(1));
                    chordsList.Add(baseChordm.New(1, CHORDM.EXT.x7));
                    chordsList.Add(baseChordm.New(4));
                    chordsList.Add(baseChordm.New(4));
                    chordsList.Add(baseChordm.New(1));
                    chordsList.Add(baseChordm.New(5, CHORDM.EXT.x7));
                    chordsList.Add(baseChordm.New(1));
                    chordsList.Add(baseChordm.New(1));
                    break;
                case (int)CHORDSTYPE.C041_GEGEGE_NO_KITAROU:
                    chordsList.Add(baseChordm.New(1, false, true));
                    chordsList.Add(baseChordm.New(5, true, false, CHORDM.EXT.x7));
                    chordsList.Add(baseChordm.New(5, CHORDM.EXT.x7));
                    chordsList.Add(baseChordm.New(5, true, false, CHORDM.EXT.x7));
                    chordsList.Add(baseChordm.New(1, false, true));
                    chordsList.Add(baseChordm.New(4, false, true));
                    chordsList.Add(baseChordm.New(5, CHORDM.EXT.x7));
                    chordsList.Add(baseChordm.New(5, CHORDM.EXT.x7));
                    break;
                case (int)CHORDSTYPE.C042_UCHUSENKAN_YAMATO:
                    chordsList.Add(baseChordm.New(1, false, true));
                    chordsList.Add(baseChordm.New(4));
                    chordsList.Add(baseChordm.New(1, false, true));
                    chordsList.Add(baseChordm.New(2, true));
                    chordsList.Add(baseChordm.New(5, true));
                    chordsList.Add(baseChordm.New(5));
                    chordsList.Add(baseChordm.New(6, true));
                    chordsList.Add(baseChordm.New(1, false, true));
                    break;
                case (int)CHORDSTYPE.C043_TENTOU_MUSHI_NO_SAMBA:
                    chordsList.Add(baseChordm.New(1));
                    chordsList.Add(baseChordm.New(3, false, true));
                    chordsList.Add(baseChordm.New(4));
                    chordsList.Add(baseChordm.New(5, CHORDM.EXT.x7));
                    chordsList.Add(baseChordm.New(4));
                    chordsList.Add(baseChordm.New(1));
                    chordsList.Add(baseChordm.New(2, CHORDM.EXT.x7));
                    chordsList.Add(baseChordm.New(5, CHORDM.EXT.x7));
                    break;
                case (int)CHORDSTYPE.C044_SYONEN_JIDAI:
                    chordsList.Add(baseChordm.New(1));
                    chordsList.Add(baseChordm.New(5));
                    chordsList.Add(baseChordm.New(3, CHORDM.EXT.x7));
                    chordsList.Add(baseChordm.New(6, false, true));
                    chordsList.Add(baseChordm.New(4));
                    chordsList.Add(baseChordm.New(1, 3));
                    chordsList.Add(baseChordm.New(2, false, true, CHORDM.EXT.x7));
                    chordsList.Add(baseChordm.New(5, CHORDM.EXT.x7));
                    break;
                case (int)CHORDSTYPE.C045_IHOJIN:
                    chordsList.Add(baseChordm.New(1, false, true));
                    chordsList.Add(baseChordm.New(4, false, true, CHORDM.EXT.x7));
                    chordsList.Add(baseChordm.New(6, true, false, CHORDM.EXT.x7));
                    chordsList.Add(baseChordm.New(2, true));
                    chordsList.Add(baseChordm.New(5, CHORDM.EXT.x7));
                    chordsList.Add(baseChordm.New(1, false, true));
                    chordsList.Add(baseChordm.New(4, false, true, CHORDM.EXT.x6));
                    chordsList.Add(baseChordm.New(5, CHORDM.EXT.x7));
                    break;
                case (int)CHORDSTYPE.C046_YELL:
                    chordsList.Add(baseChordm.New(6, false, true));
                    chordsList.Add(baseChordm.New(2, false, true));
                    chordsList.Add(baseChordm.New(5));
                    chordsList.Add(baseChordm.New(5, 7));
                    chordsList.Add(baseChordm.New(6, false, true));
                    chordsList.Add(baseChordm.New(2, false, true));
                    chordsList.Add(baseChordm.New(5));
                    chordsList.Add(baseChordm.New(3, CHORDM.EXT.sus4));
                    break;
                case (int)CHORDSTYPE.C048_NAMIDAGA_KIRARI:
                    chordsList.Add(baseChordm.New(1, CHORDM.EXT.sus4));
                    chordsList.Add(baseChordm.New(1));
                    chordsList.Add(baseChordm.New(1, CHORDM.EXT.aug));
                    chordsList.Add(baseChordm.New(1));
                    chordsList.Add(baseChordm.New(1, CHORDM.EXT.sus4));
                    chordsList.Add(baseChordm.New(1));
                    chordsList.Add(baseChordm.New(6, true));
                    chordsList.Add(baseChordm.New(5));
                    break;


                case (int)CHORDSTYPE.C049_ZURUI_ONNA:
                    chordsList.Add(baseChordm.New(1, false, true));
                    chordsList.Add(baseChordm.New(6, true));
                    chordsList.Add(baseChordm.New(5, false, true));
                    chordsList.Add(baseChordm.New(1, false, true));
                    chordsList.Add(baseChordm.New(5, true));
                    chordsList.Add(baseChordm.New(5, false, true));
                    chordsList.Add(baseChordm.New(2, false, true, CHORDM.EXT.dim)); //
                    chordsList.Add(baseChordm.New(5, false, false, CHORDM.EXT.x7));
                    break;
                case (int)CHORDSTYPE.C051_MANATSU_NO_KAJITSU:
                    chordsList.Add(baseChordm.New(1));
                    chordsList.Add(baseChordm.New(1, false, false ,CHORDM.EXT.M7));
                    chordsList.Add(baseChordm.New(6, false, true, CHORDM.EXT.x7));
                    chordsList.Add(baseChordm.New(1, false, false, CHORDM.EXT.x7));//
                    chordsList.Add(baseChordm.New(4, false, false, CHORDM.EXT.M7));
                    chordsList.Add(baseChordm.New(3, false, false, CHORDM.EXT.x7));//
                    chordsList.Add(baseChordm.New(2, false, false, CHORDM.EXT.x7));//
                    chordsList.Add(baseChordm.New(5, false, false, CHORDM.EXT.x7));
                    break;
                case (int)CHORDSTYPE.C052_IKUZE_KAITOUSHOJO:
                    chordsList.Add(baseChordm.New(2, false, true));
                    chordsList.Add(baseChordm.New(3, false, true, CHORDM.EXT.x7));
                    chordsList.Add(baseChordm.New(7, false, true, CHORDM.EXT.dim));
                    chordsList.Add(baseChordm.New(6, false, true));
                    chordsList.Add(baseChordm.New(2, false, true));
                    chordsList.Add(baseChordm.New(3, false, true));
                    chordsList.Add(baseChordm.New(2, false, true));
                    chordsList.Add(baseChordm.New(1));
                    break;
                case (int)CHORDSTYPE.C053_MANATSUNO_YORUNO_YUME:
                    chordsList.Add(baseChordm.New(6, false, true));
                    chordsList.Add(baseChordm.New(4));
                    chordsList.Add(baseChordm.New(5));
                    chordsList.Add(baseChordm.New(1));
                    chordsList.Add(baseChordm.New(4));
                    chordsList.Add(baseChordm.New(1));
                    chordsList.Add(baseChordm.New(2, true, false, CHORDM.EXT.dim));
                    chordsList.Add(baseChordm.New(3, CHORDM.EXT.sus4));
                    break;
                case (int)CHORDSTYPE.C054_I_GOT_RHYTHM:
                    chordsList.Add(baseChordm.New(1));
                    chordsList.Add(baseChordm.New(2, false, true, CHORDM.EXT.x7));
                    chordsList.Add(baseChordm.New(1, CHORDM.EXT.x6));
                    chordsList.Add(baseChordm.New(2, false, true, CHORDM.EXT.x7));
                    chordsList.Add(baseChordm.New(1));
                    chordsList.Add(baseChordm.New(2, false, true, CHORDM.EXT.x7));
                    chordsList.Add(baseChordm.New(1, CHORDM.EXT.x6));
                    chordsList.Add(baseChordm.New(5, CHORDM.EXT.x7));
                    break;

                
                case (int)CHORDSTYPE.C070_KIMI_WO_NOSETE:
                    chordsList.Add(baseChordm.New(1, false, true));
                    chordsList.Add(baseChordm.New(6, true, false, CHORDM.EXT.x6));
                    chordsList.Add(baseChordm.New(5, true, false, CHORDM.EXT.M7));
                    chordsList.Add(baseChordm.New(2, true, false, CHORDM.EXT.x, 5));
                    chordsList.Add(baseChordm.New(4, false, true, CHORDM.EXT.x7));
                    chordsList.Add(baseChordm.New(1, false, true, CHORDM.EXT.x, 3));
                    chordsList.Add(baseChordm.New(2, false, false, CHORDM.EXT.x));
                    chordsList.Add(baseChordm.New(5, false, false, CHORDM.EXT.x7));
                    break;
                case (int)CHORDSTYPE.C072_TSUBASA_WO_KUDASAI:
                    chordsList.Add(baseChordm.New(1));
                    chordsList.Add(baseChordm.New(5, false, false, CHORDM.EXT.x, 7));
                    chordsList.Add(baseChordm.New(6, false, true));
                    chordsList.Add(baseChordm.New(3, false, true, CHORDM.EXT.x, 5));
                    chordsList.Add(baseChordm.New(4, false, false));
                    chordsList.Add(baseChordm.New(1, false, false, CHORDM.EXT.x, 3));
                    chordsList.Add(baseChordm.New(6, true, false));
                    chordsList.Add(baseChordm.New(5, false, false));
                    break;
                case (int)CHORDSTYPE.C123_ALONE_AGAIN:
                    chordsList.Add(baseChordm.New(1, false, false, CHORDM.EXT.M7));
                    chordsList.Add(baseChordm.New(3, false, true));
                    chordsList.Add(baseChordm.New(5, false, true, CHORDM.EXT.x7));
                    chordsList.Add(baseChordm.New(3, false, true, CHORDM.EXT.x7));//
                    chordsList.Add(baseChordm.New(2, false, true, CHORDM.EXT.x7));//
                    chordsList.Add(baseChordm.New(2, false, true, CHORDM.EXT.x7));//
                    chordsList.Add(baseChordm.New(1));
                    chordsList.Add(baseChordm.New(1, false, false, CHORDM.EXT.x6));//
                    break;
                case (int)CHORDSTYPE.C125_WAVE:
                    chordsList.Add(baseChordm.New(1, false, false, CHORDM.EXT.M7));
                    chordsList.Add(baseChordm.New(5, true, false, CHORDM.EXT.dim));//
                    chordsList.Add(baseChordm.New(5, false, true, CHORDM.EXT.x7));
                    chordsList.Add(baseChordm.New(1, false, false, CHORDM.EXT.x7));//
                    chordsList.Add(baseChordm.New(4, false, false, CHORDM.EXT.M7));
                    chordsList.Add(baseChordm.New(4, false, true));
                    chordsList.Add(baseChordm.New(3, false, true, CHORDM.EXT.x7));//
                    chordsList.Add(baseChordm.New(6, false, false, CHORDM.EXT.x7));//
                    break;
            }
        }

        public static List<AdlivMusic.LENGTH> GetRandomLength(int minDiv)
        {
            List<AdlivMusic.LENGTH> lengthes = new List<AdlivMusic.LENGTH>();

            lengthes.Add(LENGTH.N1);

            while (lengthes.Count() < minDiv)
            {
                int targetIndex = r.Next(lengthes.Count);
                int l = (int)lengthes[targetIndex];
                int l1, l2;
                if (r.Next(100) < 12)
                {
                    l1 = (l * 3) / 4;
                    l2 = l / 4;
                }
                else if (r.Next(100) < 12)
                {
                    l1 = l / 4;
                    l2 = (l * 3) / 4;
                }
                else
                {
                    l1 = l / 2;
                    l2 = l / 2;
                }

                lengthes.RemoveAt(targetIndex);
                lengthes.Insert(targetIndex, (LENGTH)l2);
                lengthes.Insert(targetIndex, (LENGTH)l1);   
            }

            return lengthes;
        }

        public static string SayAh()
        {
            string[] ah = new string[] {"a", "sa", "na", "ma", "ra", "wa", "sya", "tya", "hya", "fa", 
                                        "e", "se", "ne", "me", "re",       "she", "the", "hhe", "fe"};

            //return ah[r.Next(ah.Count())];
            return AdlivMusic.VOICE.GetVoice();
        }


        public static void Test(AdlivMidi midi, AdlivVsq4 vsq4)
        {
            AdlivVsq4.vsTrack[] vTrack = new AdlivVsq4.vsTrack[5];
            vTrack[0] = vsq4.CreateTrack();
            vTrack[1] = vsq4.CreateTrack();
            vTrack[2] = vsq4.CreateTrack();

            AdlivMidi.MidiTrack[] mTrack = new AdlivMidi.MidiTrack[16];
            mTrack[0] = midi.CreateTrack(AdlivMidi.TONE.T001_PIANO_ACOUSTIC_PIANO);
            mTrack[1] = midi.CreateTrack(AdlivMidi.TONE.T001_PIANO_ACOUSTIC_PIANO);
            mTrack[2] = midi.CreateTrack(AdlivMidi.TONE.T001_PIANO_ACOUSTIC_PIANO);
            mTrack[3] = midi.CreateTrack(AdlivMidi.TONE.T041_STRINGS_VIOLIN);

            //  空白期間
            vTrack[0].AddRest(LENGTH.N1);
            vTrack[1].AddRest(LENGTH.N1);
            vTrack[2].AddRest(LENGTH.N1);
            mTrack[0].AddRest(LENGTH.N1);
            mTrack[1].AddRest(LENGTH.N1);
            mTrack[2].AddRest(LENGTH.N1);
            mTrack[3].AddRest(LENGTH.N1);

            List<CHORD> chords = new List<CHORD>();
            chords.Add(new CHORD(CHORD.NAME.C));
            chords.Add(new CHORD(CHORD.NAME.D));
            chords.Add(new CHORD(CHORD.NAME.E));
            chords.Add(new CHORD(CHORD.NAME.F));
            chords.Add(new CHORD(CHORD.NAME.G));
            chords.Add(new CHORD(CHORD.NAME.A));
            chords.Add(new CHORD(CHORD.NAME.B));
            chords.Add(new CHORD(CHORD.NAME.C));

            foreach (CHORD chord in chords)
            {
                NOTE.TONE[] scaleTones = chord.GetScaleTones();
                for (int i = 0; i < 4; i++)
                {
                    mTrack[0].AddNote(LENGTH.N2, chord.GetNote(0, 0), 64);
                    mTrack[1].AddNote(LENGTH.N2, chord.GetNote(0, 1), 64);
                    mTrack[2].AddNote(LENGTH.N2, chord.GetNote(0, 2), 64);
                }

                mTrack[3].AddNote(LENGTH.N2, chord.GetNote(0, 0));
                mTrack[3].AddNote(LENGTH.N2, chord.GetNote(0, 2));
                mTrack[3].AddNote(LENGTH.N1, chord.GetNote(0, 0));

                for (int i = 0; i < 4; i++)
                {
                    vTrack[0].AddNote(LENGTH.N8, scaleTones[i], "ra");
                }
                for (int i = 4; i > 0; i--)
                {
                    vTrack[0].AddNote(LENGTH.N8, scaleTones[i], "ra");
                }
                vTrack[0].AddNote(LENGTH.N1, scaleTones[0], "ra");
            }
        }

        public static void Test2(AdlivMidi midi, AdlivVsq4 vsq4)
        {
            AdlivVsq4.vsTrack[] vTrack = new AdlivVsq4.vsTrack[5];
            vTrack[0] = vsq4.CreateTrack();
            vTrack[1] = vsq4.CreateTrack();
            vTrack[2] = vsq4.CreateTrack();

            AdlivMidi.MidiTrack[] mTrack = new AdlivMidi.MidiTrack[16];
            mTrack[0] = midi.CreateTrack(AdlivMidi.TONE.T001_PIANO_ACOUSTIC_PIANO);
            mTrack[1] = midi.CreateTrack(AdlivMidi.TONE.T001_PIANO_ACOUSTIC_PIANO);
            mTrack[2] = midi.CreateTrack(AdlivMidi.TONE.T001_PIANO_ACOUSTIC_PIANO);
            mTrack[3] = midi.CreateTrack(AdlivMidi.TONE.T041_STRINGS_VIOLIN);

            //  空白期間
            vTrack[0].AddRest(LENGTH.N1);
            vTrack[1].AddRest(LENGTH.N1);
            vTrack[2].AddRest(LENGTH.N1);
            mTrack[0].AddRest(LENGTH.N1);
            mTrack[1].AddRest(LENGTH.N1);
            mTrack[2].AddRest(LENGTH.N1);
            mTrack[3].AddRest(LENGTH.N1);

            CHORDM chordm = new CHORDM(CHORDM.KEY.C);
            chordm.SetAnchor(NOTE.TONE.C5, NOTE.TONE.C4);
            List<CHORDM> chords = new List<CHORDM>();

            chords.Add(chordm.New(1));
            chords.Add(chordm.New(5, false, false, CHORDM.EXT.x, 7));
            chords.Add(chordm.New(6, false, true));
            chords.Add(chordm.New(3, false, true, CHORDM.EXT.x, 5));
            chords.Add(chordm.New(4));
            chords.Add(chordm.New(1, false, false, CHORDM.EXT.x, 3));
            chords.Add(chordm.New(4));
            chords.Add(chordm.New(5));

            foreach (CHORDM chord in chords)
            {
                NOTE.TONE[] chordTones = chord.GetChordTones();
                for (int i = 0; i < 4; i++)
                {
                    mTrack[0].AddNote(LENGTH.N2, chordTones[0], 64);
                    mTrack[1].AddNote(LENGTH.N2, chordTones[1], 64);
                    mTrack[2].AddNote(LENGTH.N2, chordTones[2], 64);
                }

                NOTE.TONE[] scaleTones = chord.GetScaleTones();
                for (int i = 0; i < 4; i++)
                {
                    vTrack[0].AddNote(LENGTH.N8, scaleTones[i], "ra");
                }
                for (int i = 4; i > 0; i--)
                {
                    vTrack[0].AddNote(LENGTH.N8, scaleTones[i], "ra");
                }
                vTrack[0].AddNote(LENGTH.N1, scaleTones[0], "ra");

                NOTE.TONE baseTones = chord.GetOnTone();
                vTrack[1].AddNote(LENGTH.N1x2, baseTones, "a");

            }

        }
        */
    }
}
