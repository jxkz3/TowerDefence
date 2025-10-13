using UnityEngine;
using UnityEngine.InputSystem;   // <- new Input System

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;

    [Header("References")]
    public Joystick joystick;   // Assign your UI joystick
    private Rigidbody rb;
    private Animator animator;  // NEW — animator reference

    private Vector2 moveInput;  // for Input System (PC)

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>(); // NEW — get Animator component
    }

    private void Update()
    {
        // Read joystick input (mobile)
        float horizontal = joystick != null ? joystick.Horizontal : 0f;
        float vertical = joystick != null ? joystick.Vertical : 0f;

        // Read Input System (PC testing)
        if (Keyboard.current != null)
        {
            if (Keyboard.current.wKey.isPressed) vertical += 1f;
            if (Keyboard.current.sKey.isPressed) vertical -= 1f;
            if (Keyboard.current.aKey.isPressed) horizontal -= 1f;
            if (Keyboard.current.dKey.isPressed) horizontal += 1f;
        }

        moveInput = new Vector2(horizontal, vertical);


        if (animator != null)
        {
            bool isMoving = moveInput.magnitude > 0.1f;
            animator.SetBool("isWalking", isMoving);
        }
    }

    private void FixedUpdate()
    {
        // Convert to world direction
        Vector3 direction = new Vector3(moveInput.x, 0, moveInput.y).normalized;

        // Move
        rb.MovePosition(transform.position + direction * moveSpeed * Time.fixedDeltaTime);

        // Rotate
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, 0.2f);
        }
    }
}
