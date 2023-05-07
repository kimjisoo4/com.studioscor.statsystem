#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;

namespace StudioScor.StatSystem.VisualScripting
{
    [UnitTitle("On Changed Level")]
    [UnitSubtitle("StatSystem Event")]
    [UnitCategory("Events\\StudioScor\\StatSystem")]
    public class OnChangedLevelEventUnit : StatEventUnit<FOnChangedLevel>
    {
        protected override string HookName => StatSystemWithVisualScripting.STATSYSTEM_ON_CHANGED_LEVEL;

        [DoNotSerialize]
        [PortLabel("Current Level")]
        public ValueOutput CurrentValue { get; private set; }

        [DoNotSerialize]
        [PortLabel("Prev Level")]
        public ValueOutput PrevValue { get; private set; }


        protected override void Definition()
        {
            base.Definition();

            CurrentValue = ValueOutput<int>(nameof(CurrentValue));
            PrevValue = ValueOutput<int>(nameof(PrevValue));
        }
        protected override void AssignArguments(Flow flow, FOnChangedLevel statValue)
        {
            flow.SetValue(CurrentValue, statValue.CurrentLevel);
            flow.SetValue(PrevValue, statValue.PrevLevel);
        }
    }
}

#endif