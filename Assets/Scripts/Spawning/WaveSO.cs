using System.Collections.Generic;
using UnityEngine;

namespace AVA.Spawning
{
    [CreateAssetMenu(fileName = "Wave", menuName = "AVA/Spawning/Wave Model")]
    public class WaveSO : ScriptableObject
    {
        [SerializeField]
        private List<SpawnEntity> spawnEntities;

        [SerializeField]
        private TimeRange spawnTimeRange;

        [Range(1, 10000)]
        [SerializeField]
        private int waveCurrency;

        [Range(1, 10000)]
        [SerializeField]
        private int currencyPerTimeRange;

        public HashSet<SpawnEntity> SpawnEntities { get => new(spawnEntities); }
        public TimeRange SpawnTimeRange { get => spawnTimeRange; }
        public int WaveCurrency { get => waveCurrency; }
        public int CurrencyPerTimeRange { get => currencyPerTimeRange; }
    }
}
