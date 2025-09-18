using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Enemy Stats")]
    public float maxHealth = 30f;
    private float currentHealth;

    [Header("Reward")]
    public int coinReward = 10;

    [Header("Effects")]
    public GameObject deathEffect;

    [Header("UI")]
    public HealthBar healthBar;  // drag your HealthBar component here in Inspector

    void Start()
    {
        currentHealth = maxHealth;

        if (healthBar != null)
            healthBar.SetHealth(currentHealth, maxHealth);
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
        if (CoinManager.Instance != null)
        {
            CoinManager.Instance.AddCoins(coinReward);
        }

        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
