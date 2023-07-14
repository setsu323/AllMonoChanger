using System;

namespace AllMonoChanger.Runtime
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ChangerTargetAttribute : Attribute
    {
        public bool IsTemporary = true;
        public string OverrideMethodMessage = "";
        public ChangerTargetAttribute()
        {
        }
    }
}