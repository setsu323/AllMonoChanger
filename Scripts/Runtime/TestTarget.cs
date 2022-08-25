using UnityEngine;

namespace AllMonoChanger.Scripts.Runtime
{
    [CreateAssetMenu(fileName = "testTarget", menuName = "TestTarget", order = 0)]
    public class TestTarget : ScriptableObject
    {
        [SerializeField] private int _testValue;
    }
}