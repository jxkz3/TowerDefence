using UnityEngine;

public class HeroSpawn : MonoBehaviour
{
    [Header("Hero Settings")]
    public GameObject heroPrefab;     // drag your hero prefab here
    public Transform spawnPoint;      // where hero appears
    public float spawnInterval = 5f;  // time between spawns

    private void Start()
    {
        InvokeRepeating(nameof(SpawnHero), 0f, spawnInterval);
    }

    void SpawnHero()
    {
        Instantiate(heroPrefab, spawnPoint.position, Quaternion.identity);
    }
}
