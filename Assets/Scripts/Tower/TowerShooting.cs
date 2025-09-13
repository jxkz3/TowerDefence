using UnityEngine;

public class TowerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 1f;
    public float bulletDamage = 10f;   // unique per tower

    private float fireCooldown = 0f;

    void Update()
    {
        fireCooldown -= Time.deltaTime;

        if (fireCooldown <= 0f)
        {
            Shoot();
            fireCooldown = 1f / fireRate;
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.damage = bulletDamage;
        }
    }
}
