using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using StudioScor.Utilities;

namespace StudioScor.StatSystem
{
    [AddComponentMenu("StudioScor/StatSystem/StatSystem Component", order: 0)]
    public class StatSystemComponent : BaseMonoBehaviour, IStatSystem
    {
        [Header(" [ Stat System Component ] ")]
        [SerializeField] private int _defaultLevel = 1;
        [SerializeField] private StatSet _initializationStats;

        private readonly Dictionary<StatTag, Stat> _stats = new();
        private int _level;

        public IReadOnlyDictionary<StatTag, Stat> Stats => _stats;
        public int Level => _level;

        public event IStatSystem.ChangedLevelEventHandler OnChangedLevel;
        public event IStatSystem.StatEventHandler OnGrantedStat;
        public event IStatSystem.ChangedStatValueHandler OnChangedStatValue;

        private void OnValidate()
        {
#if UNITY_EDITOR
            _level = _defaultLevel;
#endif
        }
        private void Awake()
        {
            SetupStatSystem();
        }

        protected void SetupStatSystem()
        {
            SetLevel(_defaultLevel);

            UpdateStatLevel();
        }
        public void ResetStatSystem()
        {
            RemoveAllStatModifier();

            ResetLevel();

            OnReset();
        }
        protected virtual void OnSetup() { }
        protected virtual void OnReset() { }


        public void RemoveAllStatModifier()
        {
            Log(" Remove All Stat Modifiers ");

            foreach (var stat in _stats.Values)
            {
                stat.RemoveAllModifier();
            }
        }

        public void ResetLevel()
        {
            SetLevel(_defaultLevel);
        }

        public void SetLevel(int newLevel)
        {
            if (Level == newLevel)
                return;

            var prevLevel = Level;
            _level = newLevel;

            UpdateStatLevel();

            Invoke_OnChangedLevel(prevLevel);
        }

        protected void UpdateStatLevel()
        {
            foreach (FStatSet initializationStats in _initializationStats.Stats)
            {
                SetOrCreateValue(initializationStats.Tag, initializationStats.Value.Get(_level));
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
                Log(" Set Value ");

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
            Log(" Create Value ");

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

        protected void Invoke_OnChangedLevel(int prevLevel)
        {
            Log($"{nameof(OnChangedLevel)}  - [ CurrentLevel : {Level} | PrevLevle : {prevLevel}]");

            OnChangedLevel?.Invoke(this, Level, prevLevel);
        }
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
