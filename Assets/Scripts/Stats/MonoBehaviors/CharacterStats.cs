using System.Collections.Generic;
using UnityEngine.Events;

namespace AVA.Stats
{

    /// <summary>
    /// This component is responsible for exposing methods related to the stats of the game. However it only contains methods to read from stats and to subscribe to stat events. 
    /// For a component that can modify stats, see <see cref="CharacterModifiers"/>.
    /// To add this component, the gameobject must have a <see cref="StatsController"/> component as well.
    /// </summary>
    public class CharacterStats : StatOutput
    {

        /// <param name="type"> The stat type</param>
        /// <returns> The value of the stat associated with <paramref name="type"> </returns>
        public float GetStat(StatType type)
        {
            return statServiceInstance.GetCalculatedStat(type);
        }

        /// <param name="type"> The stat type</param>
        /// <returns> The base value of the stat associated with <paramref name="type"> </returns>
        public float GetBaseStat(StatType type)
        {
            return statServiceInstance.GetBaseStat(type);
        }

        /// <returns> A list of all the stat types associated with the relevant <see cref="StatsController"> </returns>
        public List<StatType> GetStatTypes()
        {
            return statServiceInstance.GetStatTypes();
        }

        /// <returns> A dictionary of all the calculated stat types and their respective values. </returns>
        public Dictionary<StatType, float> GetAllCalculatedStats()
        {
            return statServiceInstance.GetAllCalculatedStats();
        }

        /// <returns> A dictionary of all the base stats and their respective values. </returns>
        public Dictionary<StatType, float> GetAllBaseStats()
        {
            return statServiceInstance.GetAllBaseStats();
        }

        /// <summary>
        /// Adds a listener to an event that is called when the stat associated with <paramref name="type"> changes.
        /// </summary>
        /// <param name="type"> Type of the stat to add a listener to</param>
        /// <param name="listener"> Action to be executed when the specific stat changes</param>
        public void AddStatListener(StatType type, UnityAction listener)
        {
            statServiceInstance.AddStatListener(type, listener);
        }

        /// <summary>
        /// Removes a listener from an event that is called when a stat associated with <paramref name="type"> changes.
        /// </summary>
        /// <param name="type"> Type of the stat to remove a listener from</param>
        /// <param name="listener"> Action that will be removed from the event</param>
        public void RemoveStatListener(StatType type, UnityAction listener)
        {
            statServiceInstance.RemoveStatListener(type, listener);
        }

        /// <summary>
        /// Adds a listener to an event that is called when any stat changes.
        /// </summary>
        /// <param name="listener"> Action that will be executed when any stat changes</param>
        public void AddOnStatsChangedListener(UnityAction listener)
        {
            statServiceInstance.AddOnStatsChangedListener(listener);
        }

        /// <summary>
        /// Removes a listener from an event that is called when any stat changes.
        /// </summary>
        /// <param name="listener"> Action that will be removed from the event</param>
        public void RemoveOnStatsChangedListener(UnityAction listener)
        {
            statServiceInstance.RemoveOnStatsChangedListener(listener);
        }

    }
}