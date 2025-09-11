using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemyPrefab;   // Drag your enemy prefab here
    public Transform spawnPoint;     // Where the enemy appears
    public float spawnInterval = 2f; // Time between spawns
    public Transform[] waypoints;    // Assign your scene waypoints here

    private void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 0f, spawnInterval);
    }

    void SpawnEnemy()
    {
        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

        // Give the enemy the waypoint list
        enemy.GetComponent<EnemyPath>().waypoints = waypoints;
    }
}
