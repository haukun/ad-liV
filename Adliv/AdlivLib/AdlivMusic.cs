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
    /// <summary>
    /// NOTE
    /// 音程
    /// </summary>
    public enum NOTE
    {
        x = 35,
        C1, Cs1, D1, Ds1, E1, F1, Fs1, G1, Gs1, A1, As1, B1,
        C2, Cs2, D2, Ds2, E2, F2, Fs2, G2, Gs2, A2, As2, B2,
        C3, Cs3, D3, Ds3, E3, F3, Fs3, G3, Gs3, A3, As3, B3,
        C4, Cs4, D4, Ds4, E4, F4, Fs4, G4, Gs4, A4, As4, B4,
        C5, Cs5, D5, Ds5, E5, F5, Fs5, G5, Gs5, A5, As5, B5,
        C6,
    }

    public enum CHORD
    {
        C, C7,
        D7, Dm, Dm7,
        E, E7, Em, Em7,
        F, FM7, Fm,
        G, Gsus4, G7,
        A, Am, Am7,
    }

    /// <summary>
    /// NOTEL
    /// 音の長さ
    /// </summary>
    public enum NOTEL
    {
        N1x2 = 3840,      //  全音符×二小節
        N1 = 1920,      //  全音符
        N2 = 960,       //  二分音符
        N4d = 720,      //  符点四分音符
        N3 = 640,       //  三連二部音符
        N4 = 480,       //  四分音符
        N8d = 360,      //  符点八分音符
        N6 = 320,       //  三連四部音符
        N8 = 240,       //  八分音符
        N16 = 120,      //  十六分音符
    }
}
