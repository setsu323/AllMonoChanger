using UnityEditor;

namespace AllMonoChanger.Scripts.Runtime
{
    public abstract class BaseAllChanger
    {
        public abstract void Change(SerializedObject serializedObject);
    }
}