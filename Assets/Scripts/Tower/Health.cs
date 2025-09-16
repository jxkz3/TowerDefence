using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Tower Stats")]
    public float maxHealth = 50f;
    private float currentHealth;

    [Header("Death Settings")]
    public GameObject deathEffect;

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
        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }

        Destroy(gameObject); // tower is destroyed
    }

    // When an enemy collides, it damages the tower
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(10f); // you can tweak this damage value or make it per enemy
            Debug.Log("Tower hit by enemy! Current Health: " + currentHealth);
        }
    }

    // (Optional) if you use trigger colliders instead of physics collisions
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            TakeDamage(10f);
        }
    }
}
