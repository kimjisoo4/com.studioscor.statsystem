using UnityEngine;
using System.Collections.Generic;

namespace StudioScor.StatSystem
{
    [CreateAssetMenu(fileName = "StatSet_", menuName = "StudioScor/Stat System/new StetSet")]
	public class StatSet : ScriptableObject
    {
        [Header("[ Stat ]")]
        [SerializeField] private FStatSet[] _stats;
        public IReadOnlyCollection<FStatSet> Stats => _stats;

#if UNITY_EDITOR
        private void OnValidate()
        {
            for(int i = 0; i < Stats.Count; i++)
            {
                if (_stats[i].Tag == null)
                    return;

                var stat = _stats[i];

                _stats[i].HeaderName = $"{stat.Tag.Name} [ {stat.Value:N2} ]";
            }
        }
#endif
    }
}
