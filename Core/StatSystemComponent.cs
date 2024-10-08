using StudioScor.Utilities;
using System.Collections.Generic;
using UnityEngine;

namespace StudioScor.StatSystem
{
    [AddComponentMenu("StudioScor/StatSystem/StatSystem Component", order: 0)]
    public class StatSystemComponent : BaseMonoBehaviour, IStatSystem
    {
        [Header(" [ Stat System Component ] ")]
        [SerializeField] private StatSet _initializationStats;

        private readonly Dictionary<StatTag, Stat> _stats = new();
        public IReadOnlyDictionary<StatTag, Stat> Stats => _stats;

        public event IStatSystem.StatEventHandler OnGrantedStat;
        public event IStatSystem.ChangedStatValueHandler OnChangedStatValue;

        private void Awake()
        {
            SetupStatSystem();
        }

        protected void SetupStatSystem()
        {
            if(_initializationStats)
            {
                foreach (FStatSet initializationStats in _initializationStats.Stats)
                {
                    SetOrCreateValue(initializationStats.Tag, initializationStats.Value);
                }
            }
        }
        public void ResetStatSystem()
        {
            RemoveAllStatModifier();

            OnReset();
        }
        protected virtual void OnSetup() { }
        protected virtual void OnReset() { }


        public void RemoveAllStatModifier()
        {
            Log(" Remove All Stat Modifiers ");

            foreach (var stat in _stats.Values)
            {
                stat.RemoveAllModifiers();
            }
        }

        public Stat GetOrCreateValue(StatTag tag, float value = 0f)
        {
            if (Stats.TryGetValue(tag, out Stat stat))
            {
                return stat;
            }
            else
            {
                return CreateStat(tag, value);
            }
        }
        public Stat SetOrCreateValue(StatTag tag, float value = 0f)
        {
            if(Stats.TryGetValue(tag, out Stat stat))
            {
                Log($"Set Value - {tag.Name} :: {value:N2}");

                stat.SetBaseValue(value);

                return stat;
            }
            else
            {
                return CreateStat(tag, value);
            }
        }
        public Stat CreateStat(StatTag tag, float value = 0f)
        {
            Log($"{nameof(CreateStat)} - {tag.Name} :: {value:N2}");

            var stat = new Stat(tag, value);

            _stats.Add(tag, stat);

            Invoke_OnGrantedStat(stat);

            stat.OnChangedValue += Stat_OnChangedValue;

            return stat;
        }

        private void Stat_OnChangedValue(Stat stat, float currentValue, float prevValue)
        {
            Invoke_OnChangedStatValue(stat, currentValue, prevValue);
        }

        #region Invoke

        protected void Invoke_OnGrantedStat(Stat stat)
        {
            Log($"{nameof(OnGrantedStat)}  - [ Stat : {stat.Name} ]");

            OnGrantedStat?.Invoke(this, stat);
        }
        protected void Invoke_OnChangedStatValue(Stat stat,float currentValue, float prevValue)
        {
            Log($"{nameof(OnChangedStatValue)} - [ Stat : {stat.Name} | Current : {currentValue:N2} | Prev : {prevValue:N2} ] ");

            OnChangedStatValue?.Invoke(this, stat, currentValue, prevValue);
        }

        #endregion
    }
}
