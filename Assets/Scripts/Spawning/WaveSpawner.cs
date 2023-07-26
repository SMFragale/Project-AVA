using System.Collections.Generic;
using AVA.AI;
using UnityEngine;

namespace AVA.Spawning
{
    public class WaveSpawner : MonoBehaviour
    {
        [SerializeField]
        private WaveSO waveModel;

        [SerializeField]
        private List<SpawnArea> spawnAreas = new();

        private int remainingCurrency;

        private Dictionary<SpawnEntity, float> spawnEntityDistribution;

        //These values are consistent with the range values in WaveSO. Because the Range attribute cannot be accessed by code we must persist these values manually.
        private float minSpawnEntityCost = 10000;
        private float maxSpawnEntityCost = 0;

        //TODO Connect to SpawnArea, implement spawning logic based on currency, restrict spawning to a maximum number of entities at a time and pool entities, also make it so enemies can only spawn at a certain distance from the player.

        private void Awake()
        {
            ValidateSelf();
            ValidateWaveModel();
            CalculateSpawnProbabilityDistribution();
            Debug.Log($"Spawn probability distribution for {waveModel.name}: {string.Join(", ", spawnEntityDistribution)}");
            remainingCurrency = waveModel.WaveCurrency;
        }

        public void SpawnUntilCurrencyDepleted()
        {
            while (remainingCurrency > 0)
            {
                SpawnTick();
            }
        }

        private void SpawnTick()
        {
            var currencyToSpend = waveModel.CurrencyPerTimeRange;


            List<SpawnEntity> spawnEntities = new();
            SpawnEntity newEntity = GetRandomSpawnEntity();
            //Fill the list until no newEntity can be added which means there is no more currency left to spend or the amount of currency left is less than the minimum cost of any entity in this model.
            while (newEntity != null && remainingCurrency >= minSpawnEntityCost)
            {
                spawnEntities.Add(newEntity);
                currencyToSpend += newEntity.CurrencyCost;
                remainingCurrency -= newEntity.CurrencyCost;
                newEntity = GetRandomSpawnEntity();
            }

            if (spawnEntities.Count > 0)
            {
                SpawnArea spawnArea = GetRandomSpawnArea();
                spawnArea.SpawnMultipleRandom(spawnEntities);
                remainingCurrency -= currencyToSpend;
            }
        }

        private SpawnArea GetRandomSpawnArea()
        {
            return spawnAreas[Random.Range(0, spawnAreas.Count)];
        }

        private SpawnEntity GetRandomSpawnEntity()
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
            throw new System.Exception("There was an error while generating a new spawn entity for the wave distribution.");
        }

        private void CalculateSpawnProbabilityDistribution()
        {
            float totalPriority = 0f;
            spawnEntityDistribution = new Dictionary<SpawnEntity, float>();
            foreach (SpawnEntity spawnEntity in waveModel.SpawnEntities)
            {
                totalPriority += spawnEntity.Priority;
                RecalculateMinMaxEntityCost(spawnEntity);
            }
            foreach (SpawnEntity spawnEntity in waveModel.SpawnEntities)
            {
                spawnEntityDistribution[spawnEntity] = spawnEntity.Priority / totalPriority;
            }
        }

        private void RecalculateMinMaxEntityCost(SpawnEntity entity)
        {
            if (entity.CurrencyCost < minSpawnEntityCost)
                minSpawnEntityCost = entity.CurrencyCost;
            if (entity.CurrencyCost > maxSpawnEntityCost)
                maxSpawnEntityCost = entity.CurrencyCost;
        }

        private void ValidateSelf()
        {
            if (spawnAreas == null || spawnAreas.Count == 0)
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
