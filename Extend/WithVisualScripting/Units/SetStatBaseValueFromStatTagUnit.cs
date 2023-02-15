#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;

namespace StudioScor.StatSystem.VisualScripting
{
    [UnitTitle("Set Stat Base Value From StatTag")]
    [UnitShortTitle("SetStatBaseValue")]
	[UnitSubtitle("StatSystem Unit")]
	[UnitCategory("StudioScor\\StatSystem")]
	public class SetStatBaseValueFromStatTagUnit : Unit
	{
		[DoNotSerialize]
		[PortLabel("Enter")]
		[PortLabelHidden]
		public ControlInput Enter;

		[DoNotSerialize]
		[PortLabel("Enter")]
		[PortLabelHidden]
		public ControlOutput Exit;

		[DoNotSerialize]
		[PortLabel("StatSystem")]
		[PortLabelHidden]
		[NullMeansSelf]
		public ValueInput StatSystem;

		[DoNotSerialize]
		[PortLabel("StatTag")]
		[PortLabelHidden]
		public ValueInput StatTag;

		[DoNotSerialize]
		[PortLabel("Value")]
		[PortLabelHidden]
		public ValueInput Value;

		protected override void Definition()
		{
			Enter = ControlInput(nameof(Enter), GrantModifier);
			Exit = ControlOutput(nameof(Exit));

			StatSystem = ValueInput<StatSystemComponent>(nameof(StatSystem), null).NullMeansSelf();
			StatTag = ValueInput<StatTag>(nameof(StatTag), null);
			Value = ValueInput<float>(nameof(Value), default);
		}

		private ControlOutput GrantModifier(Flow flow)
		{
			var statSystem = flow.GetValue<StatSystemComponent>(StatSystem);
			var statTag = flow.GetValue<StatTag>(StatTag);
			var value = flow.GetValue<float>(Value);

			var stat = statSystem.GetOrCreateValue(statTag);

			stat.SetBaseValue(value);

			return Exit;
		}
	}
}

#endif