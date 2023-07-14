using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace AllMonoChanger.Editor
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
                foreach(var component in gameObject.GetComponentsInChildren(componentType, true))
                {
                    yield return component;
                }
            }
        }
        
        public static IEnumerable<T> GetAllComponent<T>() where T: Component
        {
            var guids = AssetDatabase.FindAssets("t:Prefab");
            foreach (var guid in guids)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                var gameObject = AssetDatabase.LoadAssetAtPath<GameObject>(path);
                foreach(var component in gameObject.GetComponentsInChildren(typeof(T), true))
                {
                    yield return component as T;
                }
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
        
        public static IEnumerable<T> GetAllScriptableObject<T>() where T : ScriptableObject
        {
            var name = typeof(T).Name;
            var guids = AssetDatabase.FindAssets("t:" + name);
            foreach (var guid in guids)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                yield return AssetDatabase.LoadAssetAtPath(path, typeof(T)) as T;
            }
        }
    }
}