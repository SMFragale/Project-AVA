using System.Collections.Generic;
using UnityEngine;

namespace AVA.Spawning
{
    //TODO Add more spawning methods
    [System.Serializable]
    public abstract class SpawnArea : MonoBehaviour
    {
        [SerializeField]
        private float playerDistanceToSpawn = 10f;

        public float PlayerDistanceToSpawn => playerDistanceToSpawn;

        public abstract void SpawnMultipleRandom(List<SpawnEntity> spawnEntities);
    }

}
