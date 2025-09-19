using UnityEngine;

public class EnemyPath : MonoBehaviour
{
    public Transform[] waypoints;   // Assigned by spawner
    public float speed = 2f;
    private int waypointIndex = 0;

    [Header("Attack Settings")]
    public float damage = 1f;
    public float attackRate = 1f; // attacks per second
    private float attackCooldown = 0f;

    private Wall targetWall; // wall being attacked

    void Update()
    {
        if (targetWall != null)
        {
            // Stop moving and attack the wall
            attackCooldown -= Time.deltaTime;

            if (attackCooldown <= 0f)
            {
                targetWall.TakeDamage(damage);
                attackCooldown = 1f / attackRate;
            }
            return; // skip movement while attacking
        }

        // --- Normal waypoint movement ---
        if (waypoints == null || waypoints.Length == 0) return;

        if (waypointIndex < waypoints.Length)
        {
            Vector3 target = waypoints[waypointIndex].position;
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, target) < 0.1f)
            {
                waypointIndex++;
            }
        }
        else
        {
            // Enemy reached end of path (base)
            // BaseHealth.instance.TakeDamage(1); // if you add a base system
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Wall wall = collision.gameObject.GetComponent<Wall>();
        if (wall != null)
        {
            targetWall = wall; // lock onto wall
        }
    }

    void OnCollisionExit(Collision collision)
    {
        Wall wall = collision.gameObject.GetComponent<Wall>();
        if (wall == targetWall)
        {
            targetWall = null; // stop attacking, keep moving
        }
    }
}
