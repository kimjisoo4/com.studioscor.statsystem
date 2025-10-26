using UnityEngine;

namespace StudioScor.StatSystem
{
    [System.Serializable]
    public struct FStatSet
    {
#if UNITY_EDITOR
        public string HeaderName;
#endif
        [SerializeField] private StatTag _tag;
        [SerializeField] private float _value;

        public readonly StatTag Tag => _tag;
        public readonly float Value => _value;
    }
}
