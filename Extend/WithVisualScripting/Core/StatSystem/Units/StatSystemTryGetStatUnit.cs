#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;

namespace StudioScor.StatSystem.VisualScripting
{
    [UnitTitle("Try Get Stat")]
    [UnitSubtitle("StatSystem Unit")]
    public class StatSystemTryGetStatUnit : StatSystemUnit
    {
        [DoNotSerialize]
        [PortLabel("Enter")]
        [PortLabelHidden]
        public ControlInput Enter;

        [DoNotSerialize]
        [PortLabel("StatTag")]
        public ValueInput StatTag;

        [DoNotSerialize]
        [PortLabel("IsNotNull")]
        public ControlOutput IsNotNull;

        [DoNotSerialize]
        [PortLabel("IsNull")]
        public ControlOutput IsNull;

        [DoNotSerialize]
        [PortLabel("Stat")]
        public ValueOutput Stat;

        protected override void Definition()
        {
            base.Definition();

            Enter = ControlInput(nameof(Enter), OnFlow);
            StatTag = ValueInput<StatTag>(nameof(StatTag), null);

            IsNotNull = ControlOutput(nameof(IsNotNull));
            IsNull = ControlOutput(nameof(IsNull));
            Stat = ValueOutput<Stat>(nameof(Stat));
            
            Succession(Enter, IsNull);
            Succession(Enter, IsNotNull);

            Requirement(Target, Enter);
            Requirement(Target, Stat);
            Requirement(StatTag, Enter);
            Requirement(StatTag, Stat);
        }

        private ControlOutput OnFlow(Flow flow)
        {
            var statSystem = GetStatSystem(flow);
            var statTag = flow.GetValue<StatTag>(StatTag);

            if (statSystem.TryGetStat(statTag, out Stat stat))
            {
                flow.SetValue(Stat, stat);

                return IsNotNull;
            }
            else
            {
                return IsNull;
            }
        }
    }
}

#endif