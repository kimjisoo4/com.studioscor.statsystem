#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;
using UnityEngine;

namespace StudioScor.StatSystem.VisualScripting
{
    [UnitCategory("StudioScor\\StatSystem\\StatSystem")]
    public abstract class StatSystemUnit : Unit
    {
        [DoNotSerialize]
        [NullMeansSelf]
        [PortLabel("Target")]
        [PortLabelHidden]
        public ValueInput Target;

        protected override void Definition()
        {
            Target = ValueInput<GameObject>(nameof(Target), null).NullMeansSelf();
        }

        protected IStatSystem GetStatSystem(Flow flow)
        {
            return flow.GetValue<IStatSystem>(Target);
        }
    }
}

#endif