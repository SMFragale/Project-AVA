using System.Collections.Generic;
using UnityEngine;

namespace AVA.Spawning
{
    //TODO implement spawning logic that spawns enemies within the spawnRadius and within the NavMesh. Also make it so enemies can only spawn at a certain distance from the player.
    public class NavSpawnCircle : SpawnArea
    {
        [SerializeField]
        private float spawnRadius;

        public override void SpawnMultipleRandom(List<SpawnEntity> spawnEntities)
        {
            throw new System.NotImplementedException();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, spawnRadius);
        }
    }

}