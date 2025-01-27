using UnityEngine;

namespace StudioScor.StatSystem
{
    [CreateAssetMenu(fileName ="Stat_", menuName = "StudioScor/Stat System/new Stat")]
    public class StatTag : ScriptableObject
    {
        [Header(" [ Stat Tag ] ")]
        [SerializeField] private string _id;
        public string ID => _id;

        [ContextMenu(nameof(NameToID), false, 1000000)]
        private void NameToID()
        {
            _id = name;
        }
    }
}
