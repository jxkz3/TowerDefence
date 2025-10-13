using UnityEngine;

public class EnemyPath : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 2f;
    private int waypointIndex = 0;

    [HideInInspector] public bool canMove = true; // controlled by attack script

    void Update()
    {
        if (!canMove) return; // stop moving while attacking

        if (waypoints == null || waypoints.Length == 0) return;

        if (waypointIndex < waypoints.Length)
        {
            Vector3 target = waypoints[waypointIndex].position;

            // Move toward the target
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

            // Rotate enemy toward target
            Vector3 direction = target - transform.position;
            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
            }

            // Check if we reached the waypoint
            if (transform.position == target)
                waypointIndex++;
        }
        else
        {
            Destroy(gameObject); // reached end
        }
    }
}
