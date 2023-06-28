using System.Collections.Generic;
using UnityEngine;
using AVA.Core;

namespace AVA.Stats
{
    public class BaseStats
    {
        public Dictionary<StatType, ObservableValue<float>> stats { get; private set; } = new Dictionary<StatType, ObservableValue<float>>();

        public BaseStats(float maxHealth, float attack, float speed, float defense, float attackSpeed)
        {
            stats = new Dictionary<StatType, ObservableValue<float>>
            {
                { StatType.MaxHealth, new ObservableValue<float>(maxHealth) },
                { StatType.Attack, new ObservableValue<float>(attack) },
                { StatType.Speed, new ObservableValue<float>(speed) },
                { StatType.Defense, new ObservableValue<float>(defense) },
                { StatType.AttackSpeed, new ObservableValue<float>(attackSpeed) }
            };
        }

        public BaseStats(BaseStatsSO so)
        {
            Debug.Log("Creating base stats from an SO");
            if(so == null)
                Debug.LogError("BaseStatsSO is null");
            stats = new Dictionary<StatType, ObservableValue<float>>();
            var baseStats = so.GetBaseStats();
            Debug.Log("Creating base stats from " + baseStats.Length + " stats");
            foreach (var stat in baseStats)
            {
                if (!stats.ContainsKey(stat.type))
                    stats.Add(stat.type, new ObservableValue<float>(stat.baseValue));
            }
        }

        public ObservableValue<float> GetStat(StatType type)
        {
            ObservableValue<float> value;
            if (stats.TryGetValue(type, out value))
                return value;
            else
                throw new NoSuchStatException(type);
        }

        public void SetStat(StatType type, float newStat)
        {
            if (stats.ContainsKey(type))
            {
                stats[type].Value = newStat;
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
