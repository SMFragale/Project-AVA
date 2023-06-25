using AVA.Stats;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

namespace AVA.UI.Stats {

    [RequireComponent(typeof(LayoutGroup))]
    public class StatContainer : MonoBehaviour
    {
        [SerializeField]
        private CharacterStats characterStats;
        private List<StatBar> statBars;
        [SerializeField] private GameObject statBarPrefab;

        private void Start() {
            StartCoroutine(SetStatBars());
        }

        private IEnumerator SetStatBars() {
            yield return new WaitUntil(() => characterStats.isReady);
            statBars = new List<StatBar>();
            foreach (StatType type in characterStats.GetStatTypes()) {
                var statBar = Instantiate(statBarPrefab, gameObject.transform).GetComponent<StatBar>();
                statBar.SetType(type);

                statBars.Add(statBar);
            }
        }

        private void Update() {
            if(statBars != null) {
                foreach (StatBar statBar in statBars)
                {
                    // The hardcoded 3 is a multiplier that will make it so the bar is not filled by itself. 
                    statBar.SetFillAmount(characterStats.GetStat(statBar.GetStatType()) /
                        (characterStats.GetBaseStat(statBar.GetStatType()) * 3)
                    );
                }
            }
        }
    }
}
