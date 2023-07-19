using System.Collections.Generic;

namespace AVA.Stats
{
    public class StatType
    {
        public string type {get; private set;}
        public float maxValue {get; private set;}
        public float minValue { get; private set; }

        public StatType(string type, float minValue, float maxValue)
        {
            this.type = type;
            this.minValue = minValue;
            this.maxValue = maxValue;
        }

        public override int GetHashCode()
        {
            return type.GetHashCode();
        }
        
        public static readonly StatType MaxHealth = new StatType("MaxHealth", 10, 1000);
        public static readonly StatType Attack = new StatType("Attack", 0, 1000);
        public static readonly StatType Speed = new StatType("Speed", 10, 1000);
        public static readonly StatType Defense = new StatType("Defense", 10, 1000);
        public static readonly StatType AttackSpeed = new StatType("AttackSpeed", 10, 500);
        public static readonly StatType CritChance = new StatType("CritChance", 0, 100);
        public static readonly StatType CritDamage = new StatType("CritDamage", 0, 500);

        public static Dictionary<Enum, StatType> enumToType = new Dictionary<Enum, StatType> {
            {Enum.MaxHealth, StatType.MaxHealth},
            {Enum.Attack, StatType.Attack},
            {Enum.Defense, StatType.Defense},
            {Enum.Speed, StatType.Speed},
            {Enum.AttackSpeed, StatType.AttackSpeed},
            {Enum.CritChance, StatType.CritChance},
            {Enum.CritDamage, StatType.CritDamage}
        };

        public enum Enum
        {
            MaxHealth,
            Attack,
            Defense,
            Speed,
            AttackSpeed,
            CritChance,
            CritDamage
        }
    }


}