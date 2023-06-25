using System.Collections.Generic;
namespace AVA.Stats
{
    public class CharacterStats : StatOutput
    {
        public float GetStat(StatType type)
        {
            return statServiceInstance.CalculateStat(type);
        }

        public float GetBaseStat(StatType type)
        {
            return statServiceInstance.GetBaseStat(type);
        }

        public List<StatType> GetStatTypes()
        {
            return statServiceInstance.GetStatTypes();
        }

        public void UpdateBaseStat(StatType type, float newStat)
        {
            statServiceInstance.UpdateBaseStat(type, newStat);
        }
    }
}