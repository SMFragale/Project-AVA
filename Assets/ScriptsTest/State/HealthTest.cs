using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AVA.State;
using AVA.Core;
using AVA.Stats;

namespace AVA.Tests.State {
    public class HealthTest : MonoBehaviour
    {
        [SerializeField]
        private HPService healthService;

        [SerializeField]
        private CharacterStats characterStats;

        public void TakeDamage(float value)
        {
            healthService.TakeDamage(value);
        }

        public void AddShield(float value)
        {
            healthService.AddShield(value);
        }

    }
}