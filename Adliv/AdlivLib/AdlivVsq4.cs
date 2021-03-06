﻿using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

///##################################################
///     Vsq4管理クラス
///                                     2018/07/28
///                                     はぅ君
///##################################################
///
namespace AdlivMusic
{
    /// <summary>
    /// Root
    /// </summary>
    [XmlRoot(ElementName = "vsq4", Namespace = "http://www.yamaha.co.jp/vocaloid/schema/vsq4")]
    public class AdlivVsq4
    {
        private string mSavePath = "";      //  保存先
        private int mNextVsTrackNo = 0;     //  次に生成するvsトラック番号
        private string mVsTrackId = "";     //  vsトラックID

        /// <summary>
        /// VoiceChar
        /// 音素変換表
        /// </summary>
        public static Dictionary<string, Vocal> VoiceChar = new Dictionary<string, Vocal>()
        {
            {"a", new Vocal("あ", "a")},     {"i", new Vocal("い", "i")},      {"u", new Vocal("う", "M")},      {"e", new Vocal("え", "e")},     {"o", new Vocal("お", "o")},
            {"ka", new Vocal("か", "k a")},  {"ki", new Vocal("き", "k' i")},  {"ku", new Vocal("く", "k M")},   {"ke", new Vocal("け", "k e")},  {"ko", new Vocal("こ", "k o")},
            {"sa", new Vocal("さ", "s a")},  {"si", new Vocal("し", "S i")},   {"su", new Vocal("す", "k M")},   {"se", new Vocal("せ", "s e")},  {"so", new Vocal("そ", "s o")},
            {"ta", new Vocal("た", "t a")},  {"ti", new Vocal("ち", "tS i")},  {"tu", new Vocal("つ", "ts M")},  {"te", new Vocal("て", "t e")},  {"to", new Vocal("と", "t o")},
            {"na", new Vocal("な", "n a")},  {"ni", new Vocal("に", "J i")},   {"nu", new Vocal("ぬ", "n M")},   {"ne", new Vocal("ね", "n e")},  {"no", new Vocal("の", "n o")},
            {"ha", new Vocal("は", "h a")},  {"hi", new Vocal("ひ", "C i")},   {"hu", new Vocal("ふ", "p\\ M")}, {"he", new Vocal("へ", "h e")},  {"ho", new Vocal("ほ", "h o")},
            {"ma", new Vocal("ま", "m a")},  {"mi", new Vocal("み", "m' i")},  {"mu", new Vocal("む", "m M")},   {"me", new Vocal("め", "m e")},  {"mo", new Vocal("も", "m o")},
            {"ya", new Vocal("や", "j a")},                                    {"yu", new Vocal("ゆ", "j M")},                                    {"yo", new Vocal("よ", "j o")},
            {"ra", new Vocal("ら", "r a")},  {"ri", new Vocal("り", "4' i")},  {"ru", new Vocal("る", "4 M")},   {"re", new Vocal("れ", "4 e")},  {"ro", new Vocal("ろ", "4 o")},
            {"wa", new Vocal("わ", "w a")},                                                                                                       {"wo", new Vocal("を", "o")},
            {"n", new Vocal("ん", "n")},
            {"ga", new Vocal("が", "g a")},  {"gi", new Vocal("ぎ", "g' i")},  {"gu", new Vocal("ぐ", "g M")},   {"ge", new Vocal("げ", "g e")},  {"go", new Vocal("ご", "g o")},
            {"za", new Vocal("ざ", "dz a")}, {"zi", new Vocal("じ", "dZ i")},  {"zu", new Vocal("ず", "dz M")},  {"ze", new Vocal("ぜ", "dz e")}, {"zo", new Vocal("ぞ", "dz o")},
            {"da", new Vocal("だ", "d a")},  {"di", new Vocal("ぢ", "dZ i")},  {"du", new Vocal("づ", "dz M")},  {"de", new Vocal("で", "d e")},  {"do", new Vocal("ど", "d o")},
            {"ba", new Vocal("ば", "b a")},  {"bi", new Vocal("び", "b' i")},  {"bu", new Vocal("ぶ", "b M")},   {"be", new Vocal("べ", "b e")},  {"bo", new Vocal("ぼ", "b o")},
            {"pa", new Vocal("ぱ", "p a")},  {"pi", new Vocal("ぴ", "p' i")},  {"pu", new Vocal("ぷ", "p M")},   {"pe", new Vocal("ぺ", "p e")},  {"po", new Vocal("ぽ", "p o")},
        };

        /// <summary>
        /// Voice
        /// 音素
        /// </summary>
        public class Vocal
        {
            public Vocal()
            {
            }
            public Vocal(string _hiragana, string _pronunce)
            {
                hiragana = _hiragana;
                pronunce = _pronunce;
            }
            public string hiragana;
            public string pronunce;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public AdlivVsq4()
        {
            Initialize();
        }
        public AdlivVsq4(string path, string vVoiceId, string vVoiceName, string vVoiceId2, string vsTrackId, string auxContent)
        {
            mSavePath = path;
            Initialize(vVoiceId, vVoiceName, vVoiceId2, vsTrackId, auxContent);
        }

        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="vVoiceId"></param>
        /// <param name="vVoiceName"></param>
        /// <param name="vVoiceId2"></param>
        /// <param name="vsTrackId"></param>
        /// <param name="auxContent"></param>
        public void Initialize(string vVoiceId = "", string vVoiceName = "", string vVoiceId2 = "", string vsTrackId = "", string auxContent = "")
        {
            vender = new XmlDocument().CreateCDataSection("Yamaha corporation");
            version = new XmlDocument().CreateCDataSection("3.0.0.11");

            vVoiceTable _vVoiceTable = new vVoiceTable();
            mVVoiceTable = _vVoiceTable;

            mixer _mixer = new mixer();
            mMixer = _mixer;

            masterTrack _masterTrack = new masterTrack();
            mMasterTrack = _masterTrack;

            List<vsTrack> _vsTrack = new List<vsTrack>();
            mVsTrack = _vsTrack;

            monoTrack _monoTrack = new monoTrack();
            mMonoTrack = _monoTrack;

            stTrack _stTrack = new stTrack();
            mStTrack = _stTrack;

            aux _aux = new aux();
            mAux = _aux;

            //  情報のセット
            mVVoiceTable.vVoice.SetInfo(vVoiceId, vVoiceName, vVoiceId2);
            mVsTrackId = vsTrackId;
            mAux.SetInfo(auxContent);
        }

        /// <summary>
        /// vsトラックの作成
        /// </summary>
        /// <returns></returns>
        public vsTrack CreateTrack()
        {
            vsTrack track = new vsTrack(mNextVsTrackNo);
            track.vsPart.sPlug.SetInfo(mVsTrackId);
            mVsTrack.Add(track);

            mNextVsTrackNo++;

            return track;
        }
        /// <summary>
        /// XML書き出し
        /// </summary>
        public void Write()
        {
            using(StreamWriter sw = new StreamWriter(mSavePath, false, Encoding.UTF8))
            using(XmlWriter xw = XmlWriter.Create(sw, new XmlWriterSettings{Indent = true}))
            {
                xw.WriteStartDocument(false);

                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance");

                new XmlSerializer(typeof(AdlivVsq4)).Serialize(xw, this, ns);
            }
        }

        [XmlAttribute()]
        public string xsi { get; set; }
        [XmlElement()]
        public XmlCDataSection vender { get; set; }
        [XmlElement()]
        public XmlCDataSection version { get; set; }
        [XmlElement(ElementName="vVoiceTable")]
        public vVoiceTable mVVoiceTable { get; set; }
        [XmlElement(ElementName="mixer")]
        public mixer mMixer { get; set; }
        [XmlElement(ElementName="masterTrack")]
        public masterTrack mMasterTrack { get; set; }
        [XmlElement(ElementName="vsTrack")]
        public List<vsTrack> mVsTrack { get; set; }     //  vsトラック
        [XmlElement(ElementName="monoTrack")]
        public monoTrack mMonoTrack { get; set; }
        [XmlElement(ElementName="stTrack")]
        public stTrack mStTrack { get; set; }
        [XmlElement(ElementName="aux")]
        public aux mAux { get; set; }

        /// <summary>
        /// vVoiceTable
        /// vsq4 - vVoiceTable
        /// </summary>
        public class vVoiceTable
        {
            public vVoiceTable()
            {
                vVoice _vVoice = new vVoice();
                vVoice = _vVoice;
            }

            [XmlElement()]
            public vVoice vVoice { get; set; }
        }

        /// <summary>
        /// vVoice
        /// vsq4 - vVoiceTable - vVoice
        /// </summary>
        public class vVoice
        {
            public vVoice()
            {
                vPrm _vPrm = new vPrm();
                vPrm = _vPrm;

                vPrm2 _vPrm2 = new vPrm2();
                vPrm2 = _vPrm2;

                pc = 1;
            }

            public void SetInfo(string _id, string _name, string _id2)
            {
                id = new XmlDocument().CreateCDataSection(_id);
                name = new XmlDocument().CreateCDataSection(_name);
                id2 = new XmlDocument().CreateCDataSection(_id2);
            }

            [XmlElement()]
            public int bs { get; set; }
            [XmlElement()]
            public int pc { get; set; }
            [XmlElement()]
            public XmlCDataSection id { get; set; }
            [XmlElement()]
            public XmlCDataSection name { get; set; }
            [XmlElement()]
            public vPrm vPrm { get; set; }
            [XmlElement()]
            public XmlCDataSection id2 { get; set; }
            [XmlElement()]
            public vPrm2 vPrm2 { get; set; }
        }

        /// <summary>
        /// vPrm
        /// vsq4 - vVoiceTable - vVoice - vPrm
        /// </summary>
        public class vPrm
        {
            [XmlElement()]
            public int bre { get; set; }
            [XmlElement()]
            public int bri { get; set; }
            [XmlElement()]
            public int cle { get; set; }
            [XmlElement()]
            public int gen { get; set; }
            [XmlElement()]
            public int ope { get; set; }

        }

        /// <summary>
        /// vPrm2
        /// vsq4 - vVoiceTable - vVoice - vPrm2
        /// </summary>
        public class vPrm2
        {
            [XmlElement()]
            public int bre { get; set; }
            [XmlElement()]
            public int bri { get; set; }
            [XmlElement()]
            public int cle { get; set; }
            [XmlElement()]
            public int gen { get; set; }
            [XmlElement()]
            public int ope { get; set; }
            [XmlElement()]
            public int vol { get; set; }
        }

        /// <summary>
        /// mixer
        /// vsq4 - mixer
        /// </summary>
        public class mixer
        {
            public mixer()
            {
                masterUnit _masterUnit = new masterUnit();
                masterUnit = _masterUnit;

                vsUnit _vsUnit = new vsUnit();
                vsUnit = _vsUnit;

                monoUnit _monoUnit = new monoUnit();
                monoUnit = _monoUnit;

                stUnit _stUnit = new stUnit();
                stUnit = _stUnit;
            }
            [XmlElement()]
            public masterUnit masterUnit { get; set; }
            [XmlElement()]
            public vsUnit vsUnit { get; set; }
            [XmlElement()]
            public monoUnit monoUnit { get; set; }
            [XmlElement()]
            public stUnit stUnit { get; set; }
        }

        /// <summary>
        /// masterUnit
        /// vsq4 - mixer - masterUnit
        /// </summary>
        public class masterUnit
        {
            [XmlElement()]
            public int oDev { get; set; }
            [XmlElement()]
            public int rLvl { get; set; }
            [XmlElement()]
            public int vol { get; set; }
        }

        /// <summary>
        /// vsUnit
        /// vsq4 - mixer - vsUnit
        /// </summary>
        public class vsUnit
        {
            public vsUnit()
            {
                sLvl = -898;
                pan = 64;
            }

            [XmlElement()]
            public int tNo { get; set; }
            [XmlElement()]
            public int iGin { get; set; }
            [XmlElement()]
            public int sLvl { get; set; }
            [XmlElement()]
            public int sEnable { get; set; }
            [XmlElement()]
            public int m { get; set; }
            [XmlElement()]
            public int s { get; set; }
            [XmlElement()]
            public int pan { get; set; }
            [XmlElement()]
            public int vol { get; set; }
        }

        /// <summary>
        /// monoUnit
        /// vsq4 - mixer - monoUnit
        /// </summary>
        public class monoUnit
        {
            public monoUnit()
            {
                sLvl = -898;
                pan = 64;
            }
            [XmlElement()]
            public int iGin { get; set; }
            [XmlElement()]
            public int sLvl { get; set; }
            [XmlElement()]
            public int sEnable { get; set; }
            [XmlElement()]
            public int m { get; set; }
            [XmlElement()]
            public int s { get; set; }
            [XmlElement()]
            public int pan { get; set; }
            [XmlElement()]
            public int vol { get; set; }
        }

        /// <summary>
        /// stUnit
        /// vsq4 - mixer - stUnit
        /// </summary>
        public class stUnit
        {
            public stUnit()
            {
                vol = -129;
            }
            [XmlElement()]
            public int iGin { get; set; }
            [XmlElement()]
            public int m { get; set; }
            [XmlElement()]
            public int s { get; set; }
            [XmlElement()]
            public int vol { get; set; }
        }

        /// <summary>
        /// masterTrack
        /// vsq4 - masterTrack
        /// </summary>
        public class masterTrack
        {
            public masterTrack()
            {
                seqName = new XmlDocument().CreateCDataSection("none");
                comment = new XmlDocument().CreateCDataSection("none");
                resolution = 480;
                preMeasure = 4;
                List<timeSig> _timeSig = new List<timeSig>();
                timeSig = _timeSig;

                timeSig timeSig1 = new timeSig();
                timeSig.Add(timeSig1);
                timeSig timeSig2 = new timeSig(4);
                timeSig.Add(timeSig2);

                List<tempo> _tempo = new List<tempo>();
                tempo = _tempo;

                tempo tempo1 = new tempo();
                tempo.Add(tempo1);
                tempo tempo2 = new tempo(7680);
                tempo.Add(tempo2);
            }
            [XmlElement()]
            public XmlCDataSection seqName { get; set; }
            [XmlElement()]
            public XmlCDataSection comment { get; set; }
            [XmlElement()]
            public int resolution { get; set; }
            [XmlElement()]
            public int preMeasure { get; set; }
            [XmlElement()]
            public List<timeSig> timeSig { get; set; }
            [XmlElement()]
            public List<tempo> tempo { get; set; }
        }

        /// <summary>
        /// timeSig
        /// vsq4 - masterTrack - timeSig
        /// </summary>
        public class timeSig
        {
            public timeSig()
            {
                Initialize();
            }
            public timeSig(int _m)
            {
                Initialize(_m);
            }

            public void Initialize(int _m = 0)
            {
                m = _m;
                nu = 4;
                de = 4;
            }
            [XmlElement()]
            public int m { get; set; }
            [XmlElement()]
            public int nu { get; set; }
            [XmlElement()]
            public int de { get; set; }
        }

        /// <summary>
        /// tempo
        /// vsq4 - masterTrack - tempo
        /// </summary>
        public class tempo
        {
            public tempo()
            {
                Initialize();
            }
            public tempo(int _t = 0)
            {
                Initialize(_t);
            }

            public void Initialize(int _t = 0)
            {
                t = _t;
                v = 12000;
            }
            [XmlElement()]
            public int t { get; set; }
            [XmlElement()]
            public int v { get; set; }
        }

        /// <summary>
        /// vsTrack
        /// vsq4 - vsTrack
        /// </summary>
        public class vsTrack
        {
            public vsTrack()
            {
                Initialize();
            }
            public vsTrack(int _tNo)
            {
                Initialize(_tNo);
            }
            public void Initialize(int _tNo = 0)
            {
                tNo = _tNo;
                name = new XmlDocument().CreateCDataSection("MIKU_V4X_Original_EVEC");
                comment = new XmlDocument().CreateCDataSection("Track");

                vsPart _vsPart = new vsPart();
                vsPart = _vsPart;

            }

            /// <summary>
            /// 音符の追加
            /// </summary>
            /// <param name="_dur"></param>
            /// <param name="_n"></param>
            /// <param name="_voice"></param>
            public void AddNote(NOTEL _dur, NOTE _n, string _voice)
            {
                vsPart.AddNote(new note(_dur, _n, _voice));
            }

            /// <summary>
            /// 休符の追加
            /// </summary>
            /// <param name="_dur"></param>
            public void AddRest(NOTEL _dur)
            {
                vsPart.AddRest((int)_dur);
            }

            [XmlElement()]
            public int tNo { get; set; }
            [XmlElement()]
            public XmlCDataSection name { get; set; }
            [XmlElement()]
            public XmlCDataSection comment { get; set; }
            [XmlElement()]
            public vsPart vsPart { get; set; }
        }

        /// <summary>
        /// vsPart
        /// vsq4 - vsTrack - vsPart
        /// </summary>
        public class vsPart
        {
            public vsPart()
            {
                mNowT = 0;

                t = 7680;
                playTime = 0;
                name = new XmlDocument().CreateCDataSection("MIKU_V4X_Original_EVEC");
                comment = new XmlDocument().CreateCDataSection("");
                
                sPlug _sPlug = new sPlug();
                sPlug = _sPlug;
                pStyle _pStyle = new pStyle();
                pStyle = _pStyle;
                singer _singer = new singer();
                singer = _singer;
                List<note> _note = new List<note>();
                note = _note;
            }

            /// <summary>
            /// 音符の追加
            /// </summary>
            /// <param name="_note"></param>
            public void AddNote(note _note)
            {
                _note.t = mNowT;
                note.Add(_note);
                playTime += _note.dur;
                mNowT += _note.dur;
            }

            /// <summary>
            /// 休符の追加
            /// </summary>
            /// <param name="duration"></param>
            public void AddRest(int duration)
            {
                mNowT += duration;
                playTime += duration;
            }

            private int mNowT;

            [XmlElement()]
            public int t { get; set; }
            [XmlElement()]
            public int playTime { get; set; }
            [XmlElement()]
            public XmlCDataSection name { get; set; }
            [XmlElement()]
            public XmlCDataSection comment { get; set; }
            [XmlElement()]
            public sPlug sPlug { get; set; }
            [XmlElement()]
            public pStyle pStyle { get; set; }
            [XmlElement()]
            public singer singer { get; set; }
            [XmlElement()]
            public List<note> note { get; set; }
            [XmlElement()]
            public int plane { get; set; }
        }

        /// <summary>
        /// sPlug
        /// vsq4 - vsTrack - sPlug
        /// </summary>
        public class sPlug
        {
            public sPlug()
            {
                name = new XmlDocument().CreateCDataSection("VOCALOID2 Compatible Style");
                version = new XmlDocument().CreateCDataSection("3.0.0.1");
            }

            public void SetInfo(string _id)
            {
                id = new XmlDocument().CreateCDataSection(_id);
            }
            [XmlElement()]
            public XmlCDataSection id { get; set; }
            [XmlElement()]
            public XmlCDataSection name { get; set; }
            [XmlElement()]
            public XmlCDataSection version { get; set; }
        }

        /// <summary>
        /// PStyle
        /// vsq4 - vsTrack - vsPart - pStyle
        /// </summary>
        public class pStyle
        {
            public pStyle()
            {
                List<v> _v = new List<v>();
                v = _v;

                v.Add(new v("accent", 50));
                v.Add(new v("bendDep", 8));
                v.Add(new v("bendLen", 0));
                v.Add(new v("decay", 50));
                v.Add(new v("fallPort", 0));
                v.Add(new v("opening", 127));
                v.Add(new v("risePort", 0));
            }

            [XmlElement()]
            public List<v> v { get; set; }
        }

        /// <summary>
        /// v
        /// vsq4 - vsTrack - vsPart - pStyle - v
        /// </summary>
        public class v
        {
            public v()
            {
                Initialize();
            }
            public v(string _id, int _text)
            {
                Initialize(_id, _text);
            }
            public void Initialize(string _id = "", int _text = 0)
            {
                id = _id;
                text = _text;
            }

            [XmlAttribute(AttributeName = "id")]
            public string id { get; set; }
            [XmlText()]
            public int text { get; set; }
        }

        /// <summary>
        /// singer
        /// vsq4 - vsTrack - vsPart - singer
        /// </summary>
        public class singer
        {
            public singer()
            {
                t = 0;
                bs = 0;
                pc = 1;
            }
            [XmlElement()]
            public int t { get; set; }
            [XmlElement()]
            public int bs { get; set; }
            [XmlElement()]
            public int pc { get; set; }
        }

        /// <summary>
        /// note
        /// vsq4 - vsTrack - vsPart - note
        /// </summary>
        public class note
        {
            public note()
            {
                Initialize();
            }
            public note(NOTEL _dur, NOTE _n, string voice)
            {
                Initialize(_dur, _n, voice);
            }
            public void Initialize(NOTEL _dur = NOTEL.N1, NOTE _n = NOTE.x, string voice = "")
            {
                dur = (int)_dur;
                n = (int)_n;
                v = 64;
                y = new XmlDocument().CreateCDataSection(VoiceChar[voice].hiragana);
                p = new XmlDocument().CreateCDataSection(VoiceChar[voice].pronunce);

                nStyle _nStyle = new nStyle();
                nStyle = _nStyle;
            }

            [XmlElement()]
            public  int t { get; set; }     //  開始位置
            [XmlElement()]
            public int dur { get; set; }    //  長さ
            [XmlElement()]
            public int n { get; set; }     //  音程
            [XmlElement()]
            public int v { get; set; }
            [XmlElement()]
            public XmlCDataSection y { get; set; }
            [XmlElement()]
            public XmlCDataSection p { get; set; }
            [XmlElement()]
            public nStyle nStyle { get; set; }
        }

        /// <summary>
        /// nStyle
        /// vsq4 - vsTrack - vsPart - note - nStyle
        /// </summary>
        public class nStyle
        {
            public nStyle()
            {
                List<v> _v = new List<v>();
                v = _v;

                v.Add(new v("accent", 50));
                v.Add(new v("bendDep", 8));
                v.Add(new v("bendLen", 0));
                v.Add(new v("decay", 50));
                v.Add(new v("fallPort", 0));
                v.Add(new v("opening", 127));
                v.Add(new v("risePort", 0));
                v.Add(new v("vibLen", 70));
                v.Add(new v("vibType", 1));

                List<seq> _seq = new List<seq>();
                seq = _seq;

                seq.Add(new seq("vibDep", 19661, 64));
                seq.Add(new seq("vibRate", 19661, 50));
            }
            [XmlElement()]
            public List<v> v { get; set; }
            [XmlElement()]
            public List<seq> seq { get; set; }
        }

        /// <summary>
        /// seq
        /// vsq4 - vsTrack - vsPart - note - nStyle - seq
        /// </summary>
        public class seq
        {
            public seq()
            {
                Initialize();
            }
            public seq(string _id, int _p, int _v)
            {
                Initialize(_id, _p, _v);
            }
            public void Initialize(string _id = "", int _p = 0, int _v = 0)
            {
                id = _id;
                cc = new cc(_p, _v);
            }

            [XmlAttribute(AttributeName = "id")]
            public string id { get; set; }
            [XmlElement()]
            public cc cc { get; set; }
        }

        /// <summary>
        /// cc
        /// vsq4 - vsTrack - vsPart - note - nStyle - seq - cc
        /// </summary>
        public class cc
        {
            public cc()
            {
                Initialize();
            }
            public cc(int _p, int _v)
            {
                Initialize(_p, _v);
            }
            public void Initialize(int _p = 0, int _v = 0)
            {
                p = _p;
                v = _v;
            }

            [XmlElement()]
            public int p { get; set; }
            [XmlElement()]
            public int v { get; set; }
        }

        /// <summary>
        /// monoTrack
        /// vsq4 - monoTrack
        /// </summary>
        public class monoTrack
        {
        }

        /// <summary>
        /// stTrack
        /// vsq4 - stTrack
        /// </summary>
        public class stTrack
        {
        }

        /// <summary>
        /// aux
        /// vsq4 - aux
        /// </summary>
        public class aux
        {
            public aux()
            {
                id = new XmlDocument().CreateCDataSection("AUX_VST_HOST_CHUNK_INFO");
                
            }
            public void SetInfo(string _content)
            {
                content = new XmlDocument().CreateCDataSection(_content);
            }

            [XmlElement()]
            public XmlCDataSection id { get; set; }
            [XmlElement()]
            public XmlCDataSection content { get; set; }
        }
    }
}
