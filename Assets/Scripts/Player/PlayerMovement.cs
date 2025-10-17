using UnityEngine;
using UnityEngine.InputSystem; // new Input System

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;

    [Header("References")]
    public Joystick joystick; // optional mobile joystick
    private Rigidbody rb;
    private Animator animator;

    private Vector2 moveInput;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // --- Mobile joystick input ---
        float horizontal = joystick != null ? joystick.Horizontal : 0f;
        float vertical = joystick != null ? joystick.Vertical : 0f;

        // --- Keyboard input ---
        if (Keyboard.current != null)
        {
            if (Keyboard.current.wKey.isPressed) vertical += 1f;
            if (Keyboard.current.sKey.isPressed) vertical -= 1f;
            if (Keyboard.current.aKey.isPressed) horizontal -= 1f;
            if (Keyboard.current.dKey.isPressed) horizontal += 1f;
        }

        moveInput = new Vector2(horizontal, vertical);

        // Walking animation
        if (animator != null)
        {
            bool isMoving = moveInput.magnitude > 0.1f;
            animator.SetBool("isWalking", isMoving);
        }
    }

    private void FixedUpdate()
    {
        Vector3 moveDir = new Vector3(moveInput.x, 0, moveInput.y).normalized;
        rb.MovePosition(rb.position + moveDir * moveSpeed * Time.fixedDeltaTime);

        // Optional: small rotation toward movement
        if (moveDir != Vector3.zero)
        {
            Quaternion moveRot = Quaternion.LookRotation(moveDir);
            rb.rotation = Quaternion.Slerp(rb.rotation, moveRot, 0.1f); // small factor to avoid rotation conflict
        }
    }
}
