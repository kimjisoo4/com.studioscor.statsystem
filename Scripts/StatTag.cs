using UnityEngine;



namespace KimScor.StatSystem
{
    [CreateAssetMenu(fileName ="new Stat", menuName = "GAS/Stat/Stat_")]
    public class StatTag : ScriptableObject
    {
        [Header("[Name]")]
        [SerializeField] private string _StatName;
        [Header("[Text]")]
        [SerializeField] private string _Description;

        public string StatName => _StatName;
        public string Description => _Description;

        public void SetStatTag(string newName, string description = null)
        {
            _StatName = newName;
            _Description = description;
        }
    }
}
