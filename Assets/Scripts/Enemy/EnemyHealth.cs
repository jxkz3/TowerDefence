using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Enemy Stats")]
    public float maxHealth = 30f;
    private float currentHealth;

    [Header("Reward")]
    public int coinReward = 10;   // how many coins this enemy gives

    [Header("Effects")]
    public GameObject deathEffect;   // optional explosion / particle effect

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        // Reward player
        if (CoinManager.Instance != null)
        {
            CoinManager.Instance.AddCoins(coinReward);
        }

        // Death effect
        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }

        Destroy(gameObject); // remove enemy
    }
}
