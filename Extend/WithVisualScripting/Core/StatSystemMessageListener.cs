#if SCOR_ENABLE_VISUALSCRIPTING
using UnityEngine;
using Unity.VisualScripting;

namespace StudioScor.StatSystem.VisualScripting
{
    [DisableAnnotation]
    [AddComponentMenu("")]
    [IncludeInSettings(false)]
    public class StatSystemMessageListener : MonoBehaviour
    {
        private void Awake()
        {
            var statSystem = transform.GetStatSystem();

            statSystem.OnChangedStatValue += StatSystem_OnChangedStatValue;
            statSystem.OnGrantedStat += StatSystem_OnGrantedStat;
        }
        private void OnDestroy()
        {
            if (transform.TryGetStatSystem(out IStatSystem statSystem))
            {
                statSystem.OnChangedStatValue -= StatSystem_OnChangedStatValue;
                statSystem.OnGrantedStat += StatSystem_OnGrantedStat;
            }
        }

        private void StatSystem_OnGrantedStat(IStatSystem statSystem, Stat stat)
        {
            EventBus.Trigger(new EventHook(StatSystemWithVisualScripting.STATSYSTEM_ON_GRANTED_STAT, statSystem), stat);
        }

        private void StatSystem_OnChangedStatValue(IStatSystem statSystem, Stat stat, float currentValue, float prevValue)
        {
            var onChangedStatValue = new FOnChangedStatValue(stat, currentValue, prevValue);

            EventBus.Trigger(new EventHook(StatSystemWithVisualScripting.STATSYSTEM_ON_CHANGED_STAT_VALUE, statSystem), onChangedStatValue);
        }
    }
}

#endif