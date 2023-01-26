using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System.Collections.Generic;

namespace StudioScor.StatSystem.Editor
{
    public class StatSystemDebuger : MonoBehaviour
    {
        [SerializeField] private StatSystemComponent _StatSystem;
        [SerializeField] private List<Stat> _Stats;

#if UNITY_EDITOR
        private void Reset()
        {
            _StatSystem = GetComponent<StatSystemComponent>();
        }
#endif
        private void OnEnable()
        {
            if(TryGetComponent(out _StatSystem))
            {
                _Stats = _StatSystem.Stats.Values.ToList();
                _StatSystem.OnGrantedStat += StatSystem_OnAddedStat;
            }
            else
            {
                enabled = false;
            }
        }
        private void OnDisable()
        {
            if (_StatSystem)
            {
                _Stats.Clear();
                _StatSystem.OnGrantedStat -= StatSystem_OnAddedStat;
            }
        }
        private void StatSystem_OnAddedStat(StatSystemComponent statSystem, Stat stat = null)
        {
            _Stats = _StatSystem.Stats.Values.ToList();
        }
    }
}
