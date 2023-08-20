using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace AVA.Stats
{
    /// <summary>
    /// Reresents an asset that will store the base stats for a character (may be a player or other entity).
    /// These BaseStatsSO may contain any combination of stats defined in <see cref="StatType.Enum"/>, but each stat type must be unique.
    /// This is a Scriptable Object meant to be created in the editor.
    /// </summary>
    [CreateAssetMenu(fileName = "BaseStats", menuName = "Stats/BaseStats")]
    public class BaseStatsSO : ScriptableObject
    {
        [Space(10)]
        [Header("WARNING Type must be unique for each stat")]

        [SerializeField]
        private List<BaseStatModel> baseStats = new List<BaseStatModel> {
            new BaseStatModel { type = StatType.Enum.MaxHealth, baseValue = 100 },
            new BaseStatModel { type = StatType.Enum.Attack, baseValue = 100 },
            new BaseStatModel { type = StatType.Enum.Speed, baseValue = 100 },
            new BaseStatModel { type = StatType.Enum.Defense, baseValue = 100 },
            new BaseStatModel { type = StatType.Enum.AttackSpeed, baseValue = 100 },
            new BaseStatModel { type = StatType.Enum.CritChance, baseValue = 100 },
            new BaseStatModel { type = StatType.Enum.CritDamage, baseValue = 100 }
        };

        /// <returns>A list of all the base stats.</returns>
        public List<BaseStatModel> GetBaseStats()
        {
            return baseStats;
        }

        private void OnValidate()
        {
            baseStats = baseStats.GroupBy(x => x.type).Select(x => x.First()).ToList();
        }
    }

    [System.Serializable]
    /// <summary>
    /// Represents a single stat in a <see cref="BaseStatsSO"/>.
    /// </summary>
    public class BaseStatModel
    {
        [Tooltip("WARNING Stat Type must be unique for each stat, duplicates will be ignored")]
        [SerializeField] public StatType.Enum type;
        [SerializeField] public float baseValue;
    }
}

