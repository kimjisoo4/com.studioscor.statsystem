using StudioScor.Utilities;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace StudioScor.StatSystem
{
    [CreateAssetMenu(fileName = "New StatTag Container", menuName = "StudioScor/Stat System/new StatTag Container", order = -1000001)]
    public class StatTagContainer : ScriptableObject
    {
        [Header(" [ StatTag Container ] ")]
        [SerializeField] private StatTag[] _statTags;
        public IReadOnlyCollection<StatTag> StatTags => _statTags;

        private bool _wasInit = false;
        private readonly Dictionary<int, StatTag> _dictionary = new();

        [ContextMenu(nameof(FindObjectOfTypeAll), false, 1000000)]
        protected virtual void FindObjectOfTypeAll()
        {
#if UNITY_EDITOR
            var statTags = Resources.FindObjectsOfTypeAll<StatTag>();

            _statTags = statTags.OrderBy(item => item.name).ToArray();

            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }


        public StatTag GetStatTag(int id)
        {
            Init();

            return _dictionary[id];
        }

        private void Init()
        {
            if (_wasInit)
                return;

            _wasInit = true;

            for (int i = 0; i < _statTags.Length; i++)
            {
                var statTag = _statTags[i];

                if (!_dictionary.TryAdd(statTag.ID, _statTags[i]))
                {
                    SUtility.Debug.LogError($"[{name}] {statTag} have a duplicate ID !!", statTag);
                }
            }
        }

        private void OnEnable()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
#endif
        }
        private void OnDisable()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
#endif
        }

#if UNITY_EDITOR
        private void OnPlayModeStateChanged(UnityEditor.PlayModeStateChange change)
        {
            if (change == UnityEditor.PlayModeStateChange.EnteredPlayMode)
            {
                _wasInit = false;
            }
        }
#endif
    }
}
