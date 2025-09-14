using UnityEngine;

public class TowerShooting : MonoBehaviour
{
    [Header("Tower Settings")]
    public float range = 10f;
    public float rotationSpeed = 5f;
    public float fireRate = 1f;
    public float bulletDamage = 10f;

    [Header("References")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public Transform rotatingPart;

    private float fireCooldown = 0f;
    private Transform targetEnemy;

    void Update()
    {
        FindTarget();

        if (targetEnemy == null) return;

        RotateTowardTarget();

        fireCooldown -= Time.deltaTime;
        if (fireCooldown <= 0f)
        {
            Shoot();
            fireCooldown = 1f / fireRate;
        }
    }

    void FindTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < shortestDistance && distance <= range)
            {
                shortestDistance = distance;
                nearestEnemy = enemy;
            }
        }

        targetEnemy = nearestEnemy != null ? nearestEnemy.transform : null;
    }

    void RotateTowardTarget()
    {
        Vector3 dir = targetEnemy.position - rotatingPart.position;
        dir.y = 0f; // ignore height so tower only rotates horizontally

        if (dir == Vector3.zero) return;

        Quaternion lookRot = Quaternion.LookRotation(dir);
        rotatingPart.rotation = Quaternion.Lerp(rotatingPart.rotation, lookRot, Time.deltaTime * rotationSpeed);
    }

    void Shoot()
    {
        if (targetEnemy == null) return;

        // Spawn bullet
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Send target info to bullet
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            // Use enemy collider center for accurate height
            Collider enemyCollider = targetEnemy.GetComponent<Collider>();
            Vector3 targetPos = enemyCollider != null ? enemyCollider.bounds.center : targetEnemy.position;

            bulletScript.SetTarget(targetPos, targetEnemy.gameObject);
            bulletScript.damage = bulletDamage;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
