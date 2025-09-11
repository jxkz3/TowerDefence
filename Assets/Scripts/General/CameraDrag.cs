using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [Header("Drag Settings")]
    public float dragSpeed = 0.1f;
    private Vector3 lastMousePos;

    [Header("Zoom Settings")]
    public float zoomSpeed = 5f;
    public float minZoom = 5f;
    public float maxZoom = 20f;

    [Header("Boundaries")]
    public Vector2 minBounds = new Vector2(-10f, -10f);
    public Vector2 maxBounds = new Vector2(10f, 10f);

    private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        HandleDrag();
        HandleZoom();
        ClampPosition();
    }

    void HandleDrag()
    {
        // --- Mouse Drag (PC) ---
        if (Mouse.current != null)
        {
            if (Mouse.current.leftButton.isPressed)
            {
                Vector3 mouseDelta = Mouse.current.position.ReadValue() - (Vector2)lastMousePos;
                Vector3 move = new Vector3(-mouseDelta.x, 0, -mouseDelta.y) * dragSpeed * Time.deltaTime;
                transform.Translate(move, Space.World);
            }
            lastMousePos = Mouse.current.position.ReadValue();
        }

        // --- Touch Drag (Mobile) ---
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
        {
            Vector2 touchDelta = Touchscreen.current.primaryTouch.delta.ReadValue();
            Vector3 move = new Vector3(-touchDelta.x, 0, -touchDelta.y) * dragSpeed * Time.deltaTime;
            transform.Translate(move, Space.World);
        }
    }

    void HandleZoom()
    {
        // --- Mouse Scroll ---
        if (Mouse.current != null)
        {
            float scroll = Mouse.current.scroll.ReadValue().y;
            cam.orthographicSize -= scroll * zoomSpeed * Time.deltaTime;
        }

        // --- Pinch Zoom (Mobile) ---
        if (Touchscreen.current != null && Touchscreen.current.touches.Count >= 2)
        {
            var touch0 = Touchscreen.current.touches[0];
            var touch1 = Touchscreen.current.touches[1];

            if (touch0.isInProgress && touch1.isInProgress)
            {
                Vector2 prevPos0 = touch0.position.ReadValue() - touch0.delta.ReadValue();
                Vector2 prevPos1 = touch1.position.ReadValue() - touch1.delta.ReadValue();

                float prevDist = (prevPos0 - prevPos1).magnitude;
                float currDist = (touch0.position.ReadValue() - touch1.position.ReadValue()).magnitude;

                float delta = prevDist - currDist;
                cam.orthographicSize += delta * 0.01f; // Adjust sensitivity
            }
        }

        // Clamp zoom
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, minZoom, maxZoom);
    }

    void ClampPosition()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, minBounds.x, maxBounds.x);
        pos.z = Mathf.Clamp(pos.z, minBounds.y, maxBounds.y); // assuming top-down camera
        transform.position = pos;
    }
}
