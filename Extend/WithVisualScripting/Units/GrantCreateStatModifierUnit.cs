#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;

namespace StudioScor.StatSystem.VisualScripting
{
    [UnitTitle("Grant Create Stat Modifier")]
    [UnitSubtitle("StatSystem Unit")]
    [UnitCategory("StudioScor\\StatSystem")]	
    public class GrantCreateStatModifierUnit : Unit
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
		public ValueInput Value;

		[DoNotSerialize]
		[PortLabel("Type")]
        [PortLabelHidden]
		public ValueInput Type;

		[DoNotSerialize]
		[PortLabel("Order")]
		public ValueInput Order;

		[DoNotSerialize]
        [AllowsNull]
		[PortLabel("Source")]
		public ValueInput Source;

		[DoNotSerialize]
        [PortLabel("Modifier")]
		[PortLabelHidden]
		public ValueOutput Modifier;

		private StatModifier _Modifier;

		protected override void Definition()
        {
			Enter = ControlInput(nameof(Enter), GrantModifier);
			Exit = ControlOutput(nameof(Exit));

			StatSystem = ValueInput<StatSystemComponent>(nameof(StatSystem), null).NullMeansSelf();
			StatTag = ValueInput<StatTag>(nameof(StatTag), null);
			Value = ValueInput<float>(nameof(Value), 0f);
			Type = ValueInput<EStatModifierType>(nameof(Type), EStatModifierType.Absolute);
			Order = ValueInput<int>(nameof(Order), 0);
			Source = ValueInput<object>(nameof(Source), null).AllowsNull();

			Modifier = ValueOutput<StatModifier>(nameof(Modifier), (flow) => { return _Modifier; });
		}

        private ControlOutput GrantModifier(Flow flow)
        {
			var statSystem = flow.GetValue<StatSystemComponent>(StatSystem);
			var statTag = flow.GetValue<StatTag>(StatTag);
			var value = flow.GetValue<float>(Value);
			var type = flow.GetValue<EStatModifierType>(Type);
			var order = flow.GetValue<int>(Order);
			var source = flow.GetValue<object>(Source);

			var stat = statSystem.GetOrCreateValue(statTag);

			_Modifier = new StatModifier(value, type, order, source);

			stat.AddModifier(_Modifier);

			return Exit;
		}
    }
}

#endif