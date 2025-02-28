using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public enum State { Idle, Alerted, Attacking }
    public State currentState;

    [SerializeField] private float sightRadius = 10f;
    [SerializeField] private float attackRadius = 2f;
    [SerializeField] private float fieldOfViewAngle = 60f;
    [SerializeField] private float maxSpeed = 3f;  // Max movement speed
    [SerializeField] private float acceleration = 5f;  // Acceleration speed
    [SerializeField] private float deceleration = 5f;  // Deceleration speed

    public Transform player;

    private Vector2 lastKnownPlayerPosition;
    private Vector3 originalScale;
    private Rigidbody2D rb;
    private Vector2 currentVelocity;

    private void Start()
    {
        currentState = State.Idle;
        originalScale = transform.localScale;
        rb = GetComponent<Rigidbody2D>();  // Get Rigidbody2D component
    }

    private void Update()
    {
        switch (currentState)
        {
            case State.Idle:
                CheckForPlayer();
                break;
            case State.Alerted:
                break;  // Movement is now handled in FixedUpdate()
            case State.Attacking:
                AttackPlayer();
                break;
        }
    }

    private void FixedUpdate()
    {
        if (currentState == State.Alerted)
        {
            MoveTowardsPlayer();
        }
    }

    void CheckForPlayer()
    {
        Vector2 directionToPlayer = player.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;

        if (distanceToPlayer <= sightRadius && IsPlayerInSight(directionToPlayer))
        {
            lastKnownPlayerPosition = player.position;
            currentState = State.Alerted;
        }
    }

    void MoveTowardsPlayer()
    {
        Vector2 directionToPlayer = (player.position - transform.position).normalized;
        Vector2 targetVelocity = directionToPlayer * maxSpeed;

        // Smooth acceleration & deceleration
        rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, targetVelocity, (directionToPlayer.magnitude > 0 ? acceleration : deceleration) * Time.fixedDeltaTime);

        // Adjust sprite direction
        if (Mathf.Abs(directionToPlayer.x) > Mathf.Abs(directionToPlayer.y))
        {
            transform.localScale = new Vector3(originalScale.x * (directionToPlayer.x > 0 ? 1 : -1), originalScale.y, originalScale.z);
        }

        // Switch to attacking if close enough
        if (Vector2.Distance(transform.position, player.position) <= attackRadius)
        {
            currentState = State.Attacking;
            rb.linearVelocity = Vector2.zero;  // Stop moving when attacking
        }
    }

    void AttackPlayer()
    {
        if (Vector2.Distance(transform.position, player.position) > attackRadius)
        {
            currentState = State.Alerted;

        }
    }

    bool IsPlayerInSight(Vector2 directionToPlayer)
    {
        float angle = Vector2.Angle(transform.right, directionToPlayer);
        return angle <= fieldOfViewAngle / 2f;
    }
}
