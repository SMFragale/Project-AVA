using UnityEngine;

namespace AVA.Stats
{

    [CreateAssetMenu(fileName = "BaseStats", menuName = "Stats/BaseStats")]
    public class BaseStatsSO : ScriptableObject
    {
        [Space(10)]
        [Header("WARNING Type must be unique for each stat")]

        [SerializeField] private BaseStatModel[] baseStats = new BaseStatModel[] { 
            new BaseStatModel { type = StatType.MaxHealth, baseValue = 100 },
            new BaseStatModel { type = StatType.Attack, baseValue = 100 },
            new BaseStatModel { type = StatType.Speed, baseValue = 100 },
            new BaseStatModel { type = StatType.Defense, baseValue = 100 },
            new BaseStatModel { type = StatType.AttackSpeed, baseValue = 100 }
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
        [SerializeField] public StatType type;
        [SerializeField] public float baseValue;

        public StatType GetStatType()
        {
            return type;
        }

        public float GetValue()
        {
            return baseValue;
        }
    }
}

