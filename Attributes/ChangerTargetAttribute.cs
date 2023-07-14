using System;

namespace AllMonoChanger.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ChangerTargetAttribute : Attribute
    {
        public bool IsTemporary = true;
        public string OverrideMethodMessage = "";
        public bool DisableSetDirty = false;
        public ChangerTargetAttribute()
        {
        }
    }
}