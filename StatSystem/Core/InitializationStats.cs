using UnityEngine;
using System.Collections.Generic;

namespace StudioScor.StatSystem
{

    [CreateAssetMenu(fileName = "InitializationStats_", menuName = "StudioScor/Attribute System/new Initialization Stats")]
	public class InitializationStats : ScriptableObject
    {
        [Header("[Stat]")]
        [SerializeField] private FInitializationStat[] _Stats;
        public IReadOnlyCollection<FInitializationStat> Stats => _Stats;

#if UNITY_EDITOR
        private void OnValidate()
        {
            for(int i = 0; i < Stats.Count; i++)
            {
                if (_Stats[i].Tag == null)
                    return;

                _Stats[i].HeaderName = _Stats[i].Tag.Name + " [" + _Stats[i].Value + "]";
            }
        }
#endif
    }
}
