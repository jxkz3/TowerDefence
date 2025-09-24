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
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, target) < 0.1f)
                waypointIndex++;
        }
        else
        {
            Destroy(gameObject); // reached end
        }
    }
}
