using System.Linq;
using AllMonoChanger.Scripts.Runtime;
using UnityEditor;

namespace AllMonoChanger.Scripts.Editor
{
    /// <summary>
    /// 全てのPrefabの中から、特定の手順で値を上書きするための機能。
    /// </summary>
    public class ChangeWindow : EditorWindow
    {
        
        [MenuItem("Window/ChangeWindow")]
        public static void ShowWindow()
        {
            //既存のウィンドウのインスタンスを表示。ない場合は作成します。
            EditorWindow.GetWindow(typeof(ChangeWindow));
        }
        
        private void OnGUI()
        {
            var type = typeof(MonoTestTarget);
            var allSerializedObject = ChangeHelper.GetAllComponent(type).Select(c => new SerializedObject(c));
            var isChanged = false;
            using (var changed = new EditorGUI.ChangeCheckScope())
            {
                foreach (var serializedObject in allSerializedObject)
                {
                    serializedObject.Update();
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("_testValue"));
                    serializedObject.ApplyModifiedPropertiesWithoutUndo();
                }

                if (changed.changed)
                {
                    AssetDatabase.SaveAssets();
                }
            }

            
        }
    }
}
