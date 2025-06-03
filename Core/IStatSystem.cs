using UnityEngine;
using System.Collections.Generic;

namespace StudioScor.StatSystem
{
    public interface IStatSystem
    {
        public delegate void StatEventHandler(IStatSystem statSystem, Stat stat);
        public delegate void ChangedStatValueHandler(IStatSystem statSystem, Stat stat, float currentValue, float prevValue);

        public Transform transform { get; }
        public GameObject gameObject { get; }
        public IReadOnlyDictionary<StatTag, Stat> Stats { get; }

        public void Initialization();
        public void Dispose();
        public void RemoveAllStatModifier();
        public Stat GetOrCreateValue(StatTag tag, float value = 0f);
        public Stat SetOrCreateValue(StatTag tag, float value = 0f);

        public event StatEventHandler OnGrantedStat;
        public event ChangedStatValueHandler OnChangedStatValue;
    }
}
