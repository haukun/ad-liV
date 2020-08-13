# ad-liV
**A**utomatic **D**emonstration with **L**yric and **I**nfinity **V**oice.

いわゆるmidiファイル（SMF）とボーカロイドシーケンスファイル（VSQ）をプログラムから作ることができます。

作者のテーマ「乱数と定数の間」を音楽に適用した結果、GenrativeMusicをやりたくなったので作ったものです。


# 履歴
- Ver.0.1(2018/07/28)
    - 初版

# 使い方

## 環境
- VisualStudio2010 C#

 - そのうち最新のVisualStudioに移行します。

## プロジェクトの参照
- 適当なプロジェクトを作成し、AdlivMusicプロジェクトを追加して、自身のプロジェクトの「参照設定」でAdlivMusicプロジェクトを参照してください。

## 名前空間の追加
- 下記をプログラムの先頭に記述してください。
~~~CSharp
    using AdlivMusic;
~~~

## プログラム

保存先を指定してmidiとvsq4のオブジェクトを生成します。  
vsq4は引数に各種IDが必要になります。お手持ちのPiaproStudioで適当なvsqxファイルを作成し、テキストエディタで開くことで確認できます。（VoiceName以外は適当でも問題はないようですが）
~~~CSharp
    AdlivMidi midi = new AdlivMidi(保存先);
    AdlivVsq4 vsq4 = new AdlivVsq4(保存先, vVoiceId, VoiceName, vVoiceId2, vsTrackId, auxContent);
~~~

音源の単位をトラックとします。  
作成したオブジェクトを利用し、midiとvsq4をそれぞれ必要な数だけトラックを生成します。  
midiの場合は音源を指定します。また、midiは10番目に作成したトラックはドラムキットになります。
~~~CSharp
    AdlivMidi.MidiTrack track1 = midi.CreateTrack(AdlivMusic.AdlivMidi.TONE.T_042_STRINGS_VIOLA);
    AdlivVsq4.vsTrack choir1 = vsq4.CreateTrack();
~~~

トラックに必要な数だけ音符を追加します。
~~~CSharp
    track1.AddNote(NOTEL.N1, NOTE.C3, 64);
    choir1.AddNote(NOTEL.N1, NOTE.C3, "ra");
~~~

ファイルを書き出します。オブジェクト生成時に指定したフォルダにファイルが出力されます。
~~~CSharp
    midi.Write();
    vsq4.Write();
~~~

sampleフォルダにサンプルがありますので、そちらも参照ください。

# 免責事項
作者が自分のために作成している趣味プログラムなので、予告なく変更が発生します。  
あらかじめご了承ください。

# リンク
- [はぅ君プロジェクト](http://haukun.projectroom.jp)
- [@Hau_kun](https://twitter.com/hau_kun)