using UnityEngine;
using System.Collections.Generic;

namespace StudioScor.StatSystem
{
    [CreateAssetMenu(fileName = "StatSet_", menuName = "StudioScor/Stat System/new StetSet")]
	public class StatSet : ScriptableObject
    {
        [Header("[ Stat ]")]
        [SerializeField] private FStatSet[] _Stats;
        public IReadOnlyCollection<FStatSet> Stats => _Stats;

#if UNITY_EDITOR
        private void OnValidate()
        {
            for(int i = 0; i < Stats.Count; i++)
            {
                if (_Stats[i].Tag == null)
                    return;

                var stat = _Stats[i];

                _Stats[i].HeaderName = $"{stat.Tag.Name} [ {stat.Value.Min:N2} ~ {stat.Value.Max:N2} ]";
                _Stats[i].Value.UpdateValue();
            }
        }
#endif
    }
}
