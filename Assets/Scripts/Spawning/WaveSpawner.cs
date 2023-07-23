using System.Collections.Generic;
using UnityEngine;

namespace AVA.Spawning
{
    public class WaveSpawner : MonoBehaviour
    {
        [SerializeField]
        private WaveSO waveModel;

        [SerializeField]
        private List<SpawnArea> spawnAreas;

        private int remainingCurrency;

        private Dictionary<SpawnEntity, float> spawnEntityDistribution;

        //TODO Connect to SpawnArea, implement spawning logic based on currency, restrict spawning to a maximum number of entities at a time and pool entities, also make it so enemies can only spawn at a certain distance from the player.

        private void Awake()
        {
            ValidateSelf();
            ValidateWaveModel();
            CalculateSpawnProbabilityDistribution();
            Debug.Log($"Spawn probability distribution for {waveModel.name}: {string.Join(", ", spawnEntityDistribution)}");
            remainingCurrency = waveModel.WaveCurrency;
        }

        public SpawnEntity GetRandomSpawnEntity()
        {
            float randomValue = Random.value;
            foreach (KeyValuePair<SpawnEntity, float> pair in spawnEntityDistribution)
            {
                randomValue -= pair.Value;
                if (randomValue <= 0f)
                {
                    return pair.Key;
                }
            }
            return null;
        }

        private void CalculateSpawnProbabilityDistribution()
        {
            float totalPriority = 0f;
            spawnEntityDistribution = new Dictionary<SpawnEntity, float>();
            foreach (SpawnEntity spawnEntity in waveModel.SpawnEntities)
            {
                totalPriority += spawnEntity.Priority;
            }
            foreach (SpawnEntity spawnEntity in waveModel.SpawnEntities)
            {
                spawnEntityDistribution[spawnEntity] = spawnEntity.Priority / totalPriority;
            }
        }

        private void ValidateSelf()
        {
            if (spawnAreas.Count == 0)
                throw new System.Exception($"Wave Spawner {name} has no Spawn Areas");
        }

        private void ValidateWaveModel()
        {
            if (waveModel.SpawnEntities.Count == 0)
                throw new System.Exception($"Wave Model {waveModel.name} has no Spawn Entities");
            if (waveModel.SpawnTimeRange.Min > waveModel.SpawnTimeRange.Max)
                throw new System.Exception($"Wave Model {waveModel.name} has invalid Spawn Time Range");
            if (waveModel.WaveCurrency < 1)
                throw new System.Exception($"Wave Model {waveModel.name} has invalid Wave Currency");
            if (waveModel.CurrencyPerTimeRange < 1)
                throw new System.Exception($"Wave Model {waveModel.name} has invalid Currency Per Time Range");
        }

    }
}
