#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;

namespace StudioScor.StatSystem.VisualScripting
{
    [UnitTitle("Get Or Create Stat")]
    [UnitSubtitle("StatSystem Unit")]
    public class StatSystemGetOrCreateStatUnit : StatSystemFlowUnit
    {
        [DoNotSerialize]
        [PortLabel("StatTag")]
        public ValueInput StatTag;

        [DoNotSerialize]
        [PortLabel("CreateValue")]
        public ValueInput CreateValue;


        [DoNotSerialize]
        [PortLabel("Stat")]
        public ValueOutput Stat;

        protected override void Definition()
        {
            base.Definition();

            StatTag = ValueInput<StatTag>(nameof(StatTag), null);
            CreateValue = ValueInput<float>(nameof(CreateValue), 0f);
            Stat = ValueOutput<Stat>(nameof(Stat));

            Requirement(StatTag, Enter);
            Requirement(CreateValue, Enter);

            Assignment(Enter, Stat);
            Requirement(Target, Stat);
            Requirement(StatTag, Stat);
            Requirement(CreateValue, Stat);
        }
        protected override ControlOutput OnFlow(Flow flow)
        {
            var statSystem = GetStatSystem(flow);
            var statTag = flow.GetValue<StatTag>(StatTag);
            var createValue = flow.GetValue<float>(CreateValue);


            var stat = statSystem.GetOrCreateValue(statTag, createValue);
            flow.SetValue(Stat, stat);

            return Exit;
        }
    }
}

#endif