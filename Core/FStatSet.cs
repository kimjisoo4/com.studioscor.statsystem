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
        [SerializeField] private StatTag _tag;
        [SerializeField] private float _value;

        public readonly StatTag Tag => _tag;
        public readonly float Value => _value;
    }
}
