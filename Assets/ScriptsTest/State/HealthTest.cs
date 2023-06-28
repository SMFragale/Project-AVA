using UnityEngine;
using AVA.State;
using AVA.Stats;
using System.Collections;
using AVA.UI.Stats;

namespace AVA.Tests.State
{
    public class HealthTest : MonoBehaviour
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

            healthService.AddHealthListener(() => {
                healthStatBar.SetFillAmount(healthService.GetHealth() / (characterStats.GetStat(StatType.MaxHealth)));
            });

            healthService.AddShieldListener(() => {
                shieldStatBar.SetFillAmount(healthService.GetShield() / (characterStats.GetStat(StatType.MaxHealth)));
            });

            healthStatBar.SetFillAmount(healthService.GetHealth() / (characterStats.GetStat(StatType.MaxHealth)));
            shieldStatBar.SetFillAmount(healthService.GetShield() / (characterStats.GetStat(StatType.MaxHealth)));

            healthService.OnHealthZero.AddListener(HealthZero);
            Debug.Log("Set observable value");
        }

        public void TakeDamage(float value)
        {
            healthService.TakeDamage(value);
        }

        public void AddShield(float value)
        {
            healthService.AddShield(value);
        }

        public void HealthZero()
        {
            Debug.Log("Health zero");
        }

    }
}