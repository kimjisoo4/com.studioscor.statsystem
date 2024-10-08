#if SCOR_ENABLE_VISUALSCRIPTING
using System;
using Unity.VisualScripting;

namespace StudioScor.StatSystem.VisualScripting
{
    [UnitTitle("On Granted Stat")]
	[UnitSubtitle("StatSystem Event")]
	public class StatSystemOnGrantedValueEventUnit : StatSystemEventUnit<Stat>
	{
		protected override string HookName => StatSystemWithVisualScripting.STATSYSTEM_ON_GRANTED_STAT;

		[DoNotSerialize]
		[PortLabel("Stat")]
		public ValueOutput Stat;

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