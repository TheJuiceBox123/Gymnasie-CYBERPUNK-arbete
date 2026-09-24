using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public float jumpForce;

    Vector2 input;

    [Header("Ground Check")]
    public Transform groundCheckPoint;
    public float rayLength = 0.1f;
    public LayerMask groundLayer;
    private bool isGrounded;

    public void JumpInput(InputAction.CallbackContext context)
    {
        if (context.canceled) return;
        if (!isGrounded) return;

        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        Debug.Log("Jumping");
    }

    public void MoveInput(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
    }

    void Update()
    {
        isGrounded = Physics2D.Raycast(groundCheckPoint.position, Vector2.down, rayLength, groundLayer);
        Debug.DrawRay(groundCheckPoint.position, Vector2.down * rayLength, isGrounded ? Color.green : Color.red);
    }

    void FixedUpdate()
    {
        // Only touch horizontal velocity — leave vertical (gravity/jump) untouched
        rb.linearVelocity = new Vector2(input.x * moveSpeed, rb.linearVelocity.y);
    }
}