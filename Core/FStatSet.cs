using UnityEngine;
using StudioScor.Utilities;

namespace StudioScor.StatSystem
{
    [System.Serializable]
    public struct FStatSet
    {
#if UNITY_EDITOR
        [SReadOnly] public string HeaderName;
#endif
        public StatTag Tag;
        public LevelFloatValue Value;
    }
}
