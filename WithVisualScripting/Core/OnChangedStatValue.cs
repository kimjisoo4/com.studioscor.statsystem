#if SCOR_ENABLE_VISUALSCRIPTING

namespace StudioScor.StatSystem.VisualScripting
{

    public class OnChangedStatValue
    {
		public OnChangedStatValue(Stat stat, float currentValue, float prevValue)
        {
			Stat = stat;
			CurrentValue = currentValue;
			PrevValue = prevValue;
        }

		public Stat Stat;
		public float CurrentValue;
		public float PrevValue;
    }
}

#endif