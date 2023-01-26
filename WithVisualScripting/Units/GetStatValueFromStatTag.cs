#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;

namespace StudioScor.StatSystem.VisualScripting
{
    [UnitTitle("Get Stat Value From StatTag")]
	[UnitSubtitle("StatSystem Unit")]
	[UnitCategory("StudioScor\\StatSystem")]
	public class GetStatValueFromStatTag : Unit
    {
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
		public ValueOutput Value;

		protected override void Definition()
		{
			StatSystem = ValueInput<StatSystemComponent>(nameof(StatSystem), null).NullMeansSelf();
			StatTag = ValueInput<StatTag>(nameof(StatTag), null);

			Value = ValueOutput<float>(nameof(Value), GetValue);

			Requirement(StatSystem, Value);
			Requirement(StatTag, Value);
		}

        private float GetValue(Flow flow)
        {
			var statSystem = flow.GetValue<StatSystemComponent>(StatSystem);
			var statTag = flow.GetValue<StatTag>(StatTag);

			var stat = statSystem.GetOrCreateValue(statTag);

			return stat.Value;
        }
    }
}

#endif