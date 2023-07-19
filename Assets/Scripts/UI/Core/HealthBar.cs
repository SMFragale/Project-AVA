using UnityEngine;
using AVA.Combat;
using AVA.Stats;
using AVA.UI.Stats;
using System.Collections.Generic;
using AVA.Core;

namespace AVA.UI.Core
{
    public class HealthBar : MonoWaiter
    {
        [SerializeField]
        private HPService healthService;

        [SerializeField]
        private CharacterStats characterStats;

        [SerializeField]
        private Transform statContainer;

        [SerializeField]
        private GameObject statBarPrefab;

        private void Awake()
        {
            dependencies = new List<IReadyCheck> { healthService, characterStats };
        }

        protected override void OnDependenciesReady()
        {
            var healthBar = Instantiate(statBarPrefab, statContainer);
            var healthStatBar = healthBar.GetComponent<StatBar>();
            healthStatBar.SetType(StatType.MaxHealth);

            var shieldBar = Instantiate(statBarPrefab, statContainer);
            var shieldStatBar = shieldBar.GetComponent<StatBar>();
            shieldStatBar.SetType(StatType.Defense);

            healthService.AddHealthListener(() =>
            {
                UpdateFillAmount(healthService.GetHealth(), characterStats.GetStat(StatType.MaxHealth), healthStatBar);
            });

            characterStats.AddOnStatsChangedListener(() =>
            {
                UpdateFillAmount(healthService.GetHealth(), characterStats.GetStat(StatType.MaxHealth), healthStatBar);
                UpdateFillAmount(healthService.GetShield(), characterStats.GetStat(StatType.Defense), shieldStatBar);
            });

            healthService.AddShieldListener(() =>
            {
                UpdateFillAmount(healthService.GetShield(), characterStats.GetStat(StatType.Defense), shieldStatBar);
            });

            UpdateFillAmount(healthService.GetHealth(), characterStats.GetStat(StatType.MaxHealth), healthStatBar);
            UpdateFillAmount(healthService.GetShield(), characterStats.GetStat(StatType.Defense), shieldStatBar);

            Debug.Log("Set observable value");
        }

        private void UpdateFillAmount(float current, float max, StatBar statBar)
        {
            statBar.SetFillAmount(current, max);
        }
    }
}