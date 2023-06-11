using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AVA.Combat{
    public class CharacterStats : MonoBehaviour
    {

        [SerializeField] public Dictionary<StatType, Stat> stats;

        private void Awake()
        {
            stats = new Dictionary<StatType, Stat>();
        }

        public void AddStat(StatType type, float baseValue)
        {
            if (!stats.ContainsKey(type))
            {
                stats.Add(type, new Stat(type, baseValue));
            }
        }

        public void AddModifier(StatModifier sm)
        {
            if (stats.ContainsKey(sm.type))
            {
                stats[sm.type].AddModifier(sm);
            }
        }

        public void RemoveModifier(StatModifier sm)
        {
            if (stats.ContainsKey(sm.type))
            {
                stats[sm.type].RemoveModifier(sm);
            }
        }

        public float GetStatValue(StatType type)
        {
            if (stats.ContainsKey(type))
            {
                return stats[type].GetValue();
            }
            else
            {
                return 0;
            }
        }

        public Stat GetStat(StatType type)
        {
            if (stats.ContainsKey(type))
            {
                return stats[type];
            }
            else
            {
                return null;
            }
        }


        //
    }
    

}