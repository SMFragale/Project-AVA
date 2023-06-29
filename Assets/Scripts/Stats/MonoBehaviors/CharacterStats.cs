using System.Collections.Generic;
using UnityEngine.Events;

namespace AVA.Stats
{
    public class CharacterStats : StatOutput
    {
        public float GetStat(StatType type)
        {
            return statServiceInstance.GetCalculatedStat(type);
        }

        public float GetBaseStat(StatType type)
        {
            return statServiceInstance.GetBaseStat(type);
        }

        public List<StatType> GetStatTypes()
        {
            return statServiceInstance.GetStatTypes();
        }

        public Dictionary<StatType, float> GetAllCalculatedStats()
        {
            return statServiceInstance.GetAllCalculatedStats();
        }

        public Dictionary<StatType, float> GetAllBaseStats() {
            return statServiceInstance.GetAllBaseStats();
        }

        public void AddStatListener(StatType type, UnityAction listener)
        {
            statServiceInstance.AddStatListener(type, listener);
        }

        public void RemoveStatListener(StatType type, UnityAction listener)
        {
            statServiceInstance.RemoveStatListener(type, listener);
        }
        
    }
}