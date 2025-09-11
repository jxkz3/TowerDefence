using UnityEngine;

public class EnemyPath : MonoBehaviour
{
    public Transform[] waypoints;   // Assigned by spawner
    public float speed = 2f;

    private int waypointIndex = 0;

    void Update()
    {
        // Prevent errors if waypoints are not set yet
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
            // BaseHealth.instance.TakeDamage(1); // Uncomment if you add base health system
            Destroy(gameObject);
        }
    }
}
