using System;
using System.Collections.Generic;
using AVA.Core;
using UnityEngine;
using UnityEngine.Events;

namespace AVA.Stats
{
    public class StatService
    {
        private List<ModifierContainer> modifierContainers;

        private BaseStats baseStats;

        private Dictionary<StatType, ObservableValue<float>> calculatedStats;

        public StatService(BaseStatsSO baseStats)
        {
            this.baseStats = new BaseStats(baseStats);
            this.modifierContainers = new List<ModifierContainer>();
            //Initialize calculated stats as a copy of baseStats.stats
            this.calculatedStats = new Dictionary<StatType, ObservableValue<float>>();
            foreach(var keyVal in this.baseStats.stats) {
                calculatedStats.Add(keyVal.Key, new ObservableValue<float>(keyVal.Value.Value));
            }
        }

        private void CalculateStat(StatType type)
        {
            float baseValue = GetBaseStat(type);
            float finalValue = baseValue;

            Debug.Log("StatService: " + type + " calculating with " + finalValue);
            Debug.Log("Modifiers: " + modifierContainers.Count);

            foreach (ModifierContainer mc in modifierContainers)
            {
                Modifier mod = mc.GetModifierByType(type);
                if (mod == null)
                    continue;
                if (mod.isPercentual)
                {
                    finalValue += baseValue * mod.modifier;
                }
                else
                {
                    finalValue += mod.modifier;
                }
            }
            //**
            calculatedStats[type].Value = finalValue;
            Debug.Log("StatService: " + type + " calculated with " + finalValue);
        }

        // Queries ----

        public float GetCalculatedStat(StatType type)
        {
            return calculatedStats[type].Value;
        }

        public float GetBaseStat(StatType type)
        {
            return baseStats.GetStat(type).Value;
        }

        public List<StatType> GetStatTypes()
        {
            return baseStats.GetStatTypes();
        }

        public void AddStatListener(StatType type, UnityAction listener)
        {
            if(calculatedStats.ContainsKey(type))
                calculatedStats[type].AddOnChangedListener(listener);
            else 
                Debug.LogError("StatService: stat " + type + " not found");
        }
        
        public void RemoveStatListener(StatType type, UnityAction listener)
        {
            if(calculatedStats.ContainsKey(type))
                calculatedStats[type].RemoveOnChangedListener(listener);
            else
                Debug.LogError("StatService: base stat " + type + " not found");
        }
        
        public void AddBaseStatListener(StatType type, UnityAction listener)
        {
            
            if(baseStats.stats.ContainsKey(type))
                baseStats.stats[type].AddOnChangedListener(listener);
            else 
                Debug.LogError("StatService: base stat" + type + " not found");
        }
        
        public void RemoveBaseStatListener(StatType type, UnityAction listener)
        {
            if(baseStats.stats.ContainsKey(type))
                baseStats.stats[type].RemoveOnChangedListener(listener);
            else
                Debug.LogError("StatService: base stat " + type + " not found");
        }

        // Commands ----

        public void AddModifiable(ModifierContainer mod)
        {
            modifierContainers.Add(mod);
            foreach(StatType key in mod.modifiers.Keys)
            {
                CalculateStat(key);
            }
        }

        public void RemoveModifiable(ModifierContainer mod)
        {
            modifierContainers.Remove(mod);
            foreach(StatType key in mod.modifiers.Keys)
            {
                CalculateStat(key);
            }
        }

        public void UpdateBaseStat(StatType type, float value)
        {
            baseStats.SetStat(type, value);
        }

        public Dictionary<StatType, ObservableValue<float>> GetAllCalculatedStats()
        {
            return calculatedStats;
        }

        public Dictionary<StatType, ObservableValue<float>> GetAllBaseStats() {
            return baseStats.stats;
        }
    }
}
