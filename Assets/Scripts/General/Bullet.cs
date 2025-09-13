using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float damage = 10f;   // tower will assign this value
    public float lifeTime = 3f;

    void Start()
    {
        Destroy(gameObject, lifeTime); // destroy bullet after X seconds
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        // Example: If the enemy has "EnemyHealth" script
      /*  EnemyHealth enemy = other.GetComponent<EnemyHealth>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject); // destroy bullet on hit
        }
      */
    }
}
