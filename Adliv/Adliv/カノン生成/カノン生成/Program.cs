using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdlivMusic;

namespace カノン生成
{
    class Program
    {
        static void Main(string[] args)
        {
            AdlivMidi midi = AdlivMusicPlus.CreateMidi();
            AdlivVsq4 vsq4 = AdlivMusicPlus.CreateVsq4();

            AdlivMidi.MidiTrack strings1 = midi.CreateTrack(AdlivMusic.AdlivMidi.TONE.T041_STRINGS_VIOLIN);
            AdlivMidi.MidiTrack strings2 = midi.CreateTrack(AdlivMusic.AdlivMidi.TONE.T041_STRINGS_VIOLIN);
            AdlivMidi.MidiTrack strings3 = midi.CreateTrack(AdlivMusic.AdlivMidi.TONE.T042_STRINGS_VIOLA);

            AdlivVsq4.vsTrack choir1 = vsq4.CreateTrack();
            AdlivVsq4.vsTrack choir2 = vsq4.CreateTrack();
            AdlivVsq4.vsTrack choir3 = vsq4.CreateTrack();

            strings1.AddNote(NOTEL.N1, NOTE.x, 0);
            strings2.AddNote(NOTEL.N1, NOTE.x, 0);
            strings3.AddNote(NOTEL.N1, NOTE.x, 0);

            choir1.AddRest(NOTEL.N1);
            choir2.AddRest(NOTEL.N1);
            choir3.AddRest(NOTEL.N1);

            CHORD[] chord = AdlivMusicPlus.CHORD018_Canon;

            Random r = new Random();

            int t = 0;

            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j <chord.Length; j++)
                {
                    switch (t % 4)
                    {
                        case 0:
                            strings1.AddNote(NOTEL.N2, AdlivMusicPlus.GetChord(chord[j], 0, 0), 100);
                            strings2.AddNote(NOTEL.N2, AdlivMusicPlus.GetChord(chord[j], 0, 1), 100);
                            strings3.AddNote(NOTEL.N2, AdlivMusicPlus.GetChord(chord[j], -1, 2), 100);
                            break;
                        case 1:
                            for (int k = 0; k < 2; k++)
                            {
                                strings1.AddNote(NOTEL.N4, AdlivMusicPlus.GetChord(chord[j], 0), 100);
                                strings2.AddNote(NOTEL.N4, AdlivMusicPlus.GetChord(chord[j], 0), 100);
                            }
                                strings3.AddNote(NOTEL.N2, AdlivMusicPlus.GetChord(chord[j], -1, 2), 100);
                         break;
                        case 2:
                             for (int k = 0; k < 4; k++)
                             {
                                 strings1.AddNote(NOTEL.N8, AdlivMusicPlus.GetChord(chord[j], 1), 100);
                                 strings2.AddNote(NOTEL.N8, AdlivMusicPlus.GetChord(chord[j], 0), 100);
                             }
                             strings3.AddNote(NOTEL.N2, AdlivMusicPlus.GetChord(chord[j], -1, 2), 100);
                            break;
                        case 3:
                            for (int k = 0; k < 8; k++)
                            {
                                strings1.AddNote(NOTEL.N16, AdlivMusicPlus.GetChord(chord[j], 1), 100);
                                strings2.AddNote(NOTEL.N16, AdlivMusicPlus.GetChord(chord[j], 0), 100);
                            }
                            strings3.AddNote(NOTEL.N2, AdlivMusicPlus.GetChord(chord[j], -1, 2), 100);
                            break;
                    }

                    //AdlivMusicPlus.FillNote(choir1, chord[j], NOTEL.N2);
                    choir1.AddNote(NOTEL.N8, AdlivMusicPlus.GetChordNote(chord[j], i, true), "ra");
                    choir1.AddNote(NOTEL.N8, AdlivMusicPlus.GetChordNote(chord[j], i, true), "ra");
                    choir1.AddNote(NOTEL.N8, AdlivMusicPlus.GetChordNote(chord[j], i, true), "ra");
                    choir1.AddNote(NOTEL.N8, AdlivMusicPlus.GetChordNote(chord[j], i, true), "ra");
                }
                t++;
            }

            strings1.AddNote(NOTEL.N1x2, NOTE.G2, 64);
            strings2.AddNote(NOTEL.N1x2, NOTE.C3, 64);
            strings3.AddNote(NOTEL.N1x2, NOTE.E3, 64);

            choir1.AddNote(NOTEL.N1x2, NOTE.G2, "a");
            //choir2.AddNote(NOTEL.N1x2, NOTE.G3, "a");
            //choir3.AddNote(NOTEL.N1x2, NOTE.E3, "a");

            midi.Write();
            vsq4.Write();
        }
    }
}
