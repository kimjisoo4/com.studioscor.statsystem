using StudioScor.Utilities;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace StudioScor.StatSystem
{
    [Serializable]
#if SCOR_ENABLE_VISUALSCRIPTING
    [Unity.VisualScripting.IncludeInSettings(true)]
#endif
    public class Stat : ISerializationCallbackReceiver
	{
		#region Events
		public delegate void ChangedValue(Stat stat, float currentValue, float prevValue);
		#endregion

		[Header("[ Stat ]")]
		[SerializeField] private string _name;
		[SerializeField] private string _description;

		[Space(5f)]
		[SerializeField] private StatTag _statTag; 

		[Space(5f)]
		[SerializeField] private float _baseValue;
		[SerializeField] protected float _prevValue;
		[SerializeField] protected float _value;

		[Space(5f)]
		[SerializeField] protected readonly List<StatModifier> _statModifiers;

        public event ChangedValue OnChangedValue;

		public StatTag Tag => _statTag;
		public float BaseValue => _baseValue;
		public virtual float Value => _value;
		public virtual float PrevValue => _prevValue;

		public Stat(StatTag tag, float value)
        {
			_statTag = tag;

			_baseValue = value;

			_value = value;
			_prevValue = 0;

			_statModifiers = new List<StatModifier>();
		}

		public Stat(Stat stat)
        {
			_statTag = stat.Tag;

			_baseValue = stat.BaseValue;

			_value = BaseValue;
			_prevValue = 0;

			_statModifiers = new List<StatModifier>();
		}

		public void OnBeforeSerialize()
		{
		}
		public void OnAfterDeserialize()
		{
			_value = BaseValue;
			_prevValue = 0;
		}

		public void Dispose()
        {
			OnChangedValue = null;

			RemoveAllModifiers();
        }

		public void SetBaseValue(float value)
        {
			_baseValue = value;

			UpdateValue();
        }

		public virtual void AddModifier(StatModifier modifier)
		{
			if (_statModifiers.Count != 0)
			{
				bool insert = false;

				for(int i = 0; i < _statModifiers.Count; i++)
				{
					var statModifier = _statModifiers[i];

					if(statModifier.Order > modifier.Order)
					{
						insert = true;

                        _statModifiers.Insert(i, modifier);
					}
				}

				if(!insert)
				{
					_statModifiers.Add(modifier);

                }
			}
			else
			{
                _statModifiers.Add(modifier);
            }

			UpdateValue();
		}

		public virtual bool RemoveModifier(StatModifier modifier)
		{
			if (_statModifiers.Remove(modifier))
			{
				UpdateValue();

				return true;
			}

			return false;
		}

		public virtual void RemoveAllModifiers()
        {
			_statModifiers.Clear();

			UpdateValue();
        }

		public virtual bool RemoveAllModifiersFromSource(object source)
		{
			bool flag = false;

			for(int i = _statModifiers.LastIndex(); i > 0; i--)
			{
				var modifier = _statModifiers[i];

				if (modifier.Source == source)
				{
					flag = true;

					RemoveModifier(modifier);

                }
			}

			if(flag)
				UpdateValue();

			return flag;
		}

		protected virtual float UpdateValue()
        {
			_prevValue = _value;
			_value = CalculateValue();

            if (_prevValue != _value)
				Invoke_OnChangedValue();

			return Value;
		}

		

		protected virtual int CompareModifierOrder(StatModifier a, StatModifier b)
		{
			if (a.Order < b.Order)
            {
				return -1;
            }
			else if (a.Order > b.Order)
            {
				return 1;
            }

			return 0;
		}
		
		protected virtual float CalculateValue()
		{
			if (_statModifiers.Count == 0)
				return BaseValue;

			float finalValue = BaseValue;
			float sumPercentAdd = 0;

			StatModifier mod;

			for (int i = 0; i < _statModifiers.Count; i++)
			{
				mod = _statModifiers[i];

				if (mod.Type == EStatModifierType.Add)
				{
					finalValue += mod.Value;
				}
				else if (mod.Type == EStatModifierType.AddMultiply)
				{
					sumPercentAdd += mod.Value;

					if (i + 1 >= _statModifiers.Count || _statModifiers[i + 1].Type != EStatModifierType.AddMultiply)
					{
						finalValue += finalValue * sumPercentAdd;
						sumPercentAdd = 0;
					}
				}
				else if (mod.Type == EStatModifierType.Multiply)
				{
					finalValue *= mod.Value;
				}
			}

			return (float)Math.Round(finalValue, 4);
		}

		private void Invoke_OnChangedValue()
		{
			OnChangedValue?.Invoke(this, _value, _prevValue);
		}
	}
}
