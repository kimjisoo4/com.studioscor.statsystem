using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace StudioScor.StatSystem
{
    [AddComponentMenu("StudioScor/StatSystem/StatSystem Component", order: 0)]
    public class StatSystemComponent : MonoBehaviour
    {
        #region Events
        public delegate void StatEventHandler(StatSystemComponent statSystem, Stat stat);
        public delegate void ChangedStatValueHandler(StatSystemComponent statSystem, Stat stat, float prevValue, float currentValue);
        #endregion
        
        [SerializeField] private InitializationStats _initializationStats;

        private bool _WasSetup = false;

        private Dictionary<StatTag, Stat> _Stats = new();

        public IReadOnlyDictionary<StatTag, Stat> Stats
        {
            get
            {
                if (!_WasSetup)
                    SetupStatSystem();

                return _Stats;
            }
        }

        public event StatEventHandler OnGrantedStat;
        public event ChangedStatValueHandler OnChangedStatValue;

        private void Awake()
        {
            if (!_WasSetup)
                SetupStatSystem();
        }

        protected void SetupStatSystem()
        {
            if (_WasSetup)
                return;

            _WasSetup = true;

            _Stats = new();

            SetupInitializationStat();
        }
        public void ResetStatSystem()
        {
            RemoveAllStatModifier();

            SetupInitializationStat();

            OnReset();
        }

        protected virtual void OnSetup() { }
        protected virtual void OnReset() { }

        #region InitializationStat
        public void SetupInitializationStat()
        {
            if (!_initializationStats)
                return;

            foreach (FInitializationStat initializationStats in _initializationStats.Stats)
            {
                SetOrCreateValue(initializationStats.Tag, initializationStats.Value);
            }
        }

        #endregion
        #region Remove Stat
        public void RemoveAllStatModifier()
        {
            foreach (var stat in _Stats.Values)
            {
                stat.RemoveAllModifier();
            }
        }
        #endregion
        #region Get Value

        public bool TryGetValue(StatTag tag, out Stat stat)
        {
            if (!tag)
            {
                stat = null;


                return false;
            }

            return Stats.TryGetValue(tag, out stat);
        }
        public Stat GetOrCreateValue(StatTag tag, float value = 0f)
        {
            if (TryGetValue(tag, out Stat stat))
            {
                return stat;
            }
            else
            {
                stat = new Stat(tag, value);

                _Stats.Add(tag, stat);

                Callback_OnGrantedStat(stat);

                return stat;
            }
        }
        #endregion
        #region Set Value

        public bool TrySetValue(StatTag tag, float newValue)
        {
            if (TryGetValue(tag, out Stat stat))
            {
                stat.SetBaseValue(newValue);

                return true;
            }

            return false;
        }
        public Stat SetOrCreateValue(StatTag tag, float value = 0f)
        {
            if(TryGetValue(tag, out Stat stat))
            {
                stat.SetBaseValue(value);

                return stat;
            }
            else
            {
                stat = new Stat(tag, value);

                _Stats.Add(tag, stat);

                Callback_OnGrantedStat(stat);

                stat.OnChangedValue += Stat_OnChangedValue;

                return stat;
            }
        }

        private void Stat_OnChangedValue(Stat stat, float currentValue, float prevValue)
        {
            Callback_OnChangedStatValue(stat, currentValue, prevValue);
        }
        #endregion

        #region CallBack
        protected void Callback_OnGrantedStat(Stat stat)
        {
            OnGrantedStat?.Invoke(this, stat);
        }
        protected void Callback_OnChangedStatValue(Stat stat,float currentValue, float prevValue)
        {
            OnChangedStatValue?.Invoke(this, stat, currentValue, prevValue);
        }

        #endregion
    }
}
