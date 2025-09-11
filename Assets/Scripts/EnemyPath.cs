using UnityEngine;

public class EnemyPath : MonoBehaviour
{
    public Transform[] waypoints;   // Assign in Inspector
    public float speed = 2f;

    private int waypointIndex = 0;

    void Update()
    {
        if (waypointIndex < waypoints.Length)
        {
            // Move towards current waypoint
            Vector3 target = waypoints[waypointIndex].position;
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

            // Check if reached waypoint
            if (Vector3.Distance(transform.position, target) < 0.1f)
            {
                waypointIndex++;
            }
        }
        /* else
        {
            // Enemy reached the base
            BaseHealth.instance.TakeDamage(1); // Example: damage base by 1
            Destroy(gameObject); // remove enemy
        } */
    }
}
