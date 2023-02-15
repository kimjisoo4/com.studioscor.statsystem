#if SCOR_ENABLE_VISUALSCRIPTING
using UnityEngine;
using Unity.VisualScripting;

namespace StudioScor.StatSystem.VisualScripting
{

    [UnitTitle("Create Stat Modifier")]
	[UnitSubtitle("StatSystem Unit")]
	[UnitCategory("StudioScor\\StatSystem")]
	public class CreateStatModifierUnit : Unit
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
        [PortLabel("Modifier")]
		[PortLabelHidden]
		public ValueOutput Modifier;

		private StatModifier _Modifier;

		protected override void Definition()
		{
			Enter = ControlInput(nameof(Enter), GrantModifier);
			Exit = ControlOutput(nameof(Exit));

			Value = ValueInput<float>(nameof(Value), 0f);
			Type = ValueInput<EStatModifierType>(nameof(Type), EStatModifierType.Absolute);
			Order = ValueInput<int>(nameof(Order), 0);

			Modifier = ValueOutput<StatModifier>(nameof(Modifier), CreateStatModifier);
		}


        private StatModifier CreateStatModifier(Flow flow)
        {
			if (_Modifier is null)
            {
				var value = flow.GetValue<float>(Value);
				var type = flow.GetValue<EStatModifierType>(Type);
				var order = flow.GetValue<int>(Order);

				return new StatModifier(value, type, order);
			}
            else
            {
				return _Modifier;
			}
        }

        private ControlOutput GrantModifier(Flow flow)
		{
			var value = flow.GetValue<float>(Value);
			var type = flow.GetValue<EStatModifierType>(Type);
			var order = flow.GetValue<int>(Order);

			_Modifier = new StatModifier(value, type, order);

			return Exit;
		}
	}
}

#endif