#if SCOR_ENABLE_VISUALSCRIPTING
using System;
using Unity.VisualScripting;

namespace StudioScor.StatSystem.VisualScripting
{
    [UnitTitle("Grant Stat Modifier")]
	[UnitSubtitle("StatSystem Unit")]
	[UnitCategory("StudioScor\\StatSystem")]
	public class GrantStatModifierUnit : Unit
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
		[PortLabel("Modifier")]
		[PortLabelHidden]
		public ValueInput Modifier;

		protected override void Definition()
		{
			Enter = ControlInput(nameof(Enter), GrantModifier);
			Exit = ControlOutput(nameof(Exit));

			StatSystem = ValueInput<StatSystemComponent>(nameof(StatSystem), null).NullMeansSelf();
			StatTag = ValueInput<StatTag>(nameof(StatTag), null);
			Modifier = ValueInput<StatModifier>(nameof(Modifier), null);
		}

		private ControlOutput GrantModifier(Flow flow)
		{
			var statSystem = flow.GetValue<StatSystemComponent>(StatSystem);
			var statTag = flow.GetValue<StatTag>(StatTag);
			var modifier = flow.GetValue<StatModifier>(Modifier);

			var stat = statSystem.GetOrCreateValue(statTag);

			stat.AddModifier(modifier);

			return Exit;
		}
	}
}

#endif