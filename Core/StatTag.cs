using StudioScor.Utilities;
using UnityEngine;

namespace StudioScor.StatSystem
{
    [CreateAssetMenu(fileName ="Stat_", menuName = "StudioScor/Stat System/new Stat", order = -1000000)]
    public class StatTag : ScriptableObject
    {
        [Header(" [ Stat Tag ] ")]
        [SerializeField] private int _id = -1;

        public int ID => _id;

        private void OnValidate()
        {
#if UNITY_EDITOR
            if (_id == -1)
            {
                IDtoGUID();
            }
#endif
        }

        private void OnEnable()
        {
            IDtoGUID();
        }


        [ContextMenu(nameof(IDtoGUID), false, 1000000)]
        [System.Diagnostics.Conditional(SUtility.DEFINE_UNITY_EDITOR)]
        internal void IDtoGUID()
        {
#if UNITY_EDITOR
            if (this)
            {
                _id = this.GUIDToHash();

                UnityEditor.EditorUtility.SetDirty(this);
            }
#endif
        }
    }
}
