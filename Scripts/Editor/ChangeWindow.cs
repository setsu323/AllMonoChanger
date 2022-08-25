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
                var overrideName = changeTypesData.Attribute.OverrideMethodMessage;
                var methodName = changeTypesData.MethodInfo.Name;
                var buttonName = overrideName == "" ? methodName : overrideName;
                
                if (GUILayout.Button(buttonName))
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

        private static void Change(ChangeTypesData changeTypesData)
        {
            IEnumerable<object> allComponent;
            if (changeTypesData.TargetType.IsSubclassOf(typeof(Component)))
            {
                allComponent = ChangeHelper.GetAllComponent(changeTypesData.TargetType);
            }
            else if(changeTypesData.TargetType.IsSubclassOf(typeof(ScriptableObject)))
            {
                allComponent = ChangeHelper.GetAllScriptableObject(changeTypesData.TargetType);
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
            foreach (var component in allComponent)
            {
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
                    Debug.LogWarning("ChangerTarget関数はstaticの必要があります" + methodInfo.Name);
                    continue;
                }

                var parameters = methodInfo.GetParameters();
                if (parameters.Length != 1)
                {
                    Debug.LogWarning("AllMonoChangerはひとつの引数にしか対応していません" + methodInfo.Name);
                    continue;
                }

                var targetType = parameters[0].ParameterType;
                var changerTargetAttribute = methodInfo.GetCustomAttribute<ChangerTargetAttribute>();

                yield return new ChangeTypesData()
                    { MethodInfo = methodInfo, TargetType = targetType, Attribute = changerTargetAttribute };
            }
        }

        private class ChangeTypesData
        {
            public ChangerTargetAttribute Attribute;
            public MethodInfo MethodInfo;
            public Type TargetType;
        }
    }
}
