using UnityEditor;
using UnityEngine;

namespace AllMonoChanger.Scripts.Runtime
{
    public class MonoTestTarget : MonoBehaviour
    {
        [SerializeField] private int _testValue;
    }
    
    public class TestChanger
    {
        [ChangerTarget(typeof(MonoTestTarget))]
        public static void Change(MonoTestTarget testTarget)
        {
            var serializedObject = new SerializedObject(testTarget);
            serializedObject.Update();
            var testValueProp = serializedObject.FindProperty("_testValue");
            testValueProp.intValue *= 2;
            serializedObject.ApplyModifiedProperties();
        }
    }
}