using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public float damage = 10f;
    public float lifetime = 3f;

    private Vector3 targetPosition;
    private GameObject targetEnemy;

    void Start()
    {
        Destroy(gameObject, lifetime); // safety cleanup
    }

    public void SetTarget(Vector3 targetPos, GameObject enemy)
    {
        targetPosition = targetPos;
        targetEnemy = enemy;
    }

    void Update()
    {
        // Destroy if target is gone
        if (targetEnemy == null)
        {
            Destroy(gameObject);
            return;
        }

        // Update target position (enemy could move)
        Collider enemyCollider = targetEnemy.GetComponent<Collider>();
        targetPosition = enemyCollider != null ? enemyCollider.bounds.center : targetEnemy.transform.position;

        // Move toward target
        Vector3 direction = (targetPosition - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        // Rotate bullet to face movement
        if (direction != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(direction);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyHealth enemy = other.GetComponent<EnemyHealth>();
            if (enemy != null)
                enemy.TakeDamage(damage);

            Destroy(gameObject); // disappear immediately
        }
    }
}
