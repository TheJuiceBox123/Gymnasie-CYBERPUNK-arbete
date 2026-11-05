using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;
    public float jumpForce;

    Vector2 input;

    [Header("Ground Check")]
    public Transform groundCheckPoint;
    public float rayLength;
    public LayerMask groundLayer;
    private bool isGrounded;

    [Header("Knockback")]
    private bool isKnockedBack = false;
    public float knockbackDuration;

    private Inetractable nearbyInteractable;

    public void JumpInput(InputAction.CallbackContext context)
    {
        if (Time.timeScale == 0f) return;

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

        Flip();
    }

    void Flip()
    {
        if (input.x > 0)
        {
            transform.localScale = new Vector3(1f, transform.localScale.y, transform.localScale.z);
        }
        else if (input.x < 0)
        {
            transform.localScale = new Vector3(-1f, transform.localScale.y, transform.localScale.z);
        }
    }

    void FixedUpdate()
    {
        if (isKnockedBack) return;

        // Only touch horizontal velocity — leave vertical (gravity/jump) untouched
        rb.linearVelocity = new Vector2(input.x * moveSpeed, rb.linearVelocity.y);
    }

    public void SetKnockedBack(float duration)
    {
        StartCoroutine(KnockbackRoutine(duration));
    }

    System.Collections.IEnumerator KnockbackRoutine(float duration)
    {
        isKnockedBack = true;
        yield return new WaitForSeconds(duration);
        isKnockedBack = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Inetractable interactable = other.GetComponent<Inetractable>();
        if (interactable != null) nearbyInteractable = interactable;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<Inetractable>() == nearbyInteractable)
            nearbyInteractable = null;
    }

    public void InteractInput(InputAction.CallbackContext context)
    {
        if (Time.timeScale == 0f) return; // ignore while paused
        if (!context.performed) return;

        if (nearbyInteractable != null) nearbyInteractable.Interact();

    }
}