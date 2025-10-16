using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // assign multiple prefabs here
    public Transform spawnPoint;
    public Transform[] waypoints;

    // Spawn a specific prefab
    public void SpawnEnemy(GameObject prefab)
    {
        if (prefab == null) return;

        GameObject enemy = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
        EnemyPath path = enemy.GetComponent<EnemyPath>();
        if (path != null)
            path.waypoints = waypoints;
    }
}
// Compare this snippet from Assets/Scripts/Enemy/EnemyAttack.cs: