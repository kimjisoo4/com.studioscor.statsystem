#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;

namespace StudioScor.StatSystem.VisualScripting
{
    [UnitTitle("Set Or Create Stat")]
    [UnitSubtitle("StatSystem Unit")]
    public class StatSystemSetOrCreateStatUnit : StatSystemFlowUnit
    {
        [DoNotSerialize]
        [PortLabel("StatTag")]
        public ValueInput StatTag;

        [DoNotSerialize]
        [PortLabel("SetValue")]
        public ValueInput SetValue;

        [DoNotSerialize]
        [PortLabel("Stat")]
        public ValueOutput Stat;

        protected override void Definition()
        {
            base.Definition();

            StatTag = ValueInput<StatTag>(nameof(StatTag), null);
            SetValue = ValueInput<float>(nameof(SetValue), 0f);
            Stat = ValueOutput<Stat>(nameof(Stat));

            Requirement(StatTag, Enter);
            Requirement(SetValue, Enter);

            Assignment(Enter, Stat);
            Requirement(Target, Stat);
            Requirement(StatTag, Stat);
            Requirement(SetValue, Stat);
        }
        protected override ControlOutput OnFlow(Flow flow)
        {
            var statSystem = GetStatSystem(flow);
            var statTag = flow.GetValue<StatTag>(StatTag);
            var createValue = flow.GetValue<float>(SetValue);


            var stat = statSystem.SetOrCreateValue(statTag, createValue);
            flow.SetValue(Stat, stat);

            return Exit;
        }
    }
}

#endif