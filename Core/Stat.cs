using System;
using UnityEngine;
using System.Collections.Generic;

using StudioScor.Utilities;

namespace StudioScor.StatSystem
{
	[System.Serializable]
	public class Stat : ISerializationCallbackReceiver
	{
		#region Events
		public delegate void ChangedValue(Stat stat, float currentValue, float prevValue);
		#endregion

		[Header("[ Stat ]")]
		[SerializeField] private string _Name;
		[SerializeField] private string _Description;

		[Space(5f)]
		[SerializeField] private StatTag _Tag; 

		[Space(5f)]
		[SerializeField] private float _BaseValue;
		[SerializeField] protected float _PrevValue;
		[SerializeField] protected float _Value;

		[Space(5f)]
		[SerializeField] protected readonly List<StatModifier> _StatModifiers;

        public event ChangedValue OnChangedValue;

		public StatTag Tag => _Tag;
		public string Name => _Name;
		public string Description => _Description;
		public float BaseValue => _BaseValue;
		public virtual float Value => _Value;
		public virtual float PrevValue => _PrevValue;

		public Stat(StatTag tag, float value)
        {
			_Name = tag.Name;
			_Description = tag.Description;

			_Tag = tag;

			_BaseValue = value;

			_Value = value;
			_PrevValue = 0;

			_StatModifiers = new List<StatModifier>();
		}

		public Stat(Stat stat)
        {
			_Name = stat.Tag.name;
			_Description = stat.Tag.Description;

			_Tag = stat.Tag;

			_BaseValue = stat.BaseValue;

			_Value = BaseValue;
			_PrevValue = 0;

			_StatModifiers = new List<StatModifier>();
		}

		public void OnBeforeSerialize()
		{
		}
		public void OnAfterDeserialize()
		{
			_Value = BaseValue;
			_PrevValue = 0;
		}

		public void Remove()
        {
			RemoveAllModifier();

			_StatModifiers.Clear();
        }

		public void SetBaseValue(float value)
        {
			_BaseValue = value;

			UpdateValue();
        }

		public virtual void AddModifier(StatModifier modifier)
		{
			_StatModifiers.Add(modifier);

			UpdateValue();
		}

		public virtual bool RemoveModifier(StatModifier modifier)
		{
			if (_StatModifiers.Remove(modifier))
			{
				UpdateValue();

				return true;
			}

			return false;
		}

		public virtual void RemoveAllModifier()
        {
			if(_StatModifiers.Count > 0)
            {
				_StatModifiers.Clear();

				UpdateValue();
			}			
        }

		public virtual bool RemoveAllModifiersFromSource(object source)
		{
			int numRemovals = _StatModifiers.RemoveAll(mod => mod.Source == source);

			if (numRemovals > 0)
			{
				UpdateValue();

				return true;
			}

			return false;
		}

		protected virtual float UpdateValue()
        {
			_PrevValue = _Value;
			_Value = CalculateValue();

            if (_PrevValue != _Value)
				Callback_OnChangedValue();

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
			if (_StatModifiers.Count == 0)
				return BaseValue;

			float finalValue = BaseValue;
			float sumPercentAdd = 0;

			_StatModifiers.Sort(CompareModifierOrder);

			StatModifier mod;

			for (int i = 0; i < _StatModifiers.Count; i++)
			{
				mod = _StatModifiers[i];

				if (mod.Type == EStatModifierType.Absolute)
				{
					finalValue += mod.Value;
				}
				else if (mod.Type == EStatModifierType.Percent)
				{
					sumPercentAdd += mod.Value;

					if (i + 1 >= _StatModifiers.Count || _StatModifiers[i + 1].Type != EStatModifierType.Percent)
					{
						finalValue *= sumPercentAdd;
						sumPercentAdd = 0;
					}
				}
				else if (mod.Type == EStatModifierType.PercentResult)
				{
					finalValue *= mod.Value;
				}
			}

			return (float)Math.Round(finalValue, 4);
		}
		private void Callback_OnChangedValue()
		{
			OnChangedValue?.Invoke(this, _Value, _PrevValue);
		}
	}
}
