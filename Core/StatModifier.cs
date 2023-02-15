using System;

namespace StudioScor.StatSystem
{
    [System.Serializable]
	public class StatModifier
	{
		private float _Value;
		private EStatModifierType _Type;
		private int _Order;
		private object _Source;

		public float Value => _Value;
		public EStatModifierType Type => _Type;
		public int Order => (int)Type + _Order;
		public object Source => _Source;

		public StatModifier(StatModifier statModifier)
        {
			_Value = statModifier.Value;
			_Type = statModifier.Type;
			_Order = statModifier.Order;
			_Source = statModifier.Source;
		}

		public StatModifier(float value, EStatModifierType type, int order = 0, object source = null)
		{
			_Value = value;
			_Type = type;
			_Order = order;
			_Source = source;
		}
	}
}
