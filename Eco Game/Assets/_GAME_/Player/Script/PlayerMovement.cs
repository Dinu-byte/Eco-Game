using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float maxSpeed = 5f;  // Max movement speed
    public float acceleration = 10f;  // How fast the player accelerates
    public float deceleration = 15f;  // How fast the player slows down
    private Vector2 movementInput;
    private Vector2 currentVelocity = Vector2.zero;
    private Rigidbody2D rb;
    private PlayerInput inputActions;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        inputActions = new PlayerInput();

        // Bind movement input
        inputActions.Player.Move.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += ctx => movementInput = Vector2.zero;
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void FixedUpdate()
    {
        // Target velocity based on input
        Vector2 targetVelocity = movementInput * maxSpeed;

        // Smoothly transition to the target velocity
        rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, targetVelocity, (movementInput.magnitude > 0 ? acceleration : deceleration) * Time.fixedDeltaTime);
    }

}
