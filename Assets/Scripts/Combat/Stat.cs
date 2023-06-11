using System.Collections.Generic;
using UnityEngine;

namespace AVA.Combat{

    [System.Serializable]
    public class Stat
    {
        [SerializeField] private StatType type;
        [SerializeField] private float baseValue;

        [SerializeField] private List<StatModifier> modifiers = new List<StatModifier>();

        //Constructors
        public Stat(StatType type, float baseValue)
        {
            this.type = type;
            this.baseValue = baseValue;
        }
        public Stat()
        {

        }

        //Getters and Setters

        public StatType GetStatType()
        {
            return type;
        }

        public float GetValue()
        {
            float finalValue = baseValue;
            modifiers.ForEach((x) => {
                if(x.isPercentual)
                    finalValue += x.modifier * finalValue;
                else
                    finalValue += x.modifier;
                });
            return finalValue;
        }

        public void AddModifier(StatModifier sm)
        {
            if (sm.type == type && sm.modifier != 0)
                modifiers.Add(sm);
        }

        public void RemoveModifier(StatModifier sm)
        {
            if (sm.type == type && sm.modifier != 0)
                modifiers.Remove(sm);
        }

    }
    public enum StatType
    {
        Health,
        Damage,
        Speed,
        Defense,
        AttackSpeed
    }

    public struct StatModifier
    {
        public StatType type;
        public float modifier;

        public bool isPercentual;

        public StatModifier(StatType type, float modifier, bool isPercentual)
        {
            this.type = type;
            this.modifier = modifier;
            this.isPercentual = isPercentual;
        }
    }
}