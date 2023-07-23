using System.Collections.Generic;

namespace AVA.Spawning
{

    //TODO Add more spawning methods
    public interface SpawnArea
    {
        void SpawnMultipleRandom(List<SpawnEntity> spawnEntities);
    }

}
