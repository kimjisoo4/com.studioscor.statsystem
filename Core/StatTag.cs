using UnityEngine;

namespace StudioScor.StatSystem
{
    [CreateAssetMenu(fileName ="Stat_", menuName = "StudioScor/Stat System/new Stat")]
    public class StatTag : ScriptableObject
    {
        [Header(" [ Stat Tag ] ")]
        [SerializeField] private string _name;
        [SerializeField][TextArea] private string _description;

        public string Name => _name;
        public string Description => _description;

        public void SetStatTag(string newName, string newDescription = null)
        {
            _name = newName;
            _description = newDescription;
        }
    }
}
