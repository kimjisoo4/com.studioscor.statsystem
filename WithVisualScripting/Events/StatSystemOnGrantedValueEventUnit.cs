#if SCOR_ENABLE_VISUALSCRIPTING
using System;
using Unity.VisualScripting;

namespace StudioScor.StatSystem.VisualScripting
{
    [UnitTitle("On Granted Stat")]
	[UnitSubtitle("StatSystem Event")]
	[UnitCategory("Events\\StudioScor\\StatSystem")]
	public class StatSystemOnGrantedValueEventUnit : GameObjectEventUnit<Stat>
	{
		protected override string hookName => StatSystemWithVisualScripting.STATSYSTEM_ON_GRANTED_STAT;

		[DoNotSerialize]
		[PortLabel("Stat")]
		public ValueOutput Stat;

		public override Type MessageListenerType => typeof(StatSystemMessageListener);


		protected override void Definition()
		{
			base.Definition();

			Stat = ValueOutput<Stat>(nameof(Stat));
		}

		protected override void AssignArguments(Flow flow, Stat stat)
		{
			flow.SetValue(Stat, stat);
		}
	}
}

#endif