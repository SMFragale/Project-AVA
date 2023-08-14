using System;
using System.Collections.Generic;
using UnityEngine;

namespace AVA.Spawning
{
    //TODO implement spawning logic that spawns enemies within the spawnRadius and within the NavMesh. Also make it so enemies can only spawn at a certain distance from the player.
    public class NavSpawnCircle : SpawnArea
    {
        [SerializeField]
        private float spawnRadius;

        public float Radius => spawnRadius;

        private List<Vector3> spawnPositions = new();

        public override void SpawnMultipleRandom(List<SpawnEntity> spawnEntities)
        {
            //Spawn all the spawn entities from the list in locations within the spawn radius without overlapping with other entities.
            foreach (var spawnEntity in spawnEntities)
            {
                var spawnPosition = GetNonOverlappingSpawnPosition(spawnRadius);
                //Instantiate spawnEntity.Prefab at spawnPosition with this gameobject as parent.
                var newEntity = Instantiate(spawnEntity.Prefab, spawnPosition, Quaternion.identity, transform);
            }

            spawnPositions = new();

            Debug.Log($"Spawn Area spawning {spawnEntities.Count} entities");
        }

        private Vector3 GetNonOverlappingSpawnPosition(float spawnRadius)
        {
            var spawnPosition = GetRandomSpawnPosition(spawnRadius);
            while (IsOverlapping(spawnPosition))
            {
                spawnPosition = GetRandomSpawnPosition(spawnRadius);
            }
            spawnPositions.Add(spawnPosition);
            return spawnPosition;
        }

        private bool IsOverlapping(Vector3 spawnPosition)
        {
            var maxIterations = 1000;
            var currentIteration = 0;
            //Check if the spawn position is overlapping with any other spawn positions.
            foreach (var position in spawnPositions)
            {
                if (currentIteration >= maxIterations)
                {
                    throw new System.Exception("Could not find a non-overlapping spawn position within the maximum number of iterations, consider making your spawn radius bigger or reducing the number of entities to spawn at a time.");
                }
                if (Vector3.Distance(spawnPosition, position) < 1)
                {
                    return true;
                }
            }
            return false;
        }

        private Vector3 GetRandomSpawnPosition(float spawnRadius)
        {
            //Get a random spawn position based on the spawn radius.
            var randomSpawnPosition = UnityEngine.Random.insideUnitCircle * spawnRadius;
            return new Vector3(randomSpawnPosition.x, 0, randomSpawnPosition.y) + transform.position;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, spawnRadius);
        }
    }

}