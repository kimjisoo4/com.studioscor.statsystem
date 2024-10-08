#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;

namespace StudioScor.StatSystem.VisualScripting
{
    [UnitTitle("Has Stat")]
    [UnitSubtitle("StatSystem Unit")]
    public class StatSystemHasStatUnit : StatSystemUnit
    {
        [DoNotSerialize]
        [PortLabel("StatTag")]
        public ValueInput StatTag;

        [DoNotSerialize]
        [PortLabel("hasStat")]
        [PortLabelHidden]
        public ValueOutput HasStat;

        protected override void Definition()
        {
            base.Definition();

            StatTag = ValueInput<StatTag>(nameof(StatTag), null);
            HasStat = ValueOutput<bool>(nameof(HasStat), GetHasStat);

            Requirement(Target, HasStat);
            Requirement(StatTag, HasStat);
        }

        private bool GetHasStat(Flow flow)
        {
            var statSystem = GetStatSystem(flow);
            var statTag = flow.GetValue<StatTag>(StatTag);

            return statSystem.HasStat(statTag);
        }
    }
}

#endif