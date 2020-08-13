using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdlivMusic;

namespace AdlivMusic
{
    public enum CHORD
    {
        C, C7,
        D7, Dm, Dm7,
        E, E7, Em, Em7,
        F, FM7, Fm,
        G, Gsus4, G7,
        A, Am, Am7,
    }

    public class AdlivMusicPlus
    {
        private static Random r = new Random();

        public static string[] voices = new string[] { "a", "ka", "sa", "ta", "na", "ha", "ma", "ya",  "ra", "wa", "ga", "ba", "za", "da", "pa",
                                             "i", "ki", "si", "ti", "ni", "hi", "mi",        "ri",       "gi", "bi", "zi", "di", "pi",
                                             "u", "ku", "su", "tu", "nu", "hu", "mu", "yu",  "ru",       "gu", "bu", "zu", "du", "pu",
                                             "e", "ke", "se", "te", "ne", "he", "me",        "re",       "ge", "be", "ze", "de", "pe",
                                             "o", "ko", "so", "to", "no", "ho", "mo", "yo",  "ro", "wo", "go", "bo", "zo", "do", "po",
                                             "n",
            };
        //  Chord 001
        public static CHORD[] CHORD001_Basic = new CHORD[]{
                CHORD.C, CHORD.C, CHORD.C, CHORD.C,
                CHORD.Dm, CHORD.Dm, CHORD.Dm, CHORD.Dm,
                CHORD.Em, CHORD.Em, CHORD.Dm, CHORD.Dm,
                CHORD.G, CHORD.G,CHORD.C, CHORD.C7,
            };

        //  Chord 003
        public static CHORD[] CHORD003_Basic = new CHORD[]{
                CHORD.C, CHORD.C, CHORD.C, CHORD.C,
                CHORD.F, CHORD.F, CHORD.C, CHORD.G,
                CHORD.C, CHORD.C, CHORD.E, CHORD.Am,
                CHORD.Dm, CHORD.G,CHORD.C, CHORD.C,
            };

        //  Chord 004
        public static CHORD[] CHORD004_Basic = new CHORD[]{
                CHORD.C, CHORD.C, CHORD.G, CHORD.G,
                CHORD.Am, CHORD.Am, CHORD.F, CHORD.F,
                CHORD.C, CHORD.C, CHORD.G, CHORD.G,
                CHORD.F, CHORD.F ,CHORD.C, CHORD.C,
            };

        //  Chord 005
        public static CHORD[] CHORD005_Basic = new CHORD[]{
                CHORD.C, CHORD.G, CHORD.Am, CHORD.G,
                CHORD.F, CHORD.C, CHORD.Dm7, CHORD.G7,
                CHORD.C, CHORD.G, CHORD.Am, CHORD.G,
                CHORD.F, CHORD.C, CHORD.Dm7, CHORD.G7,
            };

        //  Chord 006
        public static CHORD[] CHORD006_Basic = new CHORD[]{
                CHORD.F, CHORD.G7, CHORD.C, CHORD.C,
                CHORD.F, CHORD.G7, CHORD.C, CHORD.C,
                CHORD.F, CHORD.G7, CHORD.C, CHORD.C,
                CHORD.F, CHORD.G7, CHORD.C, CHORD.C,
            };

        //  Chord 007
        public static CHORD[] CHORD007_Basic = new CHORD[]{
                CHORD.C, CHORD.C, CHORD.C, CHORD.C,
                CHORD.Am, CHORD.Am, CHORD.Am, CHORD.Am,
                CHORD.Dm, CHORD.Dm, CHORD.Dm, CHORD.Dm,
                CHORD.F, CHORD.F, CHORD.G7, CHORD.G7,
            };

        //  Chord 008
        public static CHORD[] CHORD008_Basic = new CHORD[]{
                CHORD.C, CHORD.C, CHORD.G, CHORD.G,
                CHORD.F, CHORD.F, CHORD.C, CHORD.C,
                CHORD.C, CHORD.C, CHORD.G, CHORD.G,
                CHORD.F, CHORD.F, CHORD.C, CHORD.C,
            };

        //  Chord 009
        public static CHORD[] CHORD009_Basic = new CHORD[]{
                CHORD.Am, CHORD.Dm, CHORD.G, CHORD.C,
                CHORD.Am, CHORD.Dm, CHORD.G, CHORD.C,
                CHORD.Am, CHORD.Dm, CHORD.G, CHORD.C,
                CHORD.Am, CHORD.Dm, CHORD.G, CHORD.C,
            };

        //  Chord 010
        public static CHORD[] CHORD010_Basic = new CHORD[]{
                CHORD.C, CHORD.C, CHORD.G, CHORD.G,
                CHORD.Am, CHORD.Am, CHORD.Em, CHORD.Em,
                CHORD.F, CHORD.G, CHORD.Em, CHORD.Am,
                CHORD.Dm, CHORD.Dm, CHORD.G, CHORD.G,
            };

        //  Chord 018
        public static CHORD[] CHORD018_Canon = new CHORD[]{
                CHORD.C, CHORD.G, CHORD.Am, CHORD.Em,
                CHORD.F, CHORD.C, CHORD.F, CHORD.G,
                CHORD.C, CHORD.G, CHORD.Am, CHORD.Em,
                CHORD.F, CHORD.C, CHORD.F, CHORD.G,
            };

        //  Chord 021
        public static CHORD[] CHORD021_Canon = new CHORD[]{
                CHORD.C, CHORD.G, CHORD.Am, CHORD.Em7,
                CHORD.F, CHORD.C, CHORD.Dm7, CHORD.G7,
                CHORD.C, CHORD.G, CHORD.Am, CHORD.Em7,
                CHORD.F, CHORD.C, CHORD.Dm7, CHORD.E7,
            };

        //  Chord 026
        public static CHORD[] CHORD026_Basic = new CHORD[]{
                CHORD.F, CHORD.G, CHORD.G, CHORD.Am,
                CHORD.F, CHORD.G, CHORD.G, CHORD.Am,
                CHORD.F, CHORD.G, CHORD.E, CHORD.Am,
                CHORD.F, CHORD.G, CHORD.Am, CHORD.Am,
            };

        //  Chord 033
        public static CHORD[] CHORD033_Basic = new CHORD[]{
                CHORD.C, CHORD.C, CHORD.C, CHORD.C,
                CHORD.F, CHORD.F, CHORD.C, CHORD.C,
                CHORD.G, CHORD.G, CHORD.C, CHORD.C,
                CHORD.D7, CHORD.D7, CHORD.G7, CHORD.G7,
            };

        //  Chord 035
        public static CHORD[] CHORD035_Basic = new CHORD[]{
                CHORD.C, CHORD.C, CHORD.G, CHORD.G,
                CHORD.Am, CHORD.Am, CHORD.Em, CHORD.Em,
                CHORD.F, CHORD.Fm, CHORD.C, CHORD.A,
                CHORD.D7, CHORD.D7, CHORD.G7, CHORD.G7,
            };

        //  Chord 059
        public static CHORD[] CHORD059_Basic = new CHORD[]{
                CHORD.C, CHORD.C, CHORD.E7, CHORD.E7,
                CHORD.F, CHORD.F, CHORD.C, CHORD.G,
                CHORD.Am, CHORD.Am, CHORD.D7, CHORD.D7,
                CHORD.Gsus4, CHORD.Gsus4, CHORD.G, CHORD.G,
            };

        //  Chord 0101
        public static CHORD[] CHORD101_Basic = new CHORD[]{
                CHORD.Dm7, CHORD.Dm7, CHORD.Dm7, CHORD.Dm7,
                CHORD.Am7, CHORD.Am7, CHORD.Am7, CHORD.Am7,
                CHORD.FM7, CHORD.FM7, CHORD.C, CHORD.C,
                CHORD.Dm7, CHORD.Dm7, CHORD.E7, CHORD.E7,
            };

        public static NOTE GetChord(CHORD chord, int Oct = 0, int index = -1)
        {
            NOTE result = NOTE.x; ;

            if (index == -1)
            {
                index = r.Next(4);
            }
            switch (chord)
            {
                case CHORD.C:
                    result = new NOTE[] { NOTE.G2, NOTE.C3, NOTE.E3, NOTE.E2 }[index];
                    break;
                case CHORD.C7:
                    result = new NOTE[] { NOTE.G2, NOTE.C3, NOTE.E3, NOTE.As2 }[index];
                    break;
                case CHORD.D7:
                    result = new NOTE[] { NOTE.A2, NOTE.D3, NOTE.Fs3, NOTE.C3 }[index];
                    break;
                case CHORD.Dm:
                    result = new NOTE[] { NOTE.A2, NOTE.D3, NOTE.F3, NOTE.A2 }[index];
                    break;
                case CHORD.Dm7:
                    result = new NOTE[] { NOTE.A2, NOTE.D3, NOTE.F3, NOTE.C3 }[index];
                    break;
                case CHORD.E:
                    result = new NOTE[] { NOTE.Gs2, NOTE.B2, NOTE.E3, NOTE.Gs2 }[index];
                    break;
                case CHORD.E7:
                    result = new NOTE[] { NOTE.Gs2, NOTE.B2, NOTE.E3, NOTE.D3 }[index];
                    break;
                case CHORD.Em:
                    result = new NOTE[] { NOTE.G2, NOTE.B2, NOTE.E3, NOTE.G3 }[index];
                    break;
                case CHORD.Em7:
                    result = new NOTE[] { NOTE.G2, NOTE.B2, NOTE.E3, NOTE.D3 }[index];
                    break;
                case CHORD.F:
                    result = new NOTE[] { NOTE.A2, NOTE.C3, NOTE.F3, NOTE.A2 }[index];
                    break;
                case CHORD.FM7:
                    result = new NOTE[] { NOTE.A2, NOTE.C3, NOTE.F3, NOTE.E3 }[index];
                    break;
                case CHORD.Fm:
                    result = new NOTE[] { NOTE.Gs2, NOTE.C3, NOTE.F3, NOTE.Gs2 }[index];
                    break;
                case CHORD.G:
                    result = new NOTE[] { NOTE.G2, NOTE.B2, NOTE.D3, NOTE.G2 }[index];
                    break;
                case CHORD.Gsus4:
                    result = new NOTE[] { NOTE.G2, NOTE.C3, NOTE.D3, NOTE.G2 }[index];
                    break;
                case CHORD.G7:
                    result = new NOTE[] { NOTE.G2, NOTE.B2, NOTE.D3, NOTE.F3 }[index];
                    break;
                case CHORD.A:
                    result = new NOTE[] { NOTE.A2, NOTE.Cs3, NOTE.E3, NOTE.A2 }[index];
                    break;
                case CHORD.Am:
                    result = new NOTE[] { NOTE.A2, NOTE.C3, NOTE.E3, NOTE.A2 }[index];
                    break;
                case CHORD.Am7:
                    result = new NOTE[] { NOTE.A2, NOTE.C3, NOTE.E3, NOTE.G2 }[index];
                    break;
            }

            result += Oct * 12;
            return result;
        }
    }
}
