using System.Collections;
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

        private int maxIterationsPerTick = 1000;

        //TODO Restrict spawning to a maximum number of entities at a time and pool entities, also make it so enemies can only spawn at a certain distance from the player.

        private void Awake()
        {
            ValidateSelf();
            ValidateWaveModel();
            CalculateSpawnProbabilityDistribution();
            Debug.Log($"Spawn probability distribution for {waveModel.name}: {string.Join(", ", spawnEntityDistribution)}");
            remainingCurrency = waveModel.WaveCurrency;
        }

        private void Start()
        {
            StartCoroutine(SpawnUntilCurrencyDepleted());
        }

        public IEnumerator SpawnUntilCurrencyDepleted()
        {
            while (remainingCurrency > 0)
            {
                yield return new WaitForSeconds(Random.Range(waveModel.SpawnTimeRange.Min, waveModel.SpawnTimeRange.Max));
                SpawnTick();
            }
            Debug.Log("Wave currency depleted, stopping spawning");
        }

        private void SpawnTick()
        {
            var currencyToSpend = waveModel.CurrencyPerTimeRange;
            var currencySpent = 0;
            var iterations = 0;

            List<SpawnEntity> spawnEntities = new();
            SpawnEntity newEntity = GetRandomSpawnEntity();

            Debug.Log("Creating new entities for this tick");
            do
            {
                currencySpent += newEntity.CurrencyCost;
                if (currencySpent > currencyToSpend)
                    break;

                spawnEntities.Add(newEntity);
                remainingCurrency -= newEntity.CurrencyCost;
                newEntity = GetRandomSpawnEntity();
                iterations++;
            }
            while (newEntity != null && currencySpent <= currencyToSpend && iterations < maxIterationsPerTick);

            Debug.Log($"Spawning {spawnEntities.Count} entities for this tick for a cost of {currencySpent}, {currencySpent * 100.0f / currencyToSpend}% efficiency");

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

            if (maxSpawnEntityCost > waveModel.WaveCurrency || maxSpawnEntityCost > waveModel.CurrencyPerTimeRange)
                Debug.LogError($"Wave Model {waveModel.name} has a Spawn Entity with a cost greater than the Wave Currency or the currency per time range, this entity can never spawn");
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
