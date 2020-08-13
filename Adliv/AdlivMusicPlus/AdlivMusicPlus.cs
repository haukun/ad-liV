using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdlivMusic;
using System.IO;


namespace AdlivMusic
{
    /*
    public class AdlivMusicPlus
    {
        private static Random r = new Random();

        public static AdlivMidi CreateMidi()
        {
            return new AdlivMidi(PathHelper() + ".midi");
        }

        public static AdlivVsq4 CreateVsq4()
        {
            return new AdlivVsq4(PathHelper() + ".vsqx", "BCNFCY43LB2LZCD4", "MIKU_V4X_Original_EVEC", "BFPL93T7GE3RWFC9", "ACA9C502-A04B-42b5-B2EB-5CEA36D16FCE", "VlNDSwAAAAADAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA=");
        }
        public static string PathHelper()
        {
            if (! Directory.Exists("output"))
            {
                Directory.CreateDirectory("output");
            }
            return "output\\" + DateTime.Now.ToString("yyyyMMdd-HHmmss");
        }

        //  Chord 001
        public static CHORD.NAME[] CHORD001_Basic = new CHORD.NAME[]{
                CHORD.NAME.C, CHORD.NAME.C, CHORD.NAME.C, CHORD.NAME.C,
                CHORD.NAME.Dm, CHORD.NAME.Dm, CHORD.NAME.Dm, CHORD.NAME.Dm,
                CHORD.NAME.Em, CHORD.NAME.Em, CHORD.NAME.Dm, CHORD.NAME.Dm,
                CHORD.NAME.G, CHORD.NAME.G,CHORD.NAME.C, CHORD.NAME.C7,
            };

        //  Chord 003
        public static CHORD.NAME[] CHORD003_Basic = new CHORD.NAME[]{
                CHORD.NAME.C, CHORD.NAME.C, CHORD.NAME.C, CHORD.NAME.C,
                CHORD.NAME.F, CHORD.NAME.F, CHORD.NAME.C, CHORD.NAME.G,
                CHORD.NAME.C, CHORD.NAME.C, CHORD.NAME.E, CHORD.NAME.Am,
                CHORD.NAME.Dm, CHORD.NAME.G,CHORD.NAME.C, CHORD.NAME.C,
            };

        //  Chord 004
        public static CHORD.NAME[] CHORD004_Basic = new CHORD.NAME[]{
                CHORD.NAME.C, CHORD.NAME.C, CHORD.NAME.G, CHORD.NAME.G,
                CHORD.NAME.Am, CHORD.NAME.Am, CHORD.NAME.F, CHORD.NAME.F,
                CHORD.NAME.C, CHORD.NAME.C, CHORD.NAME.G, CHORD.NAME.G,
                CHORD.NAME.F, CHORD.NAME.F ,CHORD.NAME.C, CHORD.NAME.C,
            };

        //  Chord 005
        public static CHORD.NAME[] CHORD005_Basic = new CHORD.NAME[]{
                CHORD.NAME.C, CHORD.NAME.G, CHORD.NAME.Am, CHORD.NAME.G,
                CHORD.NAME.F, CHORD.NAME.C, CHORD.NAME.Dm7, CHORD.NAME.G7,
                CHORD.NAME.C, CHORD.NAME.G, CHORD.NAME.Am, CHORD.NAME.G,
                CHORD.NAME.F, CHORD.NAME.C, CHORD.NAME.Dm7, CHORD.NAME.G7,
            };

        //  Chord 006
        public static CHORD.NAME[] CHORD006_Basic = new CHORD.NAME[]{
                CHORD.NAME.F, CHORD.NAME.G7, CHORD.NAME.C, CHORD.NAME.C,
                CHORD.NAME.F, CHORD.NAME.G7, CHORD.NAME.C, CHORD.NAME.C,
                CHORD.NAME.F, CHORD.NAME.G7, CHORD.NAME.C, CHORD.NAME.C,
                CHORD.NAME.F, CHORD.NAME.G7, CHORD.NAME.C, CHORD.NAME.C,
            };

        //  Chord 007
        public static CHORD.NAME[] CHORD007_Basic = new CHORD.NAME[]{
                CHORD.NAME.C, CHORD.NAME.C, CHORD.NAME.C, CHORD.NAME.C,
                CHORD.NAME.Am, CHORD.NAME.Am, CHORD.NAME.Am, CHORD.NAME.Am,
                CHORD.NAME.Dm, CHORD.NAME.Dm, CHORD.NAME.Dm, CHORD.NAME.Dm,
                CHORD.NAME.F, CHORD.NAME.F, CHORD.NAME.G7, CHORD.NAME.G7,
            };

        //  Chord 008
        public static CHORD.NAME[] CHORD008_Basic = new CHORD.NAME[]{
                CHORD.NAME.C, CHORD.NAME.C, CHORD.NAME.G, CHORD.NAME.G,
                CHORD.NAME.F, CHORD.NAME.F, CHORD.NAME.C, CHORD.NAME.C,
                CHORD.NAME.C, CHORD.NAME.C, CHORD.NAME.G, CHORD.NAME.G,
                CHORD.NAME.F, CHORD.NAME.F, CHORD.NAME.C, CHORD.NAME.C,
            };

        //  Chord 009
        public static CHORD.NAME[] CHORD009_Basic = new CHORD.NAME[]{
                CHORD.NAME.Am, CHORD.NAME.Dm, CHORD.NAME.G, CHORD.NAME.C,
                CHORD.NAME.Am, CHORD.NAME.Dm, CHORD.NAME.G, CHORD.NAME.C,
                CHORD.NAME.Am, CHORD.NAME.Dm, CHORD.NAME.G, CHORD.NAME.C,
                CHORD.NAME.Am, CHORD.NAME.Dm, CHORD.NAME.G, CHORD.NAME.C,
            };

        //  Chord 010
        public static CHORD.NAME[] CHORD010_Basic = new CHORD.NAME[]{
                CHORD.NAME.C, CHORD.NAME.C, CHORD.NAME.G, CHORD.NAME.G,
                CHORD.NAME.Am, CHORD.NAME.Am, CHORD.NAME.Em, CHORD.NAME.Em,
                CHORD.NAME.F, CHORD.NAME.G, CHORD.NAME.Em, CHORD.NAME.Am,
                CHORD.NAME.Dm, CHORD.NAME.Dm, CHORD.NAME.G, CHORD.NAME.G,
            };


        //  Chord 011
        public static CHORD.NAME[] CHORD011_Basic = new CHORD.NAME[]{
                CHORD.NAME.C, CHORD.NAME.G, CHORD.NAME.Am, CHORD.NAME.F,
                CHORD.NAME.C, CHORD.NAME.G, CHORD.NAME.Am, CHORD.NAME.F,
                CHORD.NAME.C, CHORD.NAME.G, CHORD.NAME.Am, CHORD.NAME.F,
                CHORD.NAME.C, CHORD.NAME.G, CHORD.NAME.Am, CHORD.NAME.F,
            };


        //  Chord 012
        public static CHORD.NAME[] CHORD012_Basic = new CHORD.NAME[]{
                CHORD.NAME.C, CHORD.NAME.F, CHORD.NAME.G, CHORD.NAME.Am,
                CHORD.NAME.C, CHORD.NAME.F, CHORD.NAME.G, CHORD.NAME.Am,
                CHORD.NAME.C, CHORD.NAME.F, CHORD.NAME.G, CHORD.NAME.Am,
                CHORD.NAME.C, CHORD.NAME.F, CHORD.NAME.G, CHORD.NAME.Am,
            };

        //  Chord 014
        public static CHORD.NAME[] CHORD014_Basic = new CHORD.NAME[]{
                CHORD.NAME.C, CHORD.NAME.C, CHORD.NAME.C, CHORD.NAME.C,
                CHORD.NAME.Am, CHORD.NAME.Am, CHORD.NAME.C, CHORD.NAME.C,
                CHORD.NAME.F, CHORD.NAME.F, CHORD.NAME.F, CHORD.NAME.F,
                CHORD.NAME.Am, CHORD.NAME.Am, CHORD.NAME.G, CHORD.NAME.G,
            };

        //  Chord 018
        public static CHORD.NAME[] CHORD018_Canon = new CHORD.NAME[]{
                CHORD.NAME.C, CHORD.NAME.G, CHORD.NAME.Am, CHORD.NAME.Em,
                CHORD.NAME.F, CHORD.NAME.C, CHORD.NAME.F, CHORD.NAME.G,
                CHORD.NAME.C, CHORD.NAME.G, CHORD.NAME.Am, CHORD.NAME.Em,
                CHORD.NAME.F, CHORD.NAME.C, CHORD.NAME.F, CHORD.NAME.G,
            };

        //  Chord 021
        public static CHORD.NAME[] CHORD021_Canon = new CHORD.NAME[]{
                CHORD.NAME.C, CHORD.NAME.G, CHORD.NAME.Am, CHORD.NAME.Em7,
                CHORD.NAME.F, CHORD.NAME.C, CHORD.NAME.Dm7, CHORD.NAME.G7,
                CHORD.NAME.C, CHORD.NAME.G, CHORD.NAME.Am, CHORD.NAME.Em7,
                CHORD.NAME.F, CHORD.NAME.C, CHORD.NAME.Dm7, CHORD.NAME.E7,
            };

        //  Chord 026
        public static CHORD.NAME[] CHORD026_Basic = new CHORD.NAME[]{
                CHORD.NAME.F, CHORD.NAME.G, CHORD.NAME.G, CHORD.NAME.Am,
                CHORD.NAME.F, CHORD.NAME.G, CHORD.NAME.G, CHORD.NAME.Am,
                CHORD.NAME.F, CHORD.NAME.G, CHORD.NAME.E, CHORD.NAME.Am,
                CHORD.NAME.F, CHORD.NAME.G, CHORD.NAME.Am, CHORD.NAME.Am,
            };

        //  Chord 033
        public static CHORD.NAME[] CHORD033_Basic = new CHORD.NAME[]{
                CHORD.NAME.C, CHORD.NAME.C, CHORD.NAME.C, CHORD.NAME.C,
                CHORD.NAME.F, CHORD.NAME.F, CHORD.NAME.C, CHORD.NAME.C,
                CHORD.NAME.G, CHORD.NAME.G, CHORD.NAME.C, CHORD.NAME.C,
                CHORD.NAME.D7, CHORD.NAME.D7, CHORD.NAME.G7, CHORD.NAME.G7,
            };

        //  Chord 035
        public static CHORD.NAME[] CHORD035_Basic = new CHORD.NAME[]{
                CHORD.NAME.C, CHORD.NAME.C, CHORD.NAME.G, CHORD.NAME.G,
                CHORD.NAME.Am, CHORD.NAME.Am, CHORD.NAME.Em, CHORD.NAME.Em,
                CHORD.NAME.F, CHORD.NAME.Fm, CHORD.NAME.C, CHORD.NAME.A,
                CHORD.NAME.D7, CHORD.NAME.D7, CHORD.NAME.G7, CHORD.NAME.G7,
            };

        //  Chord 059
        public static CHORD.NAME[] CHORD059_Basic = new CHORD.NAME[]{
                CHORD.NAME.C, CHORD.NAME.C, CHORD.NAME.E7, CHORD.NAME.E7,
                CHORD.NAME.F, CHORD.NAME.F, CHORD.NAME.C, CHORD.NAME.G,
                CHORD.NAME.Am, CHORD.NAME.Am, CHORD.NAME.D7, CHORD.NAME.D7,
                CHORD.NAME.Gsus4, CHORD.NAME.Gsus4, CHORD.NAME.G, CHORD.NAME.G,
            };

        //  Chord 078
        public static CHORD.NAME[] CHORD078_Basic = new CHORD.NAME[]{
                CHORD.NAME.C, CHORD.NAME.C, CHORD.NAME.F, CHORD.NAME.F,
                CHORD.NAME.C, CHORD.NAME.C, CHORD.NAME.F, CHORD.NAME.C,
                CHORD.NAME.Em, CHORD.NAME.Em, CHORD.NAME.Am, CHORD.NAME.Am,
                CHORD.NAME.Dm7, CHORD.NAME.Dm7, CHORD.NAME.G7, CHORD.NAME.G7,
            };

        //  Chord 079
        public static CHORD.NAME[] CHORD079_Basic = new CHORD.NAME[]{
                CHORD.NAME.Am, CHORD.NAME.Am, CHORD.NAME.Em, CHORD.NAME.Em,
                CHORD.NAME.Am, CHORD.NAME.Am, CHORD.NAME.Em, CHORD.NAME.Em,
                CHORD.NAME.Dm7, CHORD.NAME.G7, CHORD.NAME.C, CHORD.NAME.Am,
                CHORD.NAME.Dm, CHORD.NAME.F, CHORD.NAME.C, CHORD.NAME.Dm7,
            };

        //  Chord 0101
        public static CHORD.NAME[] CHORD101_Basic = new CHORD.NAME[]{
                CHORD.NAME.Dm7, CHORD.NAME.Dm7, CHORD.NAME.Dm7, CHORD.NAME.Dm7,
                CHORD.NAME.Am7, CHORD.NAME.Am7, CHORD.NAME.Am7, CHORD.NAME.Am7,
                CHORD.NAME.FM7, CHORD.NAME.FM7, CHORD.NAME.C, CHORD.NAME.C,
                CHORD.NAME.Dm7, CHORD.NAME.Dm7, CHORD.NAME.E7, CHORD.NAME.E7,
            };
    }
    */
}
