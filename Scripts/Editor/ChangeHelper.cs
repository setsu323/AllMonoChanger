using System;
using System.Collections.Generic;
using PlasticGui;
using UnityEditor;
using UnityEngine;

namespace AllMonoChanger.Scripts.Editor
{
    /// <summary>
    /// 
    /// </summary>
    public class ChangeHelper
    {
        public static IEnumerable<Component> GetAllComponent(Type componentType)
        {
            var guids = AssetDatabase.FindAssets("t:Prefab");
            foreach (var guid in guids)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                var gameObject = AssetDatabase.LoadAssetAtPath<GameObject>(path);
                yield return gameObject.GetComponentInChildren(componentType, true);
            }
        }

        public static IEnumerable<ScriptableObject> GetAllScriptableObject(Type scriptableObjectType)
        {
            var name = scriptableObjectType.Name;
            var guids = AssetDatabase.FindAssets("t:" + name);
            foreach (var guid in guids)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                yield return AssetDatabase.LoadAssetAtPath(path, scriptableObjectType) as ScriptableObject;
            }
        }
    }
}