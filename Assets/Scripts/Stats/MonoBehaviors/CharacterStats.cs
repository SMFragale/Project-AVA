using System.Collections.Generic;
using AVA.Core;
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

        public void UpdateBaseStat(StatType type, float newStat)
        {
            statServiceInstance.UpdateBaseStat(type, newStat);
        }

        public Dictionary<StatType, ObservableValue<float>> GetAllCalculatedStats()
        {
            return statServiceInstance.GetAllCalculatedStats();
        }

        public Dictionary<StatType, ObservableValue<float>> GetAllBaseStats() {
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

        public void AddBaseStatListener(StatType type, UnityAction listener)
        {
            statServiceInstance.AddBaseStatListener(type, listener);
        }

        public void RemoveBaseStatListener(StatType type, UnityAction listener)
        {
            statServiceInstance.RemoveBaseStatListener(type, listener);
        }
        
    }
}