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
                if (GUILayout.Button(changeTypesData.ChangerType.Name))
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
            var instance = changeTypesData.ChangerType.GetConstructor(Type.EmptyTypes).Invoke(null);
            var method = changeTypesData.ChangerType.GetMethod("Change");
            foreach (var component in allComponent)
            {
                var serializedObject = new SerializedObject(component);
                method.Invoke(instance, new[] { serializedObject });
            }
        }


        private static IEnumerable<ChangeTypesData> GetTypesData()
        {
            var typeCollection = TypeCache.GetTypesWithAttribute<ChangerTargetAttribute>();
            foreach (var type in typeCollection)
            {
                var typeInfo = type.GetTypeInfo();
                var attribute =
                    typeInfo.GetCustomAttributes().Where(a => a.GetType() == typeof(ChangerTargetAttribute)).First() as
                        ChangerTargetAttribute;

                if (attribute == null)
                {
                    Debug.LogWarning(type.Name + "にはChangerTarget属性が定義されていません");
                    continue;
                }
                yield return new ChangeTypesData() { ChangerType = type, TargetType = attribute.TargetType };
            }
        }

        private class ChangeTypesData
        {
            public Type ChangerType;
            public Type TargetType;
        }
    }
}
