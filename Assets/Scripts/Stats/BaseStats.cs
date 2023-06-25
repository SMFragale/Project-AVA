using System.Collections.Generic;
using UnityEngine;

namespace AVA.Stats
{
    public class BaseStats
    {
        public Dictionary<StatType, float> stats { get; private set; } = new Dictionary<StatType, float>();

        public BaseStats(float maxHealth, float attack, float speed, float defense, float attackSpeed)
        {
            stats = new Dictionary<StatType, float>
            {
                { StatType.MaxHealth, maxHealth },
                { StatType.Attack, attack },
                { StatType.Speed, speed },
                { StatType.Defense, defense },
                { StatType.AttackSpeed, attackSpeed }
            };
        }

        public BaseStats(BaseStatsSO so)
        {
            Debug.Log("Creating base stats from an SO");
            if(so == null)
                Debug.LogError("BaseStatsSO is null");
            var baseStats = so.GetBaseStats();
            Debug.Log("Creating base stats from " + baseStats.Length + " stats");
            foreach (var stat in baseStats)
            {
                if (!stats.ContainsKey(stat.type))
                    stats.Add(stat.type, stat.baseValue);
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
