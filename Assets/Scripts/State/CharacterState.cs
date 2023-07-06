using System.Collections.Generic;
using AVA.Combat;
using AVA.Stats;
using UnityEngine;
using AVA.Core;

namespace AVA.State
{
    [RequireComponent(typeof(CharacterStats))]
    [RequireComponent(typeof(HPService))]
    public class CharacterState : MonoBehaviour, IReadyCheck
    {
        public Dictionary<StatType, float> GetCurrentStats()
        {
            return CharacterStatsInstance().GetAllCalculatedStats();
        }

        public float GetCurrentHealth()
        {
            return HPServiceInstance().GetHealth();
        }

        public float GetCurrentShield()
        {
            return HPServiceInstance().GetShield();
        }

        public CharacterStateInstance GetStateInstance()
        {
            return new CharacterStateInstance(GetCurrentStats(), GetCurrentHealth(), GetCurrentShield());
        }

        public bool isReady()
        {
            return CharacterStatsInstance().isReady() && HPServiceInstance().isReady();
        }

        private CharacterStats CharacterStatsInstance()
        {
            return GetComponent<CharacterStats>();
        }

        private HPService HPServiceInstance()
        {
            return GetComponent<HPService>();
        }

        //TODO Effects
    }
}
