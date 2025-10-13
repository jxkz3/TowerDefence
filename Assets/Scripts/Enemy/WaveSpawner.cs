using UnityEngine;
using System.Collections;

[System.Serializable]
public class EnemyGroup
{
    public int prefabIndex;       // index of prefab in EnemySpawn.enemyPrefabs
    public int count;             // how many to spawn
    public float spawnInterval;   // time between spawns
}

[System.Serializable]
public class Wave
{
    public EnemyGroup[] enemyGroups; // multiple enemy types per wave
    public float cooldown;           // delay before next wave
}

public class WaveSpawner : MonoBehaviour
{
    public EnemySpawn spawner; // reference to EnemySpawn
    public Wave[] waves;

    private void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    private IEnumerator SpawnWaves()
    {
        for (int i = 0; i < waves.Length; i++)
        {
            Wave wave = waves[i];
            Debug.Log("Starting Wave " + (i + 1));

            foreach (var group in wave.enemyGroups)
            {
                // validate prefabIndex
                if (group.prefabIndex < 0 || group.prefabIndex >= spawner.enemyPrefabs.Length)
                {
                    Debug.LogWarning("Invalid prefab index: " + group.prefabIndex);
                    continue;
                }

                GameObject prefab = spawner.enemyPrefabs[group.prefabIndex];

                for (int j = 0; j < group.count; j++)
                {
                    spawner.SpawnEnemy(prefab); // uses SpawnEnemy(prefab)
                    yield return new WaitForSeconds(group.spawnInterval);
                }
            }

            Debug.Log("Wave " + (i + 1) + " complete. Cooldown for " + wave.cooldown + " seconds.");
            yield return new WaitForSeconds(wave.cooldown);
        }

        Debug.Log("All waves completed!");
    }
}
