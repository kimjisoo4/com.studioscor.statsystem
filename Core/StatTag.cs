using UnityEngine;

namespace StudioScor.StatSystem
{
    [CreateAssetMenu(fileName ="Stat_", menuName = "StudioScor/Stat System/new Stat")]
    public class StatTag : ScriptableObject
    {
        [Header(" [ Name ] ")]
        [SerializeField] private string statName;
        [Header(" [ Text ] ")]
        [SerializeField] private string description;

        public string Name => statName;
        public string Description => description;

        public void SetStatTag(string newName, string newDescription = null)
        {
            statName = newName;
            description = newDescription;
        }
    }
}
