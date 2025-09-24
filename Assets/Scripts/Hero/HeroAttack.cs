using UnityEngine;

public class HeroAttack : MonoBehaviour
{
    [Header("Attack Settings")]
    public float damage = 5f;          // how much damage per hit
    public float attackRate = 1f;      // attacks per second
    public float attackRange = 1.5f;   // how close hero needs to be to attack

    private float attackCooldown = 0f;
    private Transform targetEnemy;

    void Update()
    {
        // reduce cooldown over time
        if (attackCooldown > 0f)
            attackCooldown -= Time.deltaTime;

        FindClosestEnemy();

        if (targetEnemy != null)
        {
            float distance = Vector3.Distance(transform.position, targetEnemy.position);
            if (distance <= attackRange && attackCooldown <= 0f)
            {
                Attack(targetEnemy);
                attackCooldown = 1f / attackRate; // reset cooldown
            }
        }
    }

    void FindClosestEnemy()
    {
        EnemyHealth[] enemies = FindObjectsByType<EnemyHealth>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        float closestDistance = Mathf.Infinity;
        targetEnemy = null;

        foreach (EnemyHealth enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                targetEnemy = enemy.transform;
            }
        }
    }

    void Attack(Transform enemy)
    {
        EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(damage);
            Debug.Log("Hero attacked enemy!");
        }
    }
}
