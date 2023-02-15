#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;

namespace StudioScor.StatSystem.VisualScripting
{
    [UnitTitle("Remove All Stat Modifier")]
	[UnitSubtitle("StatSystem Unit")]
	[UnitCategory("StudioScor\\StatSystem")]
	public class RemoveAllStatModifierUnit : Unit
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

		protected override void Definition()
		{
			Enter = ControlInput(nameof(Enter), GrantModifier);
			Exit = ControlOutput(nameof(Exit));

			StatSystem = ValueInput<StatSystemComponent>(nameof(StatSystem), null).NullMeansSelf();
			StatTag = ValueInput<StatTag>(nameof(StatTag), null);
		}

		private ControlOutput GrantModifier(Flow flow)
		{
			var statSystem = flow.GetValue<StatSystemComponent>(StatSystem);
			var statTag = flow.GetValue<StatTag>(StatTag);

			var stat = statSystem.GetOrCreateValue(statTag);

			stat.RemoveAllModifier();

			return Exit;
		}
	}
}

#endif