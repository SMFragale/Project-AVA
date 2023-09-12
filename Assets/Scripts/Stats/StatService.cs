using System.Collections.Generic;
using AVA.Core;
using UnityEngine;
using UnityEngine.Events;

namespace AVA.Stats
{
    /// <summary>
    /// Service that manages character stats.
    /// This service contains all the relevant methods to manage stats. Such as adding and removing modifiers, getting base and calculated stats, and adding listeners to stat changes. 
    /// </summary>
    public class StatService
    {
        private List<ModifierContainer> _modifierContainers;

        private BaseStats _baseStats;

        private Dictionary<StatType, ObservableValue<float>> _calculatedStats;

        private UnityEvent _onStatChanged = new UnityEvent();

        /// <summary>
        /// Creates a Stats Service from a <see cref="BaseStatsSO"/>. 
        /// </summary>
        /// <param name="baseStats">Base stats from an asset file that will be copied into runtime stats </param>
        public StatService(BaseStatsSO baseStats)
        {
            _baseStats = new BaseStats(baseStats);
            _modifierContainers = new List<ModifierContainer>();
            _calculatedStats = new Dictionary<StatType, ObservableValue<float>>();
            foreach (var keyVal in _baseStats.Stats)
            {
                _calculatedStats.Add(keyVal.Key, new ObservableValue<float>(keyVal.Value));
            }
        }

        private void CalculateStat(StatType type)
        {
            float baseValue = GetBaseStat(type);
            float finalValue = baseValue;

            Debug.Log("StatService: " + type + " calculating with " + finalValue);
            Debug.Log("Modifiers: " + _modifierContainers.Count);

            foreach (ModifierContainer mc in _modifierContainers)
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
            _calculatedStats[type].Value = finalValue;

            Debug.Log("Type " + type.type + " limits: " + type.minValue + " " + type.maxValue);
            _onStatChanged?.Invoke();
        }


        /// <summary>
        /// Adds a listener to an event that is called when a stat changes.
        /// </summary>
        /// <param name="listener"></param>
        public void AddOnStatsChangedListener(UnityAction listener)
        {
            _onStatChanged.AddListener(listener);
        }

        /// <summary>
        /// Removes a listener from an event that is called when a stat changes.
        /// </summary>
        /// <param name="listener"></param>
        public void RemoveOnStatsChangedListener(UnityAction listener)
        {
            _onStatChanged.RemoveListener(listener);
        }

        /// <param name="type">Type of the stat</param>
        /// <returns> The calculated value of a stat based on the modifiers associated with that stat.</returns>
        public float GetCalculatedStat(StatType type)
        {
            return _calculatedStats[type].Value;
        }


        /// <param name="type"></param>
        /// <returns> The base value of a stat.</returns>
        public float GetBaseStat(StatType type)
        {
            return _baseStats.GetStat(type);
        }

        /// <returns> The list of <see cref="StatType"/>s that this StatSerice contains></returns>
        public List<StatType> GetStatTypes()
        {
            return _baseStats.GetStatTypes();
        }

        /// <returns>A dictionary of all the calculated stats.</returns>
        public Dictionary<StatType, float> GetAllCalculatedStats()
        {
            Dictionary<StatType, float> calculatedStats = new Dictionary<StatType, float>();
            foreach (var keyVal in _calculatedStats)
            {
                calculatedStats.Add(keyVal.Key, keyVal.Value.Value);
            }
            return calculatedStats;
        }

        /// <returns>A dictionary of all the base stats.</returns>
        public Dictionary<StatType, float> GetAllBaseStats()
        {
            return _baseStats.Stats;
        }

        /// <summary> Adds a set of modifiers to this StatService. </summary>
        public void AddModifiable(ModifierContainer mod)
        {
            _modifierContainers.Add(mod);
            foreach (StatType key in mod.Modifiers.Keys)
            {
                CalculateStat(key);
            }
        }

        /// <summary> Removes a set of modifiers from this StatService. </summary>
        /// <remarks> This method recalculates each stat in the mod container </remarks>
        public void RemoveModifiable(ModifierContainer mod)
        {
            _modifierContainers.Remove(mod);
            foreach (StatType key in mod.Modifiers.Keys)
            {
                CalculateStat(key);
            }
        }

        /// <summary> Removes all modifiers from this StatService. </summary>
        /// <remarks> This method recalculates the stats </remarks>
        public void RemoveAllModifiables()
        {
            _modifierContainers.Clear();
            foreach (var keyVal in _calculatedStats)
            {
                CalculateStat(keyVal.Key);
            }
        }

        /// <summary>
        /// Adds a listener to a stat change event.
        /// </summary>
        /// <param name="type"> type of the stat that will be listened to </param>
        /// <param name="listener"> action that will be called when the stat changes</param>
        /// <remarks> This method will log an error if the stat type is not found </remarks>
        public void AddStatListener(StatType type, UnityAction listener)
        {
            if (_calculatedStats.ContainsKey(type))
                _calculatedStats[type].AddOnChangedListener(listener);
            else
                Debug.LogError("StatService: stat " + type + " not found");
        }

        /// <summary>
        /// Removes a listener from a stat change event.
        /// </summary>
        /// <param name="type"> type of the stat that is being listened </param>
        /// <param name="listener"> action to be removed from the listener list when the stat changes</param>
        public void RemoveStatListener(StatType type, UnityAction listener)
        {
            if (_calculatedStats.ContainsKey(type))
                _calculatedStats[type].RemoveOnChangedListener(listener);
            else
                Debug.LogError("StatService: base stat " + type + " not found");
        }
    }
}
