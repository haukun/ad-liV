using AdlivMusic;

///##################################################
///     蛍の光の楽譜を生成するプログラム
///                                     Ver.1.0
///                                     2018/07/28
///                                     はぅ君
///##################################################

namespace 蛍の光
{
    class Program
    {
        static void Main(string[] args)
        {
            AdlivVsq4 vsq4 = new AdlivVsq4("蛍の光.vsqx", "InputYourVoiceId", "MIKU_V4X_Original_EVEC", "InputYourVoiceId2", "InputYourTrackId", "InputYourAuxContent");
            AdlivMidi midi = new AdlivMidi("蛍の光.midi");
            midi.SetTempo(72);

            AdlivMidi.MidiTrack piano1 = midi.CreateTrack(AdlivMidi.TONE.T001_PIANO_ACOUSTIC_PIANO);
            AdlivMidi.MidiTrack piano2 = midi.CreateTrack(AdlivMidi.TONE.T001_PIANO_ACOUSTIC_PIANO);
            AdlivMidi.MidiTrack piano3 = midi.CreateTrack(AdlivMidi.TONE.T001_PIANO_ACOUSTIC_PIANO);

            AdlivVsq4.vsTrack choir1 = vsq4.CreateTrack();
            AdlivVsq4.vsTrack choir2 = vsq4.CreateTrack();

            piano1.AddNote(NOTEL.N1, NOTE.x, 0);
            piano2.AddNote(NOTEL.N1, NOTE.x, 0);
            piano3.AddNote(NOTEL.N1, NOTE.x, 0);
            choir1.AddRest(NOTEL.N1);
            choir2.AddRest(NOTEL.N1);

            //--------------------------------------------------
            //  １．ほたるのひかり　まどのゆき
            //--------------------------------------------------
            AddMidi(piano1, NOTEL.N8, NOTE.C3);
            AddMidi(piano1, NOTEL.N8d, NOTE.F3);
            AddMidi(piano1, NOTEL.N16 , NOTE.F3);
            AddMidi(piano1, NOTEL.N8, NOTE.F3);
            AddMidi(piano1, NOTEL.N8, NOTE.A3);
            AddMidi(piano1, NOTEL.N8d, NOTE.G3);
            AddMidi(piano1, NOTEL.N16, NOTE.F3);
            AddMidi(piano1, NOTEL.N8, NOTE.G3);
            AddMidi(piano1, NOTEL.N8, NOTE.A3);
            AddMidi(piano1, NOTEL.N8, NOTE.F3);
            AddMidi(piano1, NOTEL.N8, NOTE.F3);
            AddMidi(piano1, NOTEL.N8, NOTE.A3);
            AddMidi(piano1, NOTEL.N8, NOTE.C4);
            AddMidi(piano1, NOTEL.N4d, NOTE.D4);

            AddMidi(piano2, NOTEL.N8, NOTE.C3);
            AddMidi(piano2, NOTEL.N8d, NOTE.A2);
            AddMidi(piano2, NOTEL.N16, NOTE.A2);
            AddMidi(piano2, NOTEL.N8, NOTE.A2);
            AddMidi(piano2, NOTEL.N8, NOTE.C3);
            AddMidi(piano2, NOTEL.N8d, NOTE.As2);
            AddMidi(piano2, NOTEL.N16, NOTE.A2);
            AddMidi(piano2, NOTEL.N8, NOTE.As2);
            AddMidi(piano2, NOTEL.N8, NOTE.C3);
            AddMidi(piano2, NOTEL.N8, NOTE.A2);
            AddMidi(piano2, NOTEL.N8, NOTE.A2);
            AddMidi(piano2, NOTEL.N8, NOTE.C3);
            AddMidi(piano2, NOTEL.N8, NOTE.Ds3);
            AddMidi(piano2, NOTEL.N4d, NOTE.D3);

            AddMidi(piano3, NOTEL.N8, NOTE.x);
            AddMidi(piano3, NOTEL.N4, NOTE.F2);
            AddMidi(piano3, NOTEL.N8, NOTE.F2);
            AddMidi(piano3, NOTEL.N8, NOTE.F2);
            AddMidi(piano3, NOTEL.N4, NOTE.C2);
            AddMidi(piano3, NOTEL.N8, NOTE.C2);
            AddMidi(piano3, NOTEL.N8, NOTE.C2);
            AddMidi(piano3, NOTEL.N8, NOTE.F2);
            AddMidi(piano3, NOTEL.N8, NOTE.F2);
            AddMidi(piano3, NOTEL.N8, NOTE.F2);
            AddMidi(piano3, NOTEL.N8, NOTE.A1);
            AddMidi(piano3, NOTEL.N4d, NOTE.As1);

            choir1.AddNote(NOTEL.N8, NOTE.C3, "ho");
            choir1.AddNote(NOTEL.N8d, NOTE.F3, "ta");
            choir1.AddNote(NOTEL.N16, NOTE.F3, "ru");
            choir1.AddNote(NOTEL.N8, NOTE.F3, "no");
            choir1.AddNote(NOTEL.N8, NOTE.A3, "hi");
            choir1.AddNote(NOTEL.N8d, NOTE.G3, "ka");
            choir1.AddNote(NOTEL.N16, NOTE.F3, "a");
            choir1.AddNote(NOTEL.N8, NOTE.G3, "ri");
            choir1.AddNote(NOTEL.N8, NOTE.A3, "ma");
            choir1.AddNote(NOTEL.N8, NOTE.F3, "do");
            choir1.AddNote(NOTEL.N8, NOTE.F3, "no");
            choir1.AddNote(NOTEL.N8, NOTE.A3, "yu");
            choir1.AddNote(NOTEL.N8, NOTE.C4, "u");
            choir1.AddNote(NOTEL.N4d, NOTE.D4, "ki");

            choir2.AddNote(NOTEL.N8, NOTE.C3, "ho");
            choir2.AddNote(NOTEL.N8d, NOTE.C3, "ta");
            choir2.AddNote(NOTEL.N16, NOTE.C3, "ru");
            choir2.AddNote(NOTEL.N8, NOTE.C3, "no");
            choir2.AddNote(NOTEL.N8, NOTE.F3, "hi");
            choir2.AddNote(NOTEL.N8d, NOTE.E3, "ka");
            choir2.AddNote(NOTEL.N16, NOTE.A2, "a");
            choir2.AddNote(NOTEL.N8, NOTE.E3, "ri");
            choir2.AddNote(NOTEL.N8, NOTE.E3, "ma");
            choir2.AddNote(NOTEL.N8, NOTE.C3, "do");
            choir2.AddNote(NOTEL.N8, NOTE.C3, "no");
            choir2.AddNote(NOTEL.N8, NOTE.F3, "yu");
            choir2.AddNote(NOTEL.N8, NOTE.F3, "u");
            choir2.AddNote(NOTEL.N4d, NOTE.As3, "ki");

            //--------------------------------------------------
            //  ２．ふみよむつきひ　かさねつつ
            //--------------------------------------------------
            AddMidi(piano1, NOTEL.N8, NOTE.F4);
            AddMidi(piano1, NOTEL.N8d, NOTE.C4);
            AddMidi(piano1, NOTEL.N16, NOTE.A3);
            AddMidi(piano1, NOTEL.N8, NOTE.A3);
            AddMidi(piano1, NOTEL.N8, NOTE.F3);
            AddMidi(piano1, NOTEL.N8d, NOTE.G3);
            AddMidi(piano1, NOTEL.N16, NOTE.F3);
            AddMidi(piano1, NOTEL.N8, NOTE.G3);
            AddMidi(piano1, NOTEL.N8, NOTE.A3);
            AddMidi(piano1, NOTEL.N8d, NOTE.F3);
            AddMidi(piano1, NOTEL.N16, NOTE.D3);
            AddMidi(piano1, NOTEL.N8, NOTE.D3);
            AddMidi(piano1, NOTEL.N8, NOTE.C3);
            AddMidi(piano1, NOTEL.N4d, NOTE.F3);

            AddMidi(piano2, NOTEL.N8, NOTE.F3);
            AddMidi(piano2, NOTEL.N8d, NOTE.F3);
            AddMidi(piano2, NOTEL.N16, NOTE.C3);
            AddMidi(piano2, NOTEL.N8, NOTE.C3);
            AddMidi(piano2, NOTEL.N8, NOTE.A2);
            AddMidi(piano2, NOTEL.N8d, NOTE.As2);
            AddMidi(piano2, NOTEL.N16, NOTE.A2);
            AddMidi(piano2, NOTEL.N8, NOTE.As2);
            AddMidi(piano2, NOTEL.N8, NOTE.Cs3);
            AddMidi(piano2, NOTEL.N8d, NOTE.D2);
            AddMidi(piano2, NOTEL.N16, NOTE.As2);
            AddMidi(piano2, NOTEL.N8, NOTE.E2);
            AddMidi(piano2, NOTEL.N8, NOTE.E2);
            AddMidi(piano2, NOTEL.N4d, NOTE.F2);

            AddMidi(piano3, NOTEL.N8, NOTE.D2);
            AddMidi(piano3, NOTEL.N4, NOTE.F2);
            AddMidi(piano3, NOTEL.N8, NOTE.F2);
            AddMidi(piano3, NOTEL.N8, NOTE.F2);
            AddMidi(piano3, NOTEL.N4, NOTE.C2);
            AddMidi(piano3, NOTEL.N8, NOTE.C2);
            AddMidi(piano3, NOTEL.N8, NOTE.A1);
            AddMidi(piano3, NOTEL.N4, NOTE.As1);
            AddMidi(piano3, NOTEL.N8, NOTE.C2);
            AddMidi(piano3, NOTEL.N8, NOTE.C2);
            AddMidi(piano3, NOTEL.N4d, NOTE.F1);

            choir1.AddNote(NOTEL.N8, NOTE.D4, "hu");
            choir1.AddNote(NOTEL.N8d, NOTE.C4, "mi");
            choir1.AddNote(NOTEL.N16, NOTE.A3, "yo");
            choir1.AddNote(NOTEL.N8, NOTE.A3, "mu");
            choir1.AddNote(NOTEL.N8, NOTE.F3, "tu");
            choir1.AddNote(NOTEL.N8d, NOTE.G3, "ki");
            choir1.AddNote(NOTEL.N16, NOTE.F3, "i");
            choir1.AddNote(NOTEL.N8, NOTE.G3, "hi");
            choir1.AddNote(NOTEL.N8, NOTE.A3, "ka");
            choir1.AddNote(NOTEL.N8d, NOTE.F3, "sa");
            choir1.AddNote(NOTEL.N16, NOTE.D3, "ne");
            choir1.AddNote(NOTEL.N8, NOTE.D3, "tu");
            choir1.AddNote(NOTEL.N8, NOTE.C3, "u");
            choir1.AddNote(NOTEL.N4d, NOTE.F3, "tu");

            choir2.AddNote(NOTEL.N8, NOTE.As3, "hu");
            choir2.AddNote(NOTEL.N8d, NOTE.A3, "mi");
            choir2.AddNote(NOTEL.N16, NOTE.F3, "yo");
            choir2.AddNote(NOTEL.N8, NOTE.F3, "mu");
            choir2.AddNote(NOTEL.N8, NOTE.C3, "tu");
            choir2.AddNote(NOTEL.N8d, NOTE.E3, "ki");
            choir2.AddNote(NOTEL.N16, NOTE.A2, "i");
            choir2.AddNote(NOTEL.N8, NOTE.E3, "hi");
            choir2.AddNote(NOTEL.N8, NOTE.E3, "ka");
            choir2.AddNote(NOTEL.N8d, NOTE.D3, "sa");
            choir2.AddNote(NOTEL.N16, NOTE.As2, "ne");
            choir2.AddNote(NOTEL.N8, NOTE.As2, "tu");
            choir2.AddNote(NOTEL.N8, NOTE.As2, "u");
            choir2.AddNote(NOTEL.N4d, NOTE.C3, "tu");
            //--------------------------------------------------
            //  ３．いつしかときも　すぎのとを
            //--------------------------------------------------
            AddMidi(piano1, NOTEL.N8, NOTE.D4);
            AddMidi(piano1, NOTEL.N8d, NOTE.C4);
            AddMidi(piano1, NOTEL.N16, NOTE.A3);
            AddMidi(piano1, NOTEL.N8, NOTE.A3);
            AddMidi(piano1, NOTEL.N8, NOTE.F3);
            AddMidi(piano1, NOTEL.N8d, NOTE.G3);
            AddMidi(piano1, NOTEL.N16, NOTE.F3);
            AddMidi(piano1, NOTEL.N8, NOTE.G3);
            AddMidi(piano1, NOTEL.N8, NOTE.D4);
            AddMidi(piano1, NOTEL.N8d, NOTE.C4);
            AddMidi(piano1, NOTEL.N16, NOTE.A3);
            AddMidi(piano1, NOTEL.N8, NOTE.A3);
            AddMidi(piano1, NOTEL.N8, NOTE.C4);
            AddMidi(piano1, NOTEL.N4d, NOTE.D4);

            AddMidi(piano2, NOTEL.N16, NOTE.D3);
            AddMidi(piano2, NOTEL.N16, NOTE.E3);
            AddMidi(piano2, NOTEL.N8d, NOTE.F3);
            AddMidi(piano2, NOTEL.N16, NOTE.C3);
            AddMidi(piano2, NOTEL.N8, NOTE.C3);
            AddMidi(piano2, NOTEL.N8, NOTE.A2);
            AddMidi(piano2, NOTEL.N8d, NOTE.As2);
            AddMidi(piano2, NOTEL.N16, NOTE.A2);
            AddMidi(piano2, NOTEL.N8, NOTE.As2);
            AddMidi(piano2, NOTEL.N16, NOTE.D3);
            AddMidi(piano2, NOTEL.N16, NOTE.E3);
            AddMidi(piano2, NOTEL.N8d, NOTE.F3);
            AddMidi(piano2, NOTEL.N16, NOTE.C3);
            AddMidi(piano2, NOTEL.N8, NOTE.C3);
            AddMidi(piano2, NOTEL.N8, NOTE.Ds3);
            AddMidi(piano2, NOTEL.N4d, NOTE.D3);

            AddMidi(piano3, NOTEL.N8, NOTE.F2);
            AddMidi(piano3, NOTEL.N4, NOTE.F2);
            AddMidi(piano3, NOTEL.N8, NOTE.F2);
            AddMidi(piano3, NOTEL.N8, NOTE.F2);
            AddMidi(piano3, NOTEL.N4, NOTE.C2);
            AddMidi(piano3, NOTEL.N8, NOTE.C2);
            AddMidi(piano3, NOTEL.N8, NOTE.A1);
            AddMidi(piano3, NOTEL.N4, NOTE.F2);
            AddMidi(piano3, NOTEL.N8, NOTE.F2);
            AddMidi(piano3, NOTEL.N8, NOTE.A1);
            AddMidi(piano3, NOTEL.N4d, NOTE.As1);

            choir1.AddNote(NOTEL.N8, NOTE.D4, "i");
            choir1.AddNote(NOTEL.N8d, NOTE.C4, "tu");
            choir1.AddNote(NOTEL.N16, NOTE.A3, "si");
            choir1.AddNote(NOTEL.N8, NOTE.A3, "ka");
            choir1.AddNote(NOTEL.N8, NOTE.F3, "to");
            choir1.AddNote(NOTEL.N8d, NOTE.G3, "ki");
            choir1.AddNote(NOTEL.N16, NOTE.F3, "i");
            choir1.AddNote(NOTEL.N8, NOTE.G3, "mo");
            choir1.AddNote(NOTEL.N8, NOTE.D4, "su");
            choir1.AddNote(NOTEL.N8d, NOTE.C4, "gi");
            choir1.AddNote(NOTEL.N16, NOTE.A3, "i");
            choir1.AddNote(NOTEL.N8, NOTE.A3, "no");
            choir1.AddNote(NOTEL.N8, NOTE.C4, "to");
            choir1.AddNote(NOTEL.N4d, NOTE.D4, "wo");

            choir2.AddNote(NOTEL.N8, NOTE.As3, "i");
            choir2.AddNote(NOTEL.N8d, NOTE.A3, "tu");
            choir2.AddNote(NOTEL.N16, NOTE.F3, "si");
            choir2.AddNote(NOTEL.N8, NOTE.F3, "ka");
            choir2.AddNote(NOTEL.N8, NOTE.C3, "to");
            choir2.AddNote(NOTEL.N8d, NOTE.E3, "ki");
            choir2.AddNote(NOTEL.N16, NOTE.A2, "i");
            choir2.AddNote(NOTEL.N8, NOTE.E3, "mo");
            choir2.AddNote(NOTEL.N8, NOTE.As3, "su");
            choir2.AddNote(NOTEL.N8d, NOTE.A3, "gi");
            choir2.AddNote(NOTEL.N16, NOTE.F3, "i");
            choir2.AddNote(NOTEL.N8, NOTE.F3, "no");
            choir2.AddNote(NOTEL.N8, NOTE.F3, "to");
            choir2.AddNote(NOTEL.N4d, NOTE.As3, "wo");
            //--------------------------------------------------
            //  ４．　あけてぞけさは　わかれゆく
            //--------------------------------------------------
            AddMidi(piano1, NOTEL.N8, NOTE.F4);
            AddMidi(piano1, NOTEL.N8d, NOTE.C3);
            AddMidi(piano1, NOTEL.N16, NOTE.A3);
            AddMidi(piano1, NOTEL.N8, NOTE.A3);
            AddMidi(piano1, NOTEL.N8, NOTE.F3);
            AddMidi(piano1, NOTEL.N8d, NOTE.G3);
            AddMidi(piano1, NOTEL.N16, NOTE.F3);
            AddMidi(piano1, NOTEL.N8, NOTE.G3);
            AddMidi(piano1, NOTEL.N8, NOTE.A3);
            AddMidi(piano1, NOTEL.N8d, NOTE.F3);
            AddMidi(piano1, NOTEL.N16, NOTE.D3);
            AddMidi(piano1, NOTEL.N8, NOTE.D3);
            AddMidi(piano1, NOTEL.N8, NOTE.C3);
            AddMidi(piano1, NOTEL.N4d, NOTE.F3);

            AddMidi(piano2, NOTEL.N8, NOTE.F3);
            AddMidi(piano2, NOTEL.N8d, NOTE.F3);
            AddMidi(piano2, NOTEL.N16, NOTE.C3);
            AddMidi(piano2, NOTEL.N8, NOTE.C3);
            AddMidi(piano2, NOTEL.N8, NOTE.A2);
            AddMidi(piano2, NOTEL.N8d, NOTE.As2);
            AddMidi(piano2, NOTEL.N16, NOTE.A2);
            AddMidi(piano2, NOTEL.N8, NOTE.As2);
            AddMidi(piano2, NOTEL.N8, NOTE.Cs3);
            AddMidi(piano2, NOTEL.N8d, NOTE.D3);
            AddMidi(piano2, NOTEL.N16, NOTE.As2);
            AddMidi(piano2, NOTEL.N8, NOTE.E2);
            AddMidi(piano2, NOTEL.N8, NOTE.E2);
            AddMidi(piano2, NOTEL.N4d, NOTE.F2);

            AddMidi(piano3, NOTEL.N8, NOTE.D2);
            AddMidi(piano3, NOTEL.N4, NOTE.F2);
            AddMidi(piano3, NOTEL.N8, NOTE.F2);
            AddMidi(piano3, NOTEL.N8, NOTE.F2);
            AddMidi(piano3, NOTEL.N4, NOTE.C2);
            AddMidi(piano3, NOTEL.N8, NOTE.C2);
            AddMidi(piano3, NOTEL.N8, NOTE.A1);
            AddMidi(piano3, NOTEL.N4, NOTE.As1);
            AddMidi(piano3, NOTEL.N8, NOTE.C2);
            AddMidi(piano3, NOTEL.N8, NOTE.C2);
            AddMidi(piano3, NOTEL.N4d, NOTE.F1);

            choir1.AddNote(NOTEL.N8, NOTE.F4, "a");
            choir1.AddNote(NOTEL.N8d, NOTE.C4, "ke");
            choir1.AddNote(NOTEL.N16, NOTE.A3, "te");
            choir1.AddNote(NOTEL.N8, NOTE.A3, "zo");
            choir1.AddNote(NOTEL.N8, NOTE.F3, "ke");
            choir1.AddNote(NOTEL.N8d, NOTE.G3, "sa");
            choir1.AddNote(NOTEL.N16, NOTE.F3, "a");
            choir1.AddNote(NOTEL.N8, NOTE.G3, "wa");
            choir1.AddNote(NOTEL.N8, NOTE.A3, "wa");
            choir1.AddNote(NOTEL.N8d, NOTE.F3, "ka");
            choir1.AddNote(NOTEL.N16, NOTE.D3, "re");
            choir1.AddNote(NOTEL.N8, NOTE.D3, "yu");
            choir1.AddNote(NOTEL.N8, NOTE.C3, "u");
            choir1.AddNote(NOTEL.N4d, NOTE.F3, "ku");

            choir2.AddNote(NOTEL.N8, NOTE.As3, "a");
            choir2.AddNote(NOTEL.N8d, NOTE.A3, "ke");
            choir2.AddNote(NOTEL.N16, NOTE.F3, "te");
            choir2.AddNote(NOTEL.N8, NOTE.F3, "zo");
            choir2.AddNote(NOTEL.N8, NOTE.C3, "ke");
            choir2.AddNote(NOTEL.N8d, NOTE.E3, "sa");
            choir2.AddNote(NOTEL.N16, NOTE.A2, "a");
            choir2.AddNote(NOTEL.N8, NOTE.E3, "wa");
            choir2.AddNote(NOTEL.N8, NOTE.E3, "wa");
            choir2.AddNote(NOTEL.N8d, NOTE.D3, "ka");
            choir2.AddNote(NOTEL.N16, NOTE.As2, "re");
            choir2.AddNote(NOTEL.N8, NOTE.As2, "yu");
            choir2.AddNote(NOTEL.N8, NOTE.As2, "u");
            choir2.AddNote(NOTEL.N4d, NOTE.C3, "ku");
            midi.Write();
            vsq4.Write();
        }

        static void AddMidi(AdlivMidi.MidiTrack track, NOTEL notel, NOTE note)
        {
            track.AddNote(notel, note, 64);
        }
    }
}

///
/// 楽譜については下記サイトを参考にさせていただきました。
/// http://kaisei-music.org/others/hotaru.html
///