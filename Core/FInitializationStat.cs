using StudioScor.Utilities;

namespace StudioScor.StatSystem
{
    [System.Serializable]
    public struct FInitializationStat
    {
#if UNITY_EDITOR
        [SReadOnly] public string HeaderName;
#endif
        public StatTag Tag;
        public float Value;
    }
}
