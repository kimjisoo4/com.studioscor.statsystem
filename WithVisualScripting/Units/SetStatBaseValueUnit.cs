#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;

namespace StudioScor.StatSystem.VisualScripting
{
    [UnitTitle("Set Stat Base Value")]
    [UnitShortTitle("SetStatBaseValue")]
	[UnitSubtitle("StatSystem Unit")]
	[UnitCategory("StudioScor\\StatSystem")]
	public class SetStatBaseValueUnit : Unit
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
		[PortLabelHidden]
		[PortLabel("Stat")]
		public ValueInput Stat;

		[DoNotSerialize]
		[PortLabel("Value")]
        [PortLabelHidden]
		public ValueInput Value;

		protected override void Definition()
		{
			Enter = ControlInput(nameof(Enter), GrantModifier);
			Exit = ControlOutput(nameof(Exit));

			Stat = ValueInput<Stat>(nameof(Stat), null);
			Value = ValueInput<float>(nameof(Value), default);
		}

		private ControlOutput GrantModifier(Flow flow)
		{
			var stat = flow.GetValue<Stat>(Stat);
			var value = flow.GetValue<float>(Value);

			stat.SetBaseValue(value);

			return Exit;
		}
	}
}

#endif