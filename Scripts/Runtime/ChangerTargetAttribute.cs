using System;

namespace AllMonoChanger.Scripts.Runtime
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ChangerTargetAttribute : Attribute
    {
        public ChangerTargetAttribute()
        {
        }
    }
}