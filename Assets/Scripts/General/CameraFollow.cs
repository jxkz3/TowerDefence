using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;        // Assign Player here
    public Vector3 offset = new Vector3(0, 10, -10); // adjust for top-down
    public float smoothSpeed = 5f;

    void LateUpdate()
    {
        if (target == null) return;

        // Desired position = player + offset
        Vector3 desiredPosition = target.position + offset;

        // Smooth follow
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        transform.position = smoothedPosition;

        // Optional: always look at the player
        // transform.LookAt(target);
    }
}
