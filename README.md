# AllMonoChanger
Unity内のすべてのMonobehaviourスクリプトを指定の手順で変更します

# 使用方法
## Component継承クラスに対して変更を適用する場合
ChangerTarget属性を引数が１つの静的関数につけます。  
この時、引数はComponentを継承している必要があります。  
  
属性を付けると、Window->ChangerWindow内にその関数名のボタンが追加されます。  
  
ボタンを押すと、すべてのPrefabの中から引数に指定した型のインスタンスを全て探し出し、  
逐次、静的関数の引数にして実行します。  

*注意  
直接、インスタンスを変更する場合は
EditorUtility.SetDirtyを変更後に使う必要があります。  
  
## ScriptableObject継承クラスに対して変更を適用する場合
Component継承クラスと使い方は同じです。  
全てのScriptableObjectに対して、変更が適用されます。
