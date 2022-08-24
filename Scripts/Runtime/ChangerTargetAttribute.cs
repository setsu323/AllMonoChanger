using System;

namespace AllMonoChanger.Scripts.Runtime
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ChangerTargetAttribute : Attribute
    {
        public Type TargetType { get; private set; }
        public ChangerTargetAttribute(Type targetType)
        {
            TargetType = targetType;
        }
    }
}