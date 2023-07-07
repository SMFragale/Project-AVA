using System.Collections.Generic;
using AVA.Combat;
using AVA.Stats;
using UnityEngine;
using AVA.Core;

namespace AVA.State
{
    [RequireComponent(typeof(CharacterStats))]
    [RequireComponent(typeof(HPService))]
    public class CharacterState : MonoWaiter
    {
        private void Awake()
        {
            dependencies = new List<IReadyCheck> { CharacterStatsInstance(), HPServiceInstance() };
        }

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

        private CharacterStats CharacterStatsInstance()
        {
            return GetComponent<CharacterStats>();
        }

        private HPService HPServiceInstance()
        {
            return GetComponent<HPService>();
        }

        protected override void OnDependenciesReady()
        {
            Debug.Log("CharacterState dependencies ready");
        }
        //TODO Effects
    }
}
