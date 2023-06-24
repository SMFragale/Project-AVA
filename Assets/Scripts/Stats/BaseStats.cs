using System.Collections.Generic;

namespace AVA.Stats {
    public class BaseStats
    {
        public Dictionary<StatType, float> stats {get; private set;}

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

        public BaseStats(Dictionary<StatType, float> stats) {
            this.stats = stats;
        }

        public float GetStat(StatType type) {
            return stats[type];
        }

        public void SetStat(StatType type, float newStat) {
            if (stats.ContainsKey(type)) {
                stats[type] = newStat;
            }
        }
    }
}
