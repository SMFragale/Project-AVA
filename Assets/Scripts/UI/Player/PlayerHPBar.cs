using AVA.Combat;
using AVA.Core;
using AVA.Stats;
using AVA.UI.Stats;
using UnityEngine;

namespace AVA.UI
{
    public class PlayerHPBars : MonoWaiter
    {
        [SerializeField]
        private HPBar healthBar;

        [SerializeField]
        private HPBar shieldBar;

        [SerializeField]
        private CharacterStats characterStats;

        [SerializeField]
        private HPService healthService;

        private void Awake()
        {
            dependencies = new() { characterStats, healthService };
        }

        protected override void OnDependenciesReady()
        {
            healthBar.SetType(StatType.MaxHealth);
            shieldBar.SetType(StatType.Defense);

            healthService.AddHealthListener(() =>
            {
                UpdateFillAmount(healthService.GetHealth(), characterStats.GetStat(StatType.MaxHealth), healthBar);
            });

            characterStats.AddOnStatsChangedListener(() =>
            {
                UpdateFillAmount(healthService.GetHealth(), characterStats.GetStat(StatType.MaxHealth), healthBar);
                UpdateFillAmount(healthService.GetShield(), characterStats.GetStat(StatType.Defense), shieldBar);
            });

            healthService.AddShieldListener(() =>
            {
                UpdateFillAmount(healthService.GetShield(), characterStats.GetStat(StatType.Defense), shieldBar);
            });

            UpdateFillAmount(healthService.GetHealth(), characterStats.GetStat(StatType.MaxHealth), healthBar);
            UpdateFillAmount(healthService.GetShield(), characterStats.GetStat(StatType.Defense), shieldBar);
        }

        private void UpdateFillAmount(float current, float max, HPBar statBar)
        {
            statBar.SetFillAmount(current, max);
        }
    }
}
