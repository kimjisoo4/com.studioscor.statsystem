using UnityEngine;
using System.Collections.Generic;

namespace StudioScor.StatSystem
{
    public interface IStatSystem
    {
        public Transform transform { get; }
        public GameObject gameObject { get; }
        public IReadOnlyDictionary<StatTag, Stat> Stats { get; }

        public int Level { get; }
        public void SetLevel(int newLevel);
        public void ResetLevel();

        public void RemoveAllStatModifier();
        public Stat GetOrCreateValue(StatTag tag, float value = 0f);
        public Stat SetOrCreateValue(StatTag tag, float value = 0f);

        public event ChangedLevelEventHandler OnChangedLevel;
        public event StatEventHandler OnGrantedStat;
        public event ChangedStatValueHandler OnChangedStatValue;
    }
}
