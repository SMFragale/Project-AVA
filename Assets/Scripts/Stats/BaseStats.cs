using System.Collections.Generic;
using UnityEngine;

namespace AVA.Stats
{
    public class BaseStats
    {
        public Dictionary<StatType, float> stats { get; private set; } = new Dictionary<StatType, float>();

        public BaseStats(BaseStatsSO so)
        {
            if (so == null)
                Debug.LogError("BaseStatsSO is null");
            stats = new Dictionary<StatType, float>();
            var baseStats = so.GetBaseStats();
            foreach (var stat in baseStats)
            {
                StatType type = StatType.enumToType[stat.type];
                if (!stats.ContainsKey(type))
                    stats.Add(type, stat.baseValue);
            }
        }

        public float GetStat(StatType type)
        {
            float value;
            if (stats.TryGetValue(type, out value))
                return value;
            else
                throw new NoSuchStatException(type);
        }

        public void SetStat(StatType type, float newStat)
        {
            if (stats.ContainsKey(type))
            {
                stats[type] = newStat;
            }
        }

        public List<StatType> GetStatTypes()
        {
            return new List<StatType>(stats.Keys);
        }
    }

    public class NoSuchStatException : System.Exception
    {
        public NoSuchStatException(StatType type) : base("The stat " + type + " does not exist in this object") { }
    }
}
