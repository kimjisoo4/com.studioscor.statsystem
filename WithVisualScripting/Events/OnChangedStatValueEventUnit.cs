#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;

namespace StudioScor.StatSystem.VisualScripting
{

    [UnitTitle("On Changed Stat Value")]
    [UnitSubtitle("StatSystem Event")]
    [UnitCategory("Events\\StudioScor\\StatSystem")]
    public class OnChangedStatValueEventUnit : StatEventUnit<OnChangedStatValue>
    {
        protected override string HookName => StatSystemWithVisualScripting.STAT_ON_VALUE_CHANGED;

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

            Stat = ValueOutput<Stat>(nameof(Stat));
            CurrentValue = ValueOutput<float>(nameof(CurrentValue));
            PrevValue = ValueOutput<float>(nameof(PrevValue));
        }
        protected override void AssignArguments(Flow flow, OnChangedStatValue args)
        {
            flow.SetValue(Stat, args.Stat);
            flow.SetValue(CurrentValue, args.CurrentValue);
            flow.SetValue(PrevValue, args.PrevValue);
        }
    }
}

#endif