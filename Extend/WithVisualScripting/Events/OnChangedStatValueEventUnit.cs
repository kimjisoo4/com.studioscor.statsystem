#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;

namespace StudioScor.StatSystem.VisualScripting
{


    [UnitTitle("On Changed Stat Value")]
    [UnitSubtitle("StatSystem Event")]
    [UnitCategory("Events\\StudioScor\\StatSystem")]
    public class OnChangedStatValueEventUnit : StatEventUnit<FOnChangedStatValue>
    {
        protected override string HookName => StatSystemWithVisualScripting.STATSYSTEM_ON_CHANGED_STAT_VALUE;

        [DoNotSerialize]
        [PortLabel("StatTag")]
        public ValueInput StatTag { get; private set; }

        [DoNotSerialize]
        [PortLabel("Stat")]
        public ValueOutput Stat { get; private set; }

        [DoNotSerialize]
        [PortLabel("CurrentValue")]
        public ValueOutput CurrentValue { get; private set; }

        [DoNotSerialize]
        [PortLabel("PrevValue")]
        public ValueOutput PrevValue { get; private set; }


        protected override void Definition()
        {
            base.Definition();

            StatTag = ValueInput<StatTag>(nameof(StatTag), null);

            Stat = ValueOutput<Stat>(nameof(Stat));
            CurrentValue = ValueOutput<float>(nameof(CurrentValue));
            PrevValue = ValueOutput<float>(nameof(PrevValue));
        }
        protected override void AssignArguments(Flow flow, FOnChangedStatValue statValue)
        {
            flow.SetValue(Stat, statValue.Stat);
            flow.SetValue(CurrentValue, statValue.CurrentValue);
            flow.SetValue(PrevValue, statValue.PrevValue);
        }

        protected override bool ShouldTrigger(Flow flow, FOnChangedStatValue statValue)
        {
            if(!base.ShouldTrigger(flow, statValue))
                return false;

            return flow.GetValue<StatTag>(StatTag) == statValue.Stat.Tag;
        }
    }
}

#endif