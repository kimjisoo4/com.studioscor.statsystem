using UnityEngine;

namespace StudioScor.StatSystem
{
    [System.Serializable]
    public struct FStatModifier
    {
        [SerializeField] private StatTag _statTag;
        [SerializeField] private StatModifier _statModifier;

        public StatTag StatTag => _statTag;
        public StatModifier StatModifier => _statModifier;
    }
}
