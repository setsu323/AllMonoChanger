using UnityEditor;
using UnityEngine;

namespace AllMonoChanger.Scripts.Runtime
{
    public class MonoTestTarget : MonoBehaviour
    {
        [SerializeField] private int _testValue;
    }

    [ChangerTarget(typeof(MonoTestTarget))]
    public class TestChanger : BaseAllChanger
    {
        public override void Change(SerializedObject serializedObject)
        {
            serializedObject.Update();
            var testValueProp = serializedObject.FindProperty("_testValue");
            testValueProp.intValue *= 2;
            serializedObject.ApplyModifiedProperties();
        }
    }
}