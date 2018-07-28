using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

///##################################################
///     Midi管理クラス
///                                     2018/07/28
///                                     はぅ君
///##################################################

namespace AdlivMusic
{
    public class AdlivMidi
    {
        private string mSavePath;                           //  保存先
        List<byte> mWriteByteBuffer = new List<byte>();     //  書き込みバイトバッファ

        int mNextTrackNo = 0;                               //  次に生成するトラック番号
        List<MidiTrack> mTrackList = new List<MidiTrack>(); //  生成したトラックリスト

        int mTempo = 120;

        public enum TONE
        {
            T001_PIANO_ACOUSTIC_PIANO,
            T002_PIANO_BRIGHT_PIANO,
            T003_PIANO_ELECTRIC_GRAND_PIANO,
            T004_PIANO_HONKY_TONK_PIANO,
            T005_PIANO_ELECTRIC_PIANO,
            T006_PIANO_ELECTRIC_PIANO2,
            T007_PIANO_HARPSICHORD,
            T008_PIANO_CLAVI,
            T009_PERCUSSION_CELESTA,
            T010_PERCUSSION_GLOCKENSPIEL,
            T011_PERCUSSION_MUSICAL_BOX,
            T012_PERCUSSION_VIBRAPHONE,
            T013_PERCUSSION_MARIMBA,
            T014_PERCUSSION_XYLOPHONE,
            T015_PERCUSSION_TUBULAR_BELL,
            T016_PERCUSSION_DULCIMER,
            T017_ORGAN_DRAWBAR_ORGAN,
            T018_ORGAN_PERCUSSIVE_ORGAN,
            T019_ORGAN_ROCK_ORGAN,
            T020_ORGAN_CHURCH_ORGAN,
            T021_ORGAN_REED_ORGAN,
            T022_ORGAN_ACCORDION_ORGAN,
            T023_ORGAN_HARMONICA,
            T024_ORGAN_TANGO_ACCORDION,
            T025_GUITAR_ACOUSTIC_GUITAR_NYLON,
            T026_GUITAR_ACOUSTIC_GUITAR_STEEL,
            T027_GUITAR_ELECTRIC_GUITAR_JAZZ,
            T028_GUITAR_ELECTRIC_GUITAR_CLEAN,
            T029_GUITAR_ELECTRIC_GUITAR_MUTED,
            T030_GUITAR_OVERDRIVEN_GUITAR,
            T031_GUITAR_DISTORTIION_GUITAR,
            T032_GUITAR_GUITAR_HARMONICS,
            T033_BASS_ACOUSTIC_BASS,
            T034_BASS_ELECTRIC_BASS_FINGER,
            T035_BASS_ELECTRIC_BASS_PICK,
            T036_BASS_FRATLESS_BASS,
            T037_BASS_SLAP_BASS1,
            T038_BASS_SLAP_BASS2,
            T039_BASS_SYNTH_BASS1,
            T040_BASS_SYNTH_BASS2,
            T041_STRINGS_VIOLIN,
            T042_STRINGS_VIOLA,
            T043_STRINGS_CELLO,
            T044_STRINGS_DOUBLE_BASS,
            T045_STRINGS_TREMOLO_STRINGS,
            T046_STRINGS_PIZZICATO_STRINGS,
            T047_STRINGS_ORCHESTRAL_HARP,
            T048_STRINGS_TIMPANI,
            T049_ENSEMBLE_STRING_ENSEMBLE1,
            T050_ENSEMBLE_STRING_ENSEMBLE2,
            T051_ENSEMBLE_SYNTH_STRINGS1,
            T052_ENSEMBLE_SYNTH_STRINGS2,
            T053_ENSEMBLE_VOICE_AAHS,
            T054_ENSEMBLE_VOICE_OOHS,
            T055_ENSEMBLE_SYNTH_VOICE,
            T056_ENSEMBLE_ORCHESTRA_HIT,
            T057_BRASS_TRUMPET,
            T058_BRASS_TROMBONE,
            T059_BRASS_TUBA,
            T060_BRASS_MUTED_TRUMPET,
            T061_BRASS_FRENCH_HORN,
            T062_BRASS_BRASS_SECTION,
            T063_BRASS_SYNTH_BRASS1,
            T064_BRASS_SYNTH_BRASS2,
            T065_REED_SOPRANO_SAX,
            T066_REED_ALTO_SAX,
            T067_REED_TENOR_SAX,
            T068_REED_BARITONE_SAX,
            T069_REED_OBOE,
            T070_REED_ENGLISH_HORN,
            T071_REED_BASSOON,
            T072_REED_CLARINET,
            T073_PIPE_PICCOLO,
            T074_PIPE_FLUTE,
            T075_PIPE_RECORDER,
            T076_PIPE_PAN_FLUTE,
            T077_PIPE_BLOWN_BOTTLE,
            T078_PIPE_SHAKUHACHI,
            T079_PIPE_WHISTLE,
            T080_PIPE_OCARINA,
            T081_SYNTH_LEAD_SQUARE,
            T082_SYNTH_LEAD_SAWTOOTH,
            T083_SYNTH_LEAD_CALLIOPE,
            T084_SYNTH_LEAD_CHIFF,
            T085_SYNTH_LEAD_CHARANG,
            T086_SYNTH_LEAD_VOICE,
            T087_SYNTH_LEAD_FIFTHS,
            T088_SYNTH_LEAD_BASSLEAD,
            T089_SYNTH_PAD_FANTASIA,
            T090_SYNTH_PAD_WARM,
            T091_SYNTH_PAD_POLYSYNTH,
            T092_SYNTH_PAD_CHOIR,
            T093_SYNTH_PAD_BOWED,
            T094_SYNTH_PAD_METALLIC,
            T095_SYNTH_PAD_HALO,
            T096_SYNTH_PAD_SWEEP,
            T097_SYNTH_EFFECTS_RAIN,
            T098_SYNTH_EFFECTS_SOUNDTRACK,
            T099_SYNTH_EFFECTS_CRYSTAL,
            T100_SYNTH_EFFECTS_ATMOSPHERE,
            T101_SYNTH_EFFECTS_BRIGHTNESS,
            T102_SYNTH_EFFECTS_GOBLINS,
            T103_SYNTH_EFFECTS_ECHOES,
            T104_SYNTH_EFFECTS_SCIFI,
            T105_ETHNIC_SITAR,
            T106_ETHNIC_BANJO,
            T107_ETHNIC_SHAMISEN,
            T108_ETHNIC_KOTO,
            T109_ETHNIC_KALIMBA,
            T110_ETHNIC_BAGPIPE,
            T111_ETHNIC_FIDDLE,
            T112_ETHNIC_SHANAI,
            T113_PERCUSSIVE_TINKLE_BELL,
            T114_PERCUSSIVE_AGOGO,
            T115_PERCUSSIVE_STEEL_DRUMS,
            T116_PERCUSSIVE_WOODBLOCK,
            T117_PERCUSSIVE_TAIKO_DRUM,
            T118_PERCUSSIVE_MELODIC_TOM,
            T119_PERCUSSIVE_SYNTH_DRUM,
            T120_PERCUSSIVE_REVERSE_CYMBAL,
            T121_SOUND_EFFECTS_GUITAR_FRET_NOISE,
            T122_SOUND_EFFECTS_BREATH_NOISE,
            T123_SOUND_EFFECTS_SEASHORE,
            T124_SOUND_EFFECTS_BIRD_TWEET,
            T125_SOUND_EFFECTS_TELEPHONE_RING,
            T126_SOUND_EFFECTS_HELICOPTER,
            T127_SOUND_EFFECTS_APPLAUSE,
            T128_SOUND_EFFECTS_GUNSHOT,
        };

        public enum PNOTE
        {
            x = 35,
            BASS_DRUM1, SIDE_STICK, SNARE_DRUM1, HAND_CLAP, SNARE_DRUM2, LOW_TOM2, 
            CLOSES_HIHAT, LOW_TOM1, PEDAL_HIHAT, MID_TOM2, OPEN_HIHAT, MID_TOM1,
            HIGH_TOM2, CRASH_CYMBAL1, HIGH_TOM1, RIDE_CYMBAL1, CHINESE_CYMBAL, RIDE_BELL, 
            TAMBOURINE, SPLASH_CYMBAL, COWBELL, CRASH_CYMBAL2, VIBRA_SLAP, RIDE_CYMBAL2,
            HIGH_BONGO, LOW_BONGO, MUTE_HIGH_CONGA, OPEN_HIGH_CONGA, LOW_CONGA, HIGH_TIMBALE,
            LOW_TIMBALE, HIGH_AGOGO, LOW_AGOGO, CABASA, MARACAS, SHORT_WHISTLE,
            LONG_WHISTLE, SHORT_GUIRO, LONG_GUIRO, CLAVES, HIGH_WOOD_BLOCK, LOW_WOOD_BLOCK,
            MUTE_CUICA, OPEN_CUICA, MUTE_TRIANGLE, OPEN_TRIANGLE
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="path"></param>
        public AdlivMidi(string path)
        {
            mSavePath = path;
        }

        public void SetTempo(int tempo)
        {
            mTempo = tempo;
        }

        /// <summary>
        /// トラックの生成
        /// </summary>
        /// <param name="tone"></param>
        /// <returns></returns>
        public MidiTrack CreateTrack(TONE tone)
        {
            MidiTrack track = new MidiTrack(mNextTrackNo, tone);

            mTrackList.Add(track);
            mNextTrackNo++;

            return track;
        }

        /// <summary>
        /// ヘッダーチャンクの生成
        /// </summary>
        private void CreateHeaderChunk()
        {
            //  チャンクタイプ
            mWriteByteBuffer.Add(0x4D); //  M
            mWriteByteBuffer.Add(0x54); //  T
            mWriteByteBuffer.Add(0x68); //  h
            mWriteByteBuffer.Add(0x64); //  d

            //  チャンクデータ長
            mWriteByteBuffer.Add(0x00);
            mWriteByteBuffer.Add(0x00);
            mWriteByteBuffer.Add(0x00);
            mWriteByteBuffer.Add(0x06);

            //  フォーマット
            mWriteByteBuffer.Add(0x00);
            mWriteByteBuffer.Add(0x00);

            //  トラック数
            mWriteByteBuffer.Add(0x00);
            mWriteByteBuffer.Add(0x01);

            //  時間単位
            mWriteByteBuffer.Add(0x01);
            mWriteByteBuffer.Add(0xE0);
        }

        /// <summary>
        /// メタヘッダーの生成
        /// </summary>
        private void CreateMetaHeader()
        {
            //  トラックチャンク
            mWriteByteBuffer.Add(0x4D); //  M
            mWriteByteBuffer.Add(0x54); //  T
            mWriteByteBuffer.Add(0x72); //  r
            mWriteByteBuffer.Add(0x6B); //  k

            //  データ長
            mWriteByteBuffer.Add(0x00); //  ここは書き込み時に埋める
            mWriteByteBuffer.Add(0x00);
            mWriteByteBuffer.Add(0x00);
            mWriteByteBuffer.Add(0x00);

            //  シーケンス名・トラック名
            mWriteByteBuffer.Add(0x00); //  デルタタイム
            mWriteByteBuffer.Add(0xFF);
            mWriteByteBuffer.Add(0x03);
            mWriteByteBuffer.Add(0x00);

            //  拍子
            mWriteByteBuffer.Add(0x00); //  デルタタイム
            mWriteByteBuffer.Add(0xFF);
            mWriteByteBuffer.Add(0x58);
            mWriteByteBuffer.Add(0x04); 
            mWriteByteBuffer.Add(0x04); //  分子
            mWriteByteBuffer.Add(0x02); //  分母（2のべき乗）
            mWriteByteBuffer.Add(0x18); //  メトロノーム間隔
            mWriteByteBuffer.Add(0x08); //  四分音符辺りの32分音符の数

            //  テンポ
            byte[] tempoNanoSec = BitConverter.GetBytes((int)((60.0 / mTempo) * 1000000));    //  60秒間に120拍 ＝ 0.5秒/拍
            byte[] tempoBytes = new byte[3];
            tempoBytes[0] = tempoNanoSec[2];
            tempoBytes[1] = tempoNanoSec[1];
            tempoBytes[2] = tempoNanoSec[0];
            mWriteByteBuffer.Add(0x00); //  デルタタイム
            mWriteByteBuffer.Add(0xFF);
            mWriteByteBuffer.Add(0x51);
            mWriteByteBuffer.Add(0x03);
            mWriteByteBuffer.AddRange(tempoBytes);   //  四分音符の長さ（マイクロ秒）

            //  コントロールチェンジ
            for (int i = 0; i < mNextTrackNo; i++)
            {
                mWriteByteBuffer.Add(0x00);
                mWriteByteBuffer.Add((byte)(0xB0 | i));
                mWriteByteBuffer.Add(0x00);
                mWriteByteBuffer.Add(0x00);
                mWriteByteBuffer.Add(0x00);
                mWriteByteBuffer.Add((byte)(0xB0 | i));
                mWriteByteBuffer.Add(0x20);
                mWriteByteBuffer.Add(0x00);
            }

            //  音色のセット
            foreach (MidiTrack track in mTrackList)
            {
                mWriteByteBuffer.Add(0x00);
                mWriteByteBuffer.Add((byte)(0xC0 | track.GetTrackNo()));
                mWriteByteBuffer.Add((byte)(track.GetTone()));
            }
        }

        /// <summary>
        /// フッターの作成
        /// </summary>
        public void CreateFooter()
        {
            //  終了
            mWriteByteBuffer.Add(0x00);
            mWriteByteBuffer.Add(0xFF);
            mWriteByteBuffer.Add(0x2F);
            mWriteByteBuffer.Add(0x00);
        }

        /// <summary>
        /// Midiトラック
        /// </summary>
        public class MidiTrack
        {
            TONE tone;  //  音色
            List<MidiNote> noteList = new List<MidiNote>(); //  音符リスト
            int mDelta = 0;     //  現在のデルタタイム
            int mTrackNo = 0;   //  トラック番号

            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="_trackNo"></param>
            /// <param name="_tone"></param>
            public MidiTrack(int _trackNo, TONE _tone)
            {
                mTrackNo = _trackNo;
                tone = _tone;
            }

            /// <summary>
            /// 音符の追加
            /// </summary>
            /// <param name="notel"></param>
            /// <param name="note"></param>
            /// <param name="vel"></param>
            public void AddNote(NOTEL notel, NOTE note, int vel)
            {
                //  発音
                noteList.Add(new MidiNote(mDelta, notel, note, vel));
                mDelta += (int)notel;

                //  消音
                noteList.Add(new MidiNote(mDelta, NOTEL.N4, note, 0));
            }
            public void AddNote(NOTEL notel, PNOTE note, int vel)
            {
                //  発音
                noteList.Add(new MidiNote(mDelta, notel, (NOTE)note, vel));
                mDelta += (int)notel;

                //  消音
                noteList.Add(new MidiNote(mDelta, NOTEL.N4, (NOTE)note, 0));
            }
            
            public int GetTrackNo()
            {
                return mTrackNo;
            }
            public TONE GetTone()
            {
                return tone;
            }
            public int GetNextDelta()
            {
                int result = -1;

                if (noteList.Count > 0)
                {
                    result = noteList[0].GetDelta();
                }

                return result;
            }
            public byte[] GetNextNote(int delta = 0)
            {
                List<byte> bytes = new List<byte>();

                if (noteList.Count > 0)
                {
                    MidiNote note = noteList[0];

                    bytes.AddRange(GetDeltaTime(delta));
                    bytes.Add((byte)(0x90 | mTrackNo)); //  発音
                    bytes.Add((byte)note.GetNote()); //  音程
                    bytes.Add((byte)note.GetVel()); //  ベロシティ

                    noteList.RemoveAt(0);
                }
                return bytes.ToArray();
            }
            public int GetNoteCount()
            {
                return noteList.Count;
            }
        }

        private class MidiNote
        {
            int delta;
            NOTEL notel;
            NOTE note;
            int vel;
            public MidiNote(int _delta, NOTEL _notel, NOTE _note, int _vel)
            {
                note = _note;
                delta = _delta;
                notel = _notel;
                vel = _vel;
            }
            public int GetDelta()
            {
                return delta;
            }
            public NOTE GetNote()
            {
                return note;
            }
            public int GetVel()
            {
                return vel;
            }
        }


        static public byte[] GetDeltaTime(int time)
        {
            byte[] result = new byte[0];
            byte[] temp = BitConverter.GetBytes(time);
            byte[] bytes = new byte[4];

            //  計算しやすいように4バイト枠に収める
            for (int i = 0; i < temp.Length; i++)
            {
                bytes[3 - i] = temp[i];
            }

            //  バイト割り当て
            if (time >= 0x200000)   //  可変長4バイト
            {
                result = new byte[4];
                result[0] = (byte)((0x80 | temp[0] << 3) + temp[1] >> 5);
                result[1] = (byte)((0x80 | temp[1] << 2) + temp[2] >> 6);
                result[2] = (byte)((0x80 | temp[2] << 1) + temp[3] >> 7);
                result[3] = (byte)(0x7F & temp[3]);
            }
            else if (time >= 0x4000)    //  可変長3バイト
            {
                result = new byte[3];
                result[0] = (byte)((0x80 | temp[1] << 2) + temp[2] >> 6);
                result[1] = (byte)((0x80 | temp[2] << 1) + temp[3] >> 7);
                result[2] = (byte)(0x7F & temp[3]);
            }
            else if (time >= 0x80)  //  可変長2バイト
            {
                result = new byte[2];
                result[0] = (byte)((0x80 | bytes[2] << 1) | bytes[3] >> 7);
                result[1] = (byte)(0x7F & bytes[3]);
            }
            else  //    可変長1バイト
            {
                result = new byte[1];
                result[0] = bytes[3];
            }

            return result;
        }

        public void Write()
        {
            CreateHeaderChunk();
            CreateMetaHeader();

            int preDelta = 0;
            while (mTrackList.Count > 0)
            {
                //  NextDelta
                int minDelta = int.MaxValue;
                foreach (MidiTrack track in mTrackList)
                {
                    int tmpDelta = track.GetNextDelta();
                    if (tmpDelta < minDelta)
                    {
                        minDelta = tmpDelta;
                    }
                }

                bool isFirst = true;
                List<MidiTrack> removeList = new List<MidiTrack>();
                //  Note
                foreach (MidiTrack track in mTrackList)
                {
                    while (track.GetNextDelta() == minDelta && minDelta != -1)
                    {
                        int nowDelta = 0;
                        if (isFirst)
                        {
                            nowDelta = track.GetNextDelta() - preDelta;
                            isFirst = false;
                            preDelta = track.GetNextDelta();
                        }

                        mWriteByteBuffer.AddRange(track.GetNextNote(nowDelta));
                        nowDelta = 0;
                    }

                    if (track.GetNoteCount() == 0)
                    {
                        removeList.Add(track);
                    }
                }

                foreach (MidiTrack track in removeList)
                {
                    mTrackList.Remove(track);
                }
            }

            CreateFooter();

            byte[] size = BitConverter.GetBytes(mWriteByteBuffer.Count() - 22);

            mWriteByteBuffer[18] = size[3];
            mWriteByteBuffer[19] = size[2];
            mWriteByteBuffer[20] = size[1];
            mWriteByteBuffer[21] = size[0];

            FileStream fs = new FileStream(mSavePath, FileMode.Create, FileAccess.Write);
            fs.Write(mWriteByteBuffer.ToArray(), 0, mWriteByteBuffer.Count());
        }
    }
}
