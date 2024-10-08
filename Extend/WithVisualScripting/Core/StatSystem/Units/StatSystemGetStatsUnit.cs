#if SCOR_ENABLE_VISUALSCRIPTING
using System.Collections.Generic;
using Unity.VisualScripting;

namespace StudioScor.StatSystem.VisualScripting
{
    [UnitTitle("Get Stats")]
    [UnitSubtitle("StatSystem Unit")]
    public class StatSystemGetStatsUnit : StatSystemUnit
    {
        [DoNotSerialize]
        [PortLabel("Stats")]
        [PortLabelHidden]
        public ValueOutput Stats;

        protected override void Definition()
        {
            base.Definition();

            Stats = ValueOutput<IReadOnlyDictionary<StatTag, Stat>>(nameof(Stats), GetValue);

            Requirement(Target, Stats);
        }

        private IReadOnlyDictionary<StatTag, Stat> GetValue(Flow flow)
        {
            var statSystem = GetStatSystem(flow);

            return statSystem.Stats;
        }
    }
}

#endif