#if UNITY_EDITOR
    using UnityEditor;
#endif

using UnityEngine;

namespace AllMonoChanger.Runtime
{
    public class ChangerUtility
    {
        /// <summary>
        /// 変更したコンポーネント等を保存するために使います。
        /// EditorUtilityをそのまま使うとUnityEditorの参照が残るため、
        /// ビルドが通らなくなります。
        /// </summary>
        /// <param name="obj"></param>
        public static void SetDirty(Object obj)
        {
            #if UNITY_EDITOR
            EditorUtility.SetDirty(obj);
            #endif
        }
    }
}