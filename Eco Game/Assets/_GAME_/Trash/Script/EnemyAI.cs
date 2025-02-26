using UnityEngine;

public enum EnemyState { Idle, Alerted, Attacking }

public class EnemyAI : MonoBehaviour
{
    public EnemyState currentState = EnemyState.Idle;
    public float moveSpeed = 3f;
    public float attackRange = 1f;

    private FieldOfView fov;
    private Transform player;
    private Rigidbody2D rb;
    private bool playerInAttackRange;

    void Start()
    {
        fov = GetComponentInChildren<FieldOfView>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        switch (currentState)
        {
            case EnemyState.Idle:
                UpdateIdleState();
                break;

            case EnemyState.Alerted:
                UpdateAlertedState();
                break;

            case EnemyState.Attacking:
                UpdateAttackingState();
                break;
        }
    }

    void UpdateIdleState()
    {
        if (fov.PlayerVisible)
        {
            currentState = EnemyState.Alerted;
        }
    }

    void UpdateAlertedState()
    {
        // Movimento verso il giocatore
        Vector2 direction = ((Vector2)player.position - rb.position).normalized;
        rb.linearVelocity = direction * moveSpeed;

        // Rotazione verso il giocatore
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.MoveRotation(angle);

        // Transizioni
        if (!fov.PlayerVisible)
        {
            currentState = EnemyState.Idle;
            rb.linearVelocity = Vector2.zero;
        }
        else if (playerInAttackRange)
        {
            currentState = EnemyState.Attacking;
            rb.linearVelocity = Vector2.zero;
        }
    }

    void UpdateAttackingState()
    {
        // Logica di attacco qui

        if (!playerInAttackRange)
        {
            currentState = EnemyState.Alerted;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInAttackRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInAttackRange = false;
        }
    }
}