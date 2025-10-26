using UnityEngine;

namespace StudioScor.StatSystem
{
    [CreateAssetMenu(fileName ="Stat_", menuName = "StudioScor/Stat System/new Stat", order = -1000000)]
    public class StatTag : ScriptableObject
    {
#if UNITY_EDITOR
        [SerializeField][TextArea] internal string m_description;
#endif
    }
}
