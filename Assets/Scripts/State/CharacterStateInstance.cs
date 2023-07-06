using System.Collections.Generic;
using AVA.Stats;
using UnityEngine;

namespace AVA.State
{
    public class CharacterStateInstance
    {
        public Dictionary<StatType, float> stats { get; private set; }
        public float health { get; private set; }
        public float shield { get; private set; }

        public CharacterStateInstance(Dictionary<StatType, float> stats, float health, float shield)
        {
            this.stats = stats;
            this.health = health;
            this.shield = shield;
        }

        public override string ToString()
        {
            string result = "";
            foreach (KeyValuePair<StatType, float> entry in stats)
            {
                result += entry.Key.type + ": " + entry.Value + "\n";
            }
            result += "Health: " + health + "\n";
            result += "Shield: " + shield + "\n";
            return result;
        }
    }
}

