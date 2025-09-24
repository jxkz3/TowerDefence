using UnityEngine;

public class Wall : MonoBehaviour
{
    [Header("Wall Stats")]
    public float maxHealth = 50f;
    private float currentHealth;

    [Header("UI")]
    public HealthBar healthBar; // drag your HealthBar component in Inspector

    public float Health => currentHealth;

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
            Destroy(gameObject); // wall breaks
        }
    }
}
