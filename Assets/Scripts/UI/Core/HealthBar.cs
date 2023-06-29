using UnityEngine;
using AVA.State;
using AVA.Stats;
using System.Collections;
using AVA.UI.Stats;

namespace AVA.UI.Core
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField]
        private HPService healthService;

        [SerializeField]
        private CharacterStats characterStats;

        [SerializeField]
        private Transform statContainer;

        [SerializeField]
        private GameObject healthBarPrefab;

        private void Start()
        {
            StartCoroutine(Init());
        }

        private IEnumerator Init()
        {
            yield return new WaitUntil(() => healthService.isReady());
            var healthBar = Instantiate(healthBarPrefab, statContainer);
            var healthStatBar = healthBar.GetComponent<StatBar>();
            healthStatBar.SetType(StatType.MaxHealth);

            var shieldBar = Instantiate(healthBarPrefab, statContainer);
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

        private void UpdateFillAmount(float current, float max, StatBar statBar) {
            statBar.SetFillAmount(current, max);
        }

    }
}