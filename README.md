# SSEditor_MVVM

会話文特化型エディタ製作中。C#,WPF


WPF,MVVMの勉強として鋭意製作中です。

----
Ctrl, Alt + 0~9に人物選択のホットキー割り当て可能。

Alt + Enterで文章の入力完了。

Ctrl + Enterで地の文入力

----
ライセンス
CopyRight(c)2016 nemu626@github

yamada0626@gmail.com

このソフトフェアはMITライセンスにより配布されます。

https://opensource.org/licenses/mit-license.php

以下のライブラリを使用しています。

+ MVVM light Toolkit (https://mvvmlight.codeplex.com/license) Mit
+ Mahapps.Metro (https://github.com/MahApps/MahApps.Metro/blob/develop/LICENSE) MS-PL 

+ A WPF Font Picker (http://www.codeproject.com/Articles/368070/A-WPF-Font-Picker-with-Color) CPOL
  ( http://www.codeproject.com/info/cpol10.aspx)

Install : MVVM light toolkit, Mahapps.MetroをNugetからインストールしてください。

	Install-Package MvvmLight
	
	Install-Package MahApps.Metro





toDoList

+ Animation機能
+ コメントを書く
+ 人物情報修正
+ タブエディタ化
+ テーマ機能の実装
+ Text -> LinesへのParse機能(←正規表現)
+ プラグイン
+ Undo,Redo機能（←Memento pattern勉強)
+ プレーンエディタに登場人物非表示
+ ~~地の文を吹き出しではなく違う見た目に~~ 完了
+ ~~文章修正時に吹き出しの台詞自動更新。(LineクラスのInotify継承)~~ 完了
+ ~~Line Delete~~完了
+ ~~Window Size リサイズ時に吹き出しのサイズ調整~~完了
+ ~~吹き出しのサイズを正常にする~~完了
+ ~~右クリックメニュー実装~~完了
