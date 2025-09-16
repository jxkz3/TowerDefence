using UnityEngine;

public class CastleHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Enemy reached castle!");

            // damage castle
            TakeDamage(10f);

            // kill enemy
            Destroy(other.gameObject);
        }
    }

    void TakeDamage(float dmg)
    {
        currentHealth -= dmg;
        Debug.Log("Castle Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Debug.Log("Castle Destroyed!");
            Destroy(gameObject);
        }
    }
}
