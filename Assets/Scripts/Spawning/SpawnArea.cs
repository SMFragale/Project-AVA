using System.Collections.Generic;
using UnityEngine;

namespace AVA.Spawning
{
    //TODO Add more spawning methods
    [System.Serializable]
    public abstract class SpawnArea : MonoBehaviour
    {
        public abstract void SpawnMultipleRandom(List<SpawnEntity> spawnEntities);
    }

}
