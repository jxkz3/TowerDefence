using UnityEngine;

public class HeroHealth : MonoBehaviour
{
    [Header("Hero Stats")]
    public float maxHealth = 50f;
    private float currentHealth;

    [Header("UI")]
    public HealthBar healthBar; // assign a HealthBar for the hero

    [Header("Damage Settings")]
    public float damagePerHit = 10f; // damage taken per enemy touch
    public float damageCooldown = 1f; // prevent rapid damage
    private float damageTimer = 0f;

    void Start()
    {
        currentHealth = maxHealth;

        if (healthBar != null)
            healthBar.SetHealth(currentHealth, maxHealth);
    }

    void Update()
    {
        if (damageTimer > 0f)
            damageTimer -= Time.deltaTime;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        if (healthBar != null)
            healthBar.SetHealth(currentHealth, maxHealth);

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Hero Died!");
        Destroy(gameObject);
        // Add logic for hero death (disable, respawn, game over, etc.)
    }

    private void OnCollisionEnter(Collision collision)
    {
        EnemyAttack enemy = collision.gameObject.GetComponent<EnemyAttack>();
        if (enemy != null && damageTimer <= 0f)
        {
            TakeDamage(damagePerHit);
            damageTimer = damageCooldown;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        EnemyAttack enemy = other.GetComponent<EnemyAttack>();
        if (enemy != null && damageTimer <= 0f)
        {
            TakeDamage(damagePerHit);
            damageTimer = damageCooldown;
        }
    }
}
