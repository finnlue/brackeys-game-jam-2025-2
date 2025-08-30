using System.Collections;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] obstacleSpawnPoints;
    [SerializeField] private Transform[] zombieSpawnPoints;
    [SerializeField] private GameObject[] furniturePrefabs;
    [SerializeField] private GameObject[] zombiePrefabs;


    public float spawnTimer = 5f; // Spawn zombies from spawners every x seconds
    public int spawnAmount = 50; // Amount to be spawned

    public void SpawnFurniture()
    {
        foreach (Transform spawn in obstacleSpawnPoints)
        {
            int randomIndex = Random.Range(0, furniturePrefabs.Length);
            GameObject fPrefab = furniturePrefabs[randomIndex];

            Quaternion randomRotation = Quaternion.Euler(0, Random.Range(0f, 360f), 0);

            Instantiate(fPrefab, spawn.position, randomRotation);
        }
    }

    public IEnumerator SpawnZombiesCoroutine()
    {
        int count = 0;

        while (count < spawnAmount)
        {
            foreach (Transform spawn in zombieSpawnPoints)
            {
                if (count >= spawnAmount) break;

                int randomIndex = Random.Range(0, zombiePrefabs.Length);
                GameObject zPrefab = zombiePrefabs[randomIndex];

                Instantiate(zPrefab, spawn.position, spawn.rotation);
                count++;
            }

            yield return new WaitForSeconds(spawnTimer);
        }
    }

    public void SpawnZombies()
    {
        foreach (Transform spawn in zombieSpawnPoints)
        {
                int randomIndex = Random.Range(0, zombiePrefabs.Length);
                GameObject zPrefab = zombiePrefabs[randomIndex];

                Instantiate(zPrefab, spawn.position, spawn.rotation);
        }
    }
}
