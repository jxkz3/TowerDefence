using UnityEngine;

public class EnemyTracker : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f;            // movement speed
    public float stoppingDistance = 1f; // how close to get before stopping

    private Transform targetEnemy;

    void Update()
    {
        FindClosestEnemy();
        MoveTowardsEnemy();
    }

    void FindClosestEnemy()
    {
        EnemyAttack[] enemies = FindObjectsByType<EnemyAttack>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        float closestDistance = Mathf.Infinity;
        targetEnemy = null;

        foreach (EnemyAttack enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                targetEnemy = enemy.transform;
            }
        }
    }

    void MoveTowardsEnemy()
    {
        if (targetEnemy == null) return;

        Vector3 direction = (targetEnemy.position - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, targetEnemy.position);

        if (distance > stoppingDistance)
        {
            transform.position += direction * speed * Time.deltaTime;
        }
    }
}
