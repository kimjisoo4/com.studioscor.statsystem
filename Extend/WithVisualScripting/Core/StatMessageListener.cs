#if SCOR_ENABLE_VISUALSCRIPTING
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

namespace StudioScor.StatSystem.VisualScripting
{
    [DisableAnnotation]
    [AddComponentMenu("")]
    [IncludeInSettings(false)]
    public class StatMessageListener : MonoBehaviour
    {
        private StatSystemComponent _StatSystem;
        private List<StatTag> _StatTags;

        private void Awake()
        {
            _StatSystem = GetComponent<StatSystemComponent>();
            _StatTags = new();
        }
        private void OnDestroy()
        {
            if (!_StatSystem)
                return;

            foreach (var tag in _StatTags)
            {
                if(_StatSystem.TryGetValue(tag, out Stat stat))
                {
                    stat.OnChangedValue -= Stat_OnChangedValue;
                }
            }

            _StatSystem = null;
        }
        public void TryAddEventBus(StatTag tag)
        {
            if (_StatTags.Contains(tag))
                return;

            _StatTags.Add(tag);

            var stat = _StatSystem.GetOrCreateValue(tag);

            stat.OnChangedValue += Stat_OnChangedValue;
        }

        private void Stat_OnChangedValue(Stat stat, float currentValue, float prevValue)
        {
            var onChangedStatValue = new OnChangedStatValue(stat, currentValue, prevValue);

            EventBus.Trigger(new EventHook(StatSystemWithVisualScripting.STAT_ON_VALUE_CHANGED, stat), onChangedStatValue);
        }
    }
}

#endif