# GraphDrawingUnityProject

■説明  
●okn_data.csvを読み込んでグラフを描画する。 

●okn_data.csvの以下の列を利用する。  
・Application Time ( 経過時間 )  
・Eye Ray left dir  x（ 左眼の視線方向 x ）  
・Eye Ray left dir  y（ 左眼の視線方向 y ）  
・Eye Ray left dir  z（ 左眼の視線方向 z ）  

●1メモリはY軸は20度。X軸は1秒。  
　Y軸はマイナス40度～40度。  
　X軸は0～20秒。  

●MVRPパターンを用いてクラスを設計。  
●依存性の注入にExtenjectを利用。  
●ファイル読込は、UniTaskを利用。  
●ボタンクリック処理には、UniRxを利用。
