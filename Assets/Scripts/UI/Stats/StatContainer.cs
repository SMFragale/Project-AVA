using AVA.Stats;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

namespace AVA.UI.Stats
{

    [RequireComponent(typeof(LayoutGroup))]
    public class StatContainer : MonoBehaviour
    {
        [SerializeField]
        private CharacterStats characterStats;
        private List<StatBar> statBars;
        [SerializeField] private GameObject statBarPrefab;

        private void Start()
        {
            StartCoroutine(Init());
        }

        private IEnumerator Init()
        {
            yield return new WaitUntil(() => characterStats.isReady());
            Dictionary<StatType, StatBar> statBars = new Dictionary<StatType, StatBar>();

            foreach (var type in characterStats.GetStatTypes())
            {
                var statBar = Instantiate(statBarPrefab, gameObject.transform).GetComponent<StatBar>();
                statBar.SetType(type);
                statBars.Add(type, statBar);
                characterStats.AddStatListener(type, () => {
                    statBar.SetFillAmount(characterStats.GetStat(type) / type.maxValue);
                });
                statBar.SetFillAmount(characterStats.GetStat(type) / type.maxValue);
            } 
        }
    }
}
