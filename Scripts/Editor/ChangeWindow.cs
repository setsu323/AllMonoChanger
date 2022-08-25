using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AllMonoChanger.Scripts.Runtime;
using UnityEditor;
using UnityEngine;

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
            GetWindow(typeof(ChangeWindow));
        }

        
        private ChangeTypesData[] _cachedTypesData;
        private void OnEnable()
        {
            _cachedTypesData = GetTypesData().ToArray();
        }

        private void OnGUI()
        {
            var isChanged = false;
            foreach (var changeTypesData in _cachedTypesData)
            {
                if (GUILayout.Button(changeTypesData.MethodInfo.Name))
                {
                    isChanged = true;
                    Change(changeTypesData);
                }
            }

            if (isChanged)
            {
                AssetDatabase.SaveAssets();
            }
        }

        private void Change(ChangeTypesData changeTypesData)
        {
            var allComponent = ChangeHelper.GetAllComponent(changeTypesData.TargetType);
            foreach (var component in allComponent)
            {
                var serializedObject = new SerializedObject(component);
                changeTypesData.MethodInfo.Invoke(null, new[] { component });
            }
        }


        private static IEnumerable<ChangeTypesData> GetTypesData()
        {
            var methodCollection = TypeCache.GetMethodsWithAttribute<ChangerTargetAttribute>();
            foreach (var methodInfo in methodCollection)
            {
                if (!methodInfo.IsStatic)
                {
                    Debug.LogWarning("ChangerTarget関数はstaticの必要があります");
                    continue;
                }

                var parameters = methodInfo.GetParameters();
                if (parameters.Length != 1)
                {
                    Debug.LogWarning("AllMonoChangerはひとつの引数にしか対応していません");
                    continue;
                }

                var targetType = parameters[0].ParameterType;
                yield return new ChangeTypesData() { MethodInfo = methodInfo, TargetType = targetType };
            }
        }

        private class ChangeTypesData
        {
            public MethodInfo MethodInfo;
            public Type TargetType;
        }
    }
}
