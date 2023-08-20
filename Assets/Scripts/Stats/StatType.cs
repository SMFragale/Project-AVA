using System.Collections.Generic;

namespace AVA.Stats
{

    /// <summary>
    /// Represents a type of stat. It is used to identify a stat, all stat types are defined in this class.
    /// </summary>
    public class StatType
    {
        public string type { get; private set; }
        public float maxValue { get; private set; }
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


        //TODO Potentially change this to be configuration file and set stat types based on a string
        public static readonly StatType MaxHealth = new StatType("MaxHealth", 10, 1000);
        public static readonly StatType Attack = new StatType("Attack", 0, 1000);
        public static readonly StatType Speed = new StatType("Speed", 1, 100);
        public static readonly StatType Defense = new StatType("Defense", 10, 1000);
        public static readonly StatType AttackSpeed = new StatType("AttackSpeed", 0.1f, 30);
        public static readonly StatType CritChance = new StatType("CritChance", 0, 100);
        public static readonly StatType CritDamage = new StatType("CritDamage", 0, 500);

        /// <summary>
        /// A dictionary that maps <see cref="Enum"/> to <see cref="StatType"/>. Used to convert from the Stats Enum to a stat type.
        /// </summary>
        public static Dictionary<Enum, StatType> enumToType = new Dictionary<Enum, StatType> {
            {Enum.MaxHealth, MaxHealth},
            {Enum.Attack, Attack},
            {Enum.Defense, Defense},
            {Enum.Speed, Speed},
            {Enum.AttackSpeed, AttackSpeed},
            {Enum.CritChance, CritChance},
            {Enum.CritDamage, CritDamage}
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