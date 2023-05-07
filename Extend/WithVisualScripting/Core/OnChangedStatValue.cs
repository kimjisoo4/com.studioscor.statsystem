#if SCOR_ENABLE_VISUALSCRIPTING

namespace StudioScor.StatSystem.VisualScripting
{
	public struct FOnChangedLevel
    {
		public int CurrentLevel;
		public int PrevLevel;

        public FOnChangedLevel(int currentLevel, int prevLevel)
        {
            CurrentLevel = currentLevel;
            PrevLevel = prevLevel;
        }
    }
    public struct FOnChangedStatValue
    {
		public FOnChangedStatValue(Stat stat, float currentValue, float prevValue)
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