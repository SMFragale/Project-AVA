using UnityEngine;

namespace AVA.Stats
{

    [CreateAssetMenu(fileName = "BaseStats", menuName = "Stats/BaseStats")]
    public class BaseStatsSO : ScriptableObject
    {
        [Space(10)]
        [Header("WARNING Type must be unique for each stat")]

        [SerializeField] private BaseStatModel[] baseStats = new BaseStatModel[] {
            new BaseStatModel { type = StatType.Enum.MaxHealth, baseValue = 100 },
            new BaseStatModel { type = StatType.Enum.Attack, baseValue = 100 },
            new BaseStatModel { type = StatType.Enum.Speed, baseValue = 100 },
            new BaseStatModel { type = StatType.Enum.Defense, baseValue = 100 },
            new BaseStatModel { type = StatType.Enum.AttackSpeed, baseValue = 100 },
            new BaseStatModel { type = StatType.Enum.CritChance, baseValue = 100 },
            new BaseStatModel { type = StatType.Enum.CritDamage, baseValue = 100 }
        };

        public BaseStatModel[] GetBaseStats()
        {
            return baseStats;
        }
    }

    [System.Serializable]
    public class BaseStatModel
    {
        [Tooltip("WARNING Stat Type must be unique for each stat, duplicates will be ignored")]
        [SerializeField] public StatType.Enum type;
        [SerializeField] public float baseValue;

        public StatType.Enum GetStatType()
        {
            return type;
        }

        public float GetValue()
        {
            return baseValue;
        }
    }
}

