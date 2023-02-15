#if SCOR_ENABLE_VISUALSCRIPTING
using System;
using Unity.VisualScripting;

namespace StudioScor.StatSystem.VisualScripting
{
    [UnitTitle("Get Stat Value")]
	[UnitSubtitle("StatSystem Unit")]
	[UnitCategory("StudioScor\\StatSystem")]
	public class GetStatValue : Unit
    {
		[DoNotSerialize]
		[PortLabel("Stat")]
		public ValueInput Stat;

		[DoNotSerialize]
		[PortLabel("Value")]
		[PortLabelHidden]
		public ValueOutput Value;

		protected override void Definition()
		{
			Stat = ValueInput<Stat>(nameof(Stat));
			Value = ValueOutput<float>(nameof(Value), (flow) => { return flow.GetValue<Stat>(Stat).Value; });

			Requirement(Stat, Value);
        }
    }
}

#endif