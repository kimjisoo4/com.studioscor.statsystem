using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace KimScor.StatSystem
{
    public class StatBlockUI : MonoBehaviour
    {
        [SerializeField] private Text Name;
        [SerializeField] private Text Count;

        public void SetText(KeyValuePair<StatTag, Stat> stat)
        {
            Name.text = stat.Key.StatName;
            Count.text = Mathf.RoundToInt(stat.Value.Value).ToString();
        }
    }
}
