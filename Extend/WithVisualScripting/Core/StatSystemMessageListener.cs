#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;
using UnityEngine;

namespace StudioScor.StatSystem.VisualScripting
{
    [DisableAnnotation]
    [AddComponentMenu("")]
    [IncludeInSettings(false)]
    public class StatSystemMessageListener : MessageListener
    {
        private void Awake()
        {
			var statSystem = GetComponent<StatSystemComponent>();

            statSystem.OnGrantedStat += StatSystem_OnGrantedStat;
        }
        private void OnDestroy()
		{
            if (TryGetComponent(out StatSystemComponent statSystem))
            {
                statSystem.OnGrantedStat -= StatSystem_OnGrantedStat;
            }
		}
		private void StatSystem_OnGrantedStat(StatSystemComponent statSystem, Stat stat)
        {
			EventBus.Trigger(StatSystemWithVisualScripting.STATSYSTEM_ON_GRANTED_STAT, statSystem.gameObject, stat);
        }
    }
}

#endif