using UnityEngine;

namespace AVA.Stats
{
    public class CharacterStats : MonoBehaviour
    {
        [SerializeField]
        private StatService statServiceInstance;

        public float GetStat(StatType type)
        {
            return statServiceInstance.CalculateStat(type);
        }

        public float GetBaseStat(StatType type)
        {
            return statServiceInstance.GetBaseStat(type);
        }

        public void UpdateBaseStat(StatType type, float newStat)
        {
            statServiceInstance.UpdateBaseStat(type, newStat);
        }
    }
}