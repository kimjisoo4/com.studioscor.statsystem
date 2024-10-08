using System;
using UnityEngine;

namespace StudioScor.StatSystem
{
    [Serializable]
#if SCOR_ENABLE_VISUALSCRIPTING
	[Unity.VisualScripting.IncludeInSettings(true)]
#endif
	public class StatModifier
	{
        [Header(" [ Stat Modifier ] ")]
		[SerializeField] private float _value = 0f;
		[SerializeField] private EStatModifierType _type = EStatModifierType.Add;
		[SerializeField] private int _order = 0;
		[SerializeField] private object _source = null;

		public float Value => _value;
		public EStatModifierType Type => _type;
		public int Order => _order;
		public object Source => _source;

		public StatModifier()
		{

		}
		public StatModifier(StatModifier statModifier)
        {
            Setup(statModifier._value, statModifier._type, statModifier._order, statModifier._source);
		}

		public StatModifier(float value, EStatModifierType type, int order = 0, object source = null)
		{
			Setup(value, type, order, source);

        }

		public void Setup(float value, EStatModifierType type, int order = 0, object source = null)
        {
            _value = value;
            _type = type;
            _order = order;
            _source = source;
        }
		public void SetSource(object newSource)
		{
			_source = newSource;
		}
	}
}
