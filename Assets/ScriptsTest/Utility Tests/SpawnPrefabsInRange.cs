using UnityEngine;
//Author: GPT 3.5

public class SpawnPrefabsInRange : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public Transform playerTransform;
    public float minDistance = 5f;
    public float maxDistance = 10f;
    public float spawnIntervalMin = 3f;
    public float spawnIntervalMax = 5f;

    private float nextSpawnTime;

    void Start()
    {
        nextSpawnTime = Time.time + Random.Range(spawnIntervalMin, spawnIntervalMax);
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnPrefab();
            nextSpawnTime = Time.time + Random.Range(spawnIntervalMin, spawnIntervalMax);
        }
    }

    void SpawnPrefab()
    {
        // Calculate a random position within the distance range around the player
        Vector3 spawnPosition = playerTransform.position + Random.onUnitSphere * Random.Range(minDistance, maxDistance);

        // Ensure the prefab is spawned at ground level (you can modify this to fit your needs)
        spawnPosition.y = 0f;

        // Instantiate the prefab at the calculated position and make it face the player
        GameObject newPrefab = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
        TargetedRangeWeapon trw = newPrefab.GetComponent<TargetedRangeWeapon>();
        trw.target = playerTransform;
        trw.CharacterTransform = playerTransform;
        trw.AttackRate = 3f;
        newPrefab.transform.LookAt(playerTransform);
    }
}
