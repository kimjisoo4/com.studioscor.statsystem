using UnityEngine;

namespace StudioScor.StatSystem
{
    public delegate void ChangedLevelEventHandler(IStatSystem statSystem, int currentLevel, int prevLevel);
    public delegate void StatEventHandler(IStatSystem statSystem, Stat stat);
    public delegate void ChangedStatValueHandler(IStatSystem statSystem, Stat stat, float currentValue, float prevValue);

    public static class StatSystemUtility
    {
        public static IStatSystem GetStatSystem(this GameObject gameObject)
        {
            return gameObject.GetComponent<IStatSystem>();
        }
        public static IStatSystem GetStatSystem(this Component component)
        {
            return component.GetComponent<IStatSystem>();
        }
        public static bool TryGetStatSystem(this GameObject gameObject, out IStatSystem statSystem)
        {
            return gameObject.TryGetComponent(out statSystem);
        }
        public static bool TryGetStatSystem(this Component component, out IStatSystem statSystem)
        {
            statSystem = component as IStatSystem;

            if (statSystem is not null)
                return true;

            return component.TryGetComponent(out statSystem);
        }

        public static bool HasStat(this IStatSystem statSystem, StatTag statTag)
        {
            return statSystem.Stats.ContainsKey(statTag);
        }
        public static Stat GetStat(this IStatSystem statSystem, StatTag statTag)
        {
            return statSystem.Stats[statTag];
        }
        public static bool TryGetStat(this IStatSystem statSystem, StatTag statTag, out Stat stat)
        {
            return statSystem.Stats.TryGetValue(statTag, out stat);
        }

        public static void AddLevel(this IStatSystem statSystem, int addLevel = 1)
        {
            statSystem.SetLevel(statSystem.Level + addLevel);
        }
        public static void SubLevel(this IStatSystem statSystem, int subLevel = 1)
        {
            statSystem.SetLevel(statSystem.Level - subLevel);
        }
    }
}
