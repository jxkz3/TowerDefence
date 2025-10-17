using UnityEngine;

public class PlayerAutoAimShoot : MonoBehaviour
{
    [Header("Targeting Settings")]
    public float range = 20f;
    public float fieldOfView = 90f;
    public float rotationSpeed = 5f;

    [Header("Shooting Settings")]
    public float fireRate = 2f;
    public float bulletDamage = 10f;

    [Header("References")]
    public Transform firePoint; // upper body/gun
    public GameObject bulletPrefab;

    private Transform targetEnemy;
    private float fireCooldown = 0f;

    void Update()
    {
        FindFrontTarget();
        RotateTowardTarget();

        fireCooldown -= Time.deltaTime;

        if (targetEnemy != null && fireCooldown <= 0f)
        {
            ShootAtTarget(targetEnemy);
            fireCooldown = 1f / fireRate;
        }
    }

    void FindFrontTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float closestDistance = Mathf.Infinity;
        Transform nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            Vector3 dirToEnemy = enemy.transform.position - firePoint.position;
            float distance = dirToEnemy.magnitude;
            if (distance > range) continue;

            float angle = Vector3.Angle(firePoint.forward, dirToEnemy);
            if (angle <= fieldOfView / 2f)
            {
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    nearestEnemy = enemy.transform;
                }
            }
        }

        targetEnemy = nearestEnemy;
    }

    void RotateTowardTarget()
    {
        if (targetEnemy == null || firePoint == null) return;

        Vector3 direction = targetEnemy.position - firePoint.position;
        direction.y = 0f; // horizontal only
        if (direction.sqrMagnitude < 0.01f) return;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        firePoint.rotation = Quaternion.Lerp(firePoint.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }

    void ShootAtTarget(Transform enemy)
    {
        if (enemy == null || firePoint == null) return;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            Collider col = enemy.GetComponent<Collider>();
            Vector3 targetPos = col ? col.bounds.center : enemy.position;
            bulletScript.SetTarget(targetPos, enemy.gameObject);
            bulletScript.damage = bulletDamage;
        }
    }

    void OnDrawGizmosSelected()
    {
        if (firePoint == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(firePoint.position, range);
    }
}
