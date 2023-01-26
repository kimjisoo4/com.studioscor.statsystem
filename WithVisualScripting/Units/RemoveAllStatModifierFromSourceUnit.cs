#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;

namespace StudioScor.StatSystem.VisualScripting
{
    [UnitTitle("Remove All Stat Modifier From Source")]
	[UnitSubtitle("StatSystem Unit")]
	[UnitCategory("StudioScor\\StatSystem")]
	public class RemoveAllStatModifierFromSourceUnit : Unit
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
		[PortLabelHidden]
		[PortLabel("StatTag")]
		public ValueInput StatTag;

		[DoNotSerialize]
        [AllowsNull]
		[PortLabel("Source")]
		public ValueInput Source;

		protected override void Definition()
		{
			Enter = ControlInput(nameof(Enter), GrantModifier);
			Exit = ControlOutput(nameof(Exit));

			StatSystem = ValueInput<StatSystemComponent>(nameof(StatSystem), null).NullMeansSelf();
			StatTag = ValueInput<StatTag>(nameof(StatTag), null);
			Source = ValueInput<object>(nameof(Source), null).AllowsNull();
		}

		private ControlOutput GrantModifier(Flow flow)
		{
			var statSystem = flow.GetValue<StatSystemComponent>(StatSystem);
			var statTag = flow.GetValue<StatTag>(StatTag);
			var source = flow.GetValue<object>(Source);

			var stat = statSystem.GetOrCreateValue(statTag);

			stat.RemoveAllModifiersFromSource(source);

			return Exit;
		}
	}
}

#endif