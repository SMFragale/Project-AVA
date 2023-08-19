using System.Collections.Generic;
using UnityEngine;

namespace AVA.Stats
{
    /// <summary>
    /// BaseStats represents the runtime stats model.
    /// It holds a dictionary of stats and their values. These BaseStats are a runtime representation of the <see cref="BaseStatsSO"/>.
    /// Despite being a runtime representation, these stats are immutable, the only way to modify stats in-game is to use the <see cref="CharacterModifiers"/> class.
    /// </summary>

    public class BaseStats
    {
        private Dictionary<StatType, float> stats = new Dictionary<StatType, float>();

        /// <summary>
        /// Returns a copy of the stats dictionary.
        /// </summary>
        /// <typeparam name="StatType">The type of a stat</typeparam>
        /// <typeparam name="float">The value of a stat</typeparam>
        public Dictionary<StatType, float> Stats { get { return new Dictionary<StatType, float>(stats); } }

        /// <summary>
        /// Creates a new BaseStats object from a <see cref="BaseStatsSO"/>. It will log an error if the BaseStatsSO is null.
        /// </summary>
        /// <param name="so">A base stats Scriptable Object that contains the </param>
        public BaseStats(BaseStatsSO so)
        {
            if (so == null)
                Debug.LogError("BaseStatsSO is null");
            stats = new Dictionary<StatType, float>();
            var baseStats = so.GetBaseStats();
            foreach (var stat in baseStats)
            {
                StatType type = StatType.enumToType[stat.type];
                if (!stats.ContainsKey(type))
                    stats.Add(type, stat.baseValue);
            }
        }

        /// <param name="type">The type of the desired stat</param>
        /// <returns>The value of a stat.</returns>
        /// <exception cref="NoSuchStatException">If the stat does not exist, it will throw a <see cref="NoSuchStatException"/>.</exception>
        public float GetStat(StatType type)
        {
            if (stats.TryGetValue(type, out float value))
                return value;
            else
                throw new NoSuchStatException(type);
        }

        /// <returns>A list of all the stat types in this object. It's a good idea to check if the base stats contains a stat before trying to get it.</returns>
        public List<StatType> GetStatTypes()
        {
            return new List<StatType>(stats.Keys);
        }
    }

    /// <summary>
    /// This exception is thrown when trying to get a stat that does not exist in the <see cref="BaseStats"/> object.
    /// </summary>
    public class NoSuchStatException : System.Exception
    {
        public NoSuchStatException(StatType type) : base("The stat " + type + " does not exist in this object") { }
    }
}
