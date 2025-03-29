using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PaperShooting : MonoBehaviour

{
    private enum State { Idle, Alerted, Attacking }
    private State currentState;

    [SerializeField] private float sightRadius = 10f;
    [SerializeField] private float attackRadius = 2f;
    [SerializeField] private float alertedRadius = 2.4f;
    [SerializeField] private float fieldOfViewAngle = 60f;
    [SerializeField] private float maxSpeed = 3f;  // Max movement speed
    [SerializeField] private float acceleration = 5f;  // Acceleration speed
    [SerializeField] private float deceleration = 5f;  // Deceleration speed
    [SerializeField] private float shootCooldown = 2f;

    private float timer = 0f;

    private Transform player;
    public GameObject bullet;
    private Animator animator;

    private Vector2 lastKnownPlayerPosition;
    private Vector3 originalScale;
    private Rigidbody2D rb;
    private Vector2 currentVelocity;

    private void Start()
    {
        currentState = State.Idle;
        originalScale = transform.localScale;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();  // Get Rigidbody2D component
        animator = GetComponent<Animator>();
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

        if ((distanceToPlayer <= sightRadius && IsPlayerInSight(directionToPlayer)) || distanceToPlayer <= alertedRadius)
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

        // Controlla se il nemico si sta effettivamente muovendo
        if (rb.linearVelocity.magnitude > 0.1f)
        {
            animator.SetBool("IsMoving", true);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }



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
            animator.SetBool("IsMoving", false); // Assicurati di fermare l'animazione del movimento
        }
    }


    void AttackPlayer()
    {
        if (Vector2.Distance(transform.position, player.position) > attackRadius)
        {
            currentState = State.Alerted;
            timer = 0f;

        }
        else
        {
            timer += Time.deltaTime;
            if (timer >= shootCooldown)
            {
                shoot();
                timer = 0f;
            }
        }

    }

    bool IsPlayerInSight(Vector2 directionToPlayer)
    {
        float angle = Vector2.Angle(transform.right, directionToPlayer);
        return angle <= fieldOfViewAngle / 2f;
    }

    void shoot()
    {
        Instantiate(bullet, transform.position, Quaternion.identity);
    }
}