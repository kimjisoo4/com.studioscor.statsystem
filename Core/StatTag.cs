using UnityEngine;

namespace StudioScor.StatSystem
{
    [CreateAssetMenu(fileName ="Stat_", menuName = "StudioScor/Stat System/new Stat")]
    public class StatTag : ScriptableObject
    {
        [Header(" [ Name ] ")]
        [SerializeField] private string _statName;
        [Header(" [ Text ] ")]
        [SerializeField] private string _description;

        public string Name => _statName;
        public string Description => _description;

        public void SetStatTag(string newName, string newDescription = null)
        {
            _statName = newName;
            _description = newDescription;
        }
    }
}
