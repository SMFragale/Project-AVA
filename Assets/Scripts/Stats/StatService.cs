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

        private UnityEvent onStatChanged = new UnityEvent();

        public StatService(BaseStatsSO baseStats)
        {
            this.baseStats = new BaseStats(baseStats);
            this.modifierContainers = new List<ModifierContainer>();
            //Initialize calculated stats as a copy of baseStats.stats
            this.calculatedStats = new Dictionary<StatType, ObservableValue<float>>();
            foreach(var keyVal in this.baseStats.stats) {
                calculatedStats.Add(keyVal.Key, new ObservableValue<float>(keyVal.Value));
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
                    finalValue += baseValue * mod.modifier;
                else
                    finalValue += mod.modifier;                
            }
            finalValue = Mathf.Clamp(finalValue, type.minValue, type.maxValue);
            calculatedStats[type].Value = finalValue;

            Debug.Log("Type " + type.type + " limits: " + type.minValue + " " + type.maxValue);
            onStatChanged?.Invoke();
        }

        // Events ----
        public void AddOnStatsChangedListener(UnityAction listener)
        {
            onStatChanged.AddListener(listener);
        }

        public void RemoveOnStatsChangedListener(UnityAction listener)
        {
            onStatChanged.RemoveListener(listener);
        }

        // Queries ----

        public float GetCalculatedStat(StatType type)
        {
            return calculatedStats[type].Value;
        }

        public float GetBaseStat(StatType type)
        {
            return baseStats.GetStat(type);
        }

        public List<StatType> GetStatTypes()
        {
            return baseStats.GetStatTypes();
        }
        
        public Dictionary<StatType, float> GetAllCalculatedStats()
        {
            Dictionary<StatType, float> calculatedStats = new Dictionary<StatType, float>();
            foreach (var keyVal in this.calculatedStats)
            {
                calculatedStats.Add(keyVal.Key, keyVal.Value.Value);
            }
            return calculatedStats;
        }

        public Dictionary<StatType, float> GetAllBaseStats()
        {
            return baseStats.stats;
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

        public void RemoveAllModifiables()
        {
            modifierContainers.Clear();
            foreach (var keyVal in calculatedStats)
            {
                CalculateStat(keyVal.Key);
            }
        }

        public void AddStatListener(StatType type, UnityAction listener)
        {
            if (calculatedStats.ContainsKey(type))
                calculatedStats[type].AddOnChangedListener(listener);
            else
                Debug.LogError("StatService: stat " + type + " not found");
        }

        public void RemoveStatListener(StatType type, UnityAction listener)
        {
            if (calculatedStats.ContainsKey(type))
                calculatedStats[type].RemoveOnChangedListener(listener);
            else
                Debug.LogError("StatService: base stat " + type + " not found");
        }
    }
}
