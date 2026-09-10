using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Rigidbody2D rb;

    public float jumpForce;
    
    //InputAction jumpAction;
    //InputAction moveAction;
    Vector2 input;

    void Start()
    {
        //moveAction = InputSystem.actions.FindAction("Move");
    }

    //private void FixedUpdate()
    //{
    //    input = moveAction.ReadValue<Vector2>();
    //}

    public void JumpInput(InputAction.CallbackContext context)
    {
        if (context.canceled) return;

        rb.AddForce(Vector2.up * jumpForce * Time.fixedDeltaTime, ForceMode2D.Impulse);   
        Debug.Log("Jumping");
        // TODO: JUMP
    }

    public void MoveInput(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
        Debug.Log($"Move input: {input}");
    }

    void Update()
    {
        transform.Translate(input * moveSpeed * Time.deltaTime);
    }
}
