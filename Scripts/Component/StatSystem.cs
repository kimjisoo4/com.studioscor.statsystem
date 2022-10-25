using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace KimScor.StatSystem
{
    public class StatSystem : MonoBehaviour
    {
        #region Events
        public delegate void StatEventHandler(StatSystem statSystem, Stat stat);
        #endregion
        
        [SerializeField] private InitializationStats[] _initializationStats;
        private bool _WasSetting = false;

        private Dictionary<StatTag, Stat> _Stats = new Dictionary<StatTag, Stat>();

        public IReadOnlyDictionary<StatTag, Stat> Stats
        {
            get
            {
                if (!_WasSetting)
                    SetupStatSystem();

                return _Stats;
            }
        }

        public event StatEventHandler OnAddedStat;


        private void Awake()
        {
            if (!_WasSetting)
                SetupStatSystem();
        }

        protected virtual void SetupStatSystem()
        {
            _WasSetting = true;

            _Stats = new Dictionary<StatTag, Stat>();

            if (_initializationStats.Length > 0)
            {
                foreach (InitializationStats initializationStats in _initializationStats)
                {
                    OnInitializationStat(initializationStats);
                }
            }
        }

        public void OnInitializationStat(InitializationStats stats)
        {
            foreach (FInitializationStat initializationStat in stats.Stats)
            {
                InitializationStat(initializationStat);
            }
        }
        public void OnInitializationStat(FInitializationStat[] stats)
        {
            foreach (FInitializationStat initializationStat in stats)
            {
                InitializationStat(initializationStat);
            }
        }
        public Stat OnInitializationStat(FInitializationStat stat)
        {
            return InitializationStat(stat);
        }

        private Stat InitializationStat(FInitializationStat initializationStat)
        {
            if (!Stats.TryGetValue(initializationStat.StatTag, out Stat stat))
            {   
                stat = new Stat(initializationStat.StatTag, initializationStat.Value);

                _Stats.Add(initializationStat.StatTag, stat);

                OnAddStat(stat);
            }
            else
            {
                stat.SetBaseValue(initializationStat.Value);
            }

            return stat;
        }
        

        public Stat GetValue(StatTag Tag)
        {
            if (!Tag)
            {
                return null;
            }

            if (Stats.TryGetValue(Tag, out Stat value))
                return value;

            return null;
        }
        public Stat SetOrCreateValue(StatTag tag, float value = 0f)
        {
            if (!tag)
            {
                return null;
            }

            if (Stats.TryGetValue(tag, out Stat stat))
            {
                stat.SetBaseValue(value);

                return stat;
            }
            else
            {
                stat = new Stat(tag, value);

                _Stats.Add(tag, stat);

                OnAddStat(stat);

                return stat;
            }
        }
        public Stat GetOrCreateValue(StatTag Tag, float Value = 0f)
        {
            if (!Tag)
            {
                return null;
            }

            if (Stats.TryGetValue(Tag, out Stat stat))
                return stat;
            else
            {
                stat = new Stat(Tag, Value);

                _Stats.Add(Tag, stat);

                OnAddStat(stat);

                return stat;
            }
        }

        public Stat GetOrCreateValue(string tag, float value = 0f)
        {
            if(tag.Equals(null))
            {
                return null;
            }

            foreach (Stat stat in Stats.Values)
            {
                if (stat.StatName == tag) 
                {
                    return stat;
                }
            }

            var newStatTag = Instantiate(new StatTag());
            
            newStatTag.SetStatTag(tag);

            var newStat = new Stat(newStatTag, value);

            return newStat;
        }

        #region CallBack
        protected void OnAddStat(Stat stat)
        {
            OnAddedStat?.Invoke(this, stat);
        }

        #endregion
    }
}
