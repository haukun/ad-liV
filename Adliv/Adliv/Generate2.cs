using AdlivMusic2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Adliv
{
    class Generate2
    {
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
            Exp(midi, vsq4);

            //  出力
            midi.Write();
            vsq4.Write();

        }

        public static void Exp(AdlivMidi midi, AdlivVsq4 vsq4)
        {
            AdlivMidi.MidiTrack[] mTrack = new AdlivMidi.MidiTrack[16];
            mTrack[0] = midi.CreateTrack(AdlivMidi.TONE.T001_PIANO_ACOUSTIC_PIANO);
            mTrack[1] = midi.CreateTrack(AdlivMidi.TONE.T037_BASS_SLAP_BASS1);

            AdlivVsq4.vsTrack[] vTrack = new AdlivVsq4.vsTrack[2];
            vTrack[0] = vsq4.CreateTrack();
            

            AdlivMusic2.CHORD chord = new AdlivMusic2.CHORD(AdlivMusic2.CHORD.KEY.C);

            ExpSub1(mTrack[0], chord.getTonic());
            ExpSub1(mTrack[0], chord.getTonic(2));
            ExpSub1(mTrack[0], chord.getSubDominant());
            ExpSub1(mTrack[0], chord.getDominant());

            ExpSub1(mTrack[0], chord.getTonic(1));
            ExpSub1(mTrack[0], chord.getTonic());
            ExpSub1(mTrack[0], chord.getSubDominant(1));
            ExpSub1(mTrack[0], chord.getDominant());

           
             ExpSubBase(mTrack[1], chord.setOct(-1).getTonicBase(0));
             ExpSubBase(mTrack[1], chord.setOct(-1).getTonicBase(2));
             ExpSubBase(mTrack[1], chord.setOct(-1).getSubDominantBase());
             ExpSubBase(mTrack[1], chord.setOct(-1).getDominantBase());
             ExpSubBase(mTrack[1], chord.setOct(-1).getTonicBase(1));
             ExpSubBase(mTrack[1], chord.setOct(-1).getTonicBase(0));
             ExpSubBase(mTrack[1], chord.setOct(-1).getSubDominantBase(1));
             ExpSubBase(mTrack[1], chord.setOct(-1).getDominantBase());



            //mTrack[0].AddNote(NOTE.Create(LENGTH.N1, chord.getTonic()));
            //mTrack[0].AddNote(NOTE.Create(LENGTH.N1, chord.getTonic(2)));
            //mTrack[0].AddNote(NOTE.Create(LENGTH.N1, chord.getSubDominant()));
            //mTrack[0].AddNote(NOTE.Create(LENGTH.N1, chord.getDominant()));

            //List<NOTE> notes = new List<NOTE>();
            //notes.Add(new NOTE(LENGTH.N1, PITCH.C3));
            //notes.Add(new NOTE(LENGTH.N1, PITCH.E3));
            //notes.Add(new NOTE(LENGTH.N1, PITCH.G3));

            //mTrack[0].AddNote(notes.ToArray());
            //mTrack[0].AddNote(new NOTE(LENGTH.N1, PITCH.F3));
            //mTrack[0].AddNote(new NOTE(LENGTH.N1, PITCH.G3));
            //mTrack[0].AddNote(new NOTE(LENGTH.N1, PITCH.C3));

            ExpSub2(vTrack[0], chord.setOct(1).getTonic());
            ExpSub2(vTrack[0], chord.setOct(1).getTonic(2));
            ExpSub2(vTrack[0], chord.setOct(1).getSubDominant());
            ExpSub2(vTrack[0], chord.setOct(1).getDominant());

            ExpSub2(vTrack[0], chord.setOct(1).getTonic(1));
            ExpSub2(vTrack[0], chord.setOct(1).getTonic());
            ExpSub2(vTrack[0], chord.setOct(1).getSubDominant(1));
            ExpSub2(vTrack[0], chord.setOct(1).getDominant());
        }

        public static void ExpSub2(AdlivVsq4.vsTrack vTrack, PITCH[] pitches)
        {
            for(int i = 0; i < 8; i++)
            {
                PITCH p = pitches[r.Next(pitches.Length)];
                vTrack.AddNote(new NOTE(LENGTH.N8, p), "ra");
            }
        }

        public static void ExpSub1(AdlivMidi.MidiTrack mTrack, PITCH[] pitches)
        {
            mTrack.AddNote(NOTE.Create(LENGTH.N16, pitches));
            mTrack.AddRest(LENGTH.N16);
            mTrack.AddNote(NOTE.Create(LENGTH.N16, pitches));
            mTrack.AddRest(LENGTH.N16);
            mTrack.AddNote(NOTE.Create(LENGTH.N8, pitches));
            mTrack.AddRest(LENGTH.N16);
            mTrack.AddNote(NOTE.Create(LENGTH.N16, pitches));

            mTrack.AddRest(LENGTH.N16);
            mTrack.AddNote(NOTE.Create(LENGTH.N16, pitches));
            mTrack.AddRest(LENGTH.N16);
            mTrack.AddNote(NOTE.Create(LENGTH.N16, pitches));
            mTrack.AddNote(NOTE.Create(LENGTH.N8, pitches));
            mTrack.AddNote(NOTE.Create(LENGTH.N8, pitches));
        }

        public static void ExpSubBase(AdlivMidi.MidiTrack mTrack, PITCH pitch)
        {
            mTrack.AddNote(new NOTE(LENGTH.N4, pitch));
            mTrack.AddNote(new NOTE(LENGTH.N4, pitch));
            mTrack.AddNote(new NOTE(LENGTH.N4, pitch));
            mTrack.AddNote(new NOTE(LENGTH.N4, pitch));
        }
    }
}
