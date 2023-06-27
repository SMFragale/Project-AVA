using UnityEngine;
using AVA.State;
using AVA.Stats;
using System.Collections;
using AVA.UI.Core;

namespace AVA.Tests.State {
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

        private void Start() {
            StartCoroutine(Init());
        }

        private IEnumerator Init() {
            yield return new WaitUntil(() => healthService.isReady());
            var healthBar = Instantiate(healthBarPrefab, statContainer);
            var observableValueBar = healthBar.GetComponent<ObservableFloatBar>();
            observableValueBar.SetObservableValue(healthService.GetObservableHealth());
            observableValueBar.SetMaxValue(characterStats.GetStat(StatType.MaxHealth));
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



    }
}