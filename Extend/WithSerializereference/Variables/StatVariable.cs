using StudioScor.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StudioScor.StatSystem.Extend.Variable
{
    public class StatVariable : FloatVariable
    {
        [Header(" [ Stat Variable ] ")]
        [SerializeReference]
#if SCOR_ENABLE_SERIALIZEREFERENCE
        [SerializeReferenceDropdown]
#endif
        private IGameObjectVariable _target = new SelfGameObjectVariable();
        [SerializeField] private StatTag _statTag;

        private StatVariable _original;

        public override void Setup(GameObject owner)
        {
            base.Setup(owner);

            _target.Setup(Owner);
        }

        public override IFloatVariable Clone()
        {
            var clone = new StatVariable();

            clone._original = this;
            clone._target = _target.Clone();

            return clone;
        }

        public override float GetValue()
        {
            var actor = _target.GetValue();

            if(actor.TryGetStat(_original is null ? _statTag : _original._statTag, out Stat stat))
            {
                return stat.Value;
            }
            else
            {
                return 0;
            }
        }
    }
}
