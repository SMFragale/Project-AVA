using AVA.Stats;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using AVA.Core;

namespace AVA.UI.Stats
{

    [RequireComponent(typeof(LayoutGroup))]
    public class StatContainer : MonoWaiter
    {
        [SerializeField]
        private CharacterStats characterStats;
        [SerializeField] private GameObject statBarPrefab;

        private void Awake()
        {
            dependencies = new List<IReadyCheck> { characterStats };
        }

        protected override void OnDependenciesReady()
        {
            Dictionary<StatType, StatBar> statBars = new Dictionary<StatType, StatBar>();

            foreach (var type in characterStats.GetStatTypes())
            {
                var statBar = Instantiate(statBarPrefab, gameObject.transform).GetComponent<StatBar>();
                statBar.SetType(type);
                statBars.Add(type, statBar);
                characterStats.AddStatListener(type, () =>
                {
                    statBar.SetFillAmount(characterStats.GetStat(type), type.maxValue);
                });
                statBar.SetFillAmount(characterStats.GetStat(type), type.maxValue);
            }
        }
    }
}
