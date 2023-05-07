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
        [SerializeField] private int _DefaultLevel = 1;
        [SerializeField] private StatSet _initializationStats;

        private readonly Dictionary<StatTag, Stat> _Stats = new();
        private int _Level;

        public IReadOnlyDictionary<StatTag, Stat> Stats => _Stats;
        public int Level => _Level;

        public event ChangedLevelEventHandler OnChangedLevel;
        public event StatEventHandler OnGrantedStat;
        public event ChangedStatValueHandler OnChangedStatValue;

        private void OnValidate()
        {
#if UNITY_EDITOR
            _Level = _DefaultLevel;
#endif
        }
        private void Awake()
        {
            SetupStatSystem();
        }

        protected void SetupStatSystem()
        {
            SetLevel(_DefaultLevel);

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

            foreach (var stat in _Stats.Values)
            {
                stat.RemoveAllModifier();
            }
        }

        public void ResetLevel()
        {
            SetLevel(_DefaultLevel);
        }

        public void SetLevel(int newLevel)
        {
            if (Level == newLevel)
                return;

            var prevLevel = Level;
            _Level = newLevel;

            UpdateStatLevel();

            Callback_OnChangedLevel(prevLevel);
        }

        protected void UpdateStatLevel()
        {
            foreach (FStatSet initializationStats in _initializationStats.Stats)
            {
                SetOrCreateValue(initializationStats.Tag, initializationStats.Value.Get(_Level));
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

            _Stats.Add(tag, stat);

            Callback_OnGrantedStat(stat);

            stat.OnChangedValue += Stat_OnChangedValue;

            return stat;
        }

        private void Stat_OnChangedValue(Stat stat, float currentValue, float prevValue)
        {
            Callback_OnChangedStatValue(stat, currentValue, prevValue);
        }

        #region CallBack

        protected void Callback_OnChangedLevel(int prevLevel)
        {
            Log($"On Changed Level - [ CurrentLevel : {Level} | PrevLevle : {prevLevel}]");

            OnChangedLevel?.Invoke(this, Level, prevLevel);
        }
        protected void Callback_OnGrantedStat(Stat stat)
        {
            Log($" On Granted Stat - [ Stat : {stat.Name} ]");

            OnGrantedStat?.Invoke(this, stat);
        }
        protected void Callback_OnChangedStatValue(Stat stat,float currentValue, float prevValue)
        {
            Log($" On Changed Stat Value - [ Stat : {stat.Name} | Current : {currentValue:N2} | Prev : {prevValue:N2} ] ");

            OnChangedStatValue?.Invoke(this, stat, currentValue, prevValue);
        }

        #endregion
    }
}
