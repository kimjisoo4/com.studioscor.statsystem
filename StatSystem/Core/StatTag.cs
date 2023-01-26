using UnityEngine;

namespace StudioScor.StatSystem
{
    [CreateAssetMenu(fileName ="Stat_", menuName = "StudioScor/Attribute System/new Stat")]
    public class StatTag : ScriptableObject
    {
        [Header("[Name]")]
        [SerializeField] private string _Name;
        [Header("[Text]")]
        [SerializeField] private string _Description;

        public string Name => _Name;
        public string Description => _Description;

        public void SetStatTag(string newName, string description = null)
        {
            _Name = newName;
            _Description = description;
        }
    }
}
