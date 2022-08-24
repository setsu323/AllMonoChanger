using System;
using UnityEditor;
using UnityEngine;

namespace AllMonoChanger.Scripts.Runtime
{
    public class ChangerTargetAttribute : Attribute
    {
        public Type TargetType { get; private set; }
        public ChangerTargetAttribute(Type targetType)
        {
            TargetType = targetType;
        }
    }
}