using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Vision Settings")]
    public float fovAngle = 45f; // Angolo del campo visivo
    public float fovDistance = 5f; // Distanza massima del campo visivo
    public Vector2 eyePositionOffset = Vector2.zero; // Offset per la posizione degli "occhi"
    public LayerMask obstructionMask; // Layer degli ostacoli

    [Header("Proximity Settings")]
    public float proximityDistance = 2f; // Distanza per l'allarme immediato

    [Header("Chase Settings")]
    public float chaseExitDistance = 10f; // Distanza per perdere il giocatore
    public float chaseSpeed = 3f; // Velocità di inseguimento

    private bool isAlerted = false;
    private Transform player;
    private Rigidbody2D rb;
    private Vector2 facingDirection = Vector2.down;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        UpdateFacingDirection();

        if (!isAlerted)
        {
            CheckForPlayer();
        }
        else
        {
            ChasePlayer();
        }
    }

    void UpdateFacingDirection()
    {
        // Aggiorna la direzione in base al movimento
        if (rb.linearVelocity != Vector2.zero)
        {
            facingDirection = rb.linearVelocity.normalized;
        }
    }

    void CheckForPlayer()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Controllo prossimità
        if (distanceToPlayer <= proximityDistance)
        {
            isAlerted = true;
            Debug.Log("Player detected by proximity!");
            return;
        }

        // Controllo campo visivo
        Vector2 directionToPlayer = (player.position - transform.position).normalized;
        float angleToPlayer = Vector2.Angle(facingDirection, directionToPlayer);

        Debug.Log($"Angle: {angleToPlayer} - Distance: {distanceToPlayer}");

        if (angleToPlayer <= fovAngle / 2 && distanceToPlayer <= fovDistance)
        {
            Vector2 eyePosition = (Vector2)transform.position + eyePositionOffset;
            RaycastHit2D hit = Physics2D.Raycast(
                eyePosition,
                directionToPlayer,
                fovDistance,
                obstructionMask
            );

            Debug.DrawRay(eyePosition, directionToPlayer * fovDistance, Color.red, 0.1f);

            if (hit.collider != null  && hit.collider.CompareTag("Player"))
            {
                isAlerted = true;
                Debug.Log("Player detected by vision!");
            }
        }
    }

    void ChasePlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.linearVelocity = direction * chaseSpeed;

        // Controllo distanza di fuga
        float currentDistance = Vector2.Distance(transform.position, player.position);
        if (currentDistance > chaseExitDistance)
        {
            isAlerted = false;
            rb.linearVelocity = Vector2.zero;
            Debug.Log("Player lost!");
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Disegna il campo visivo
        Vector2 eyePosition = (Vector2)transform.position + eyePositionOffset;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(eyePosition, proximityDistance);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(eyePosition, fovDistance);

        // Disegna il cono visivo
        Vector2 forwardLine = Quaternion.Euler(0, 0, fovAngle / 2) * facingDirection * fovDistance;
        Vector2 backwardLine = Quaternion.Euler(0, 0, -fovAngle / 2) * facingDirection * fovDistance;

        Gizmos.color = Color.green;
        Gizmos.DrawRay(eyePosition, forwardLine);
        Gizmos.DrawRay(eyePosition, backwardLine);
        Gizmos.DrawLine(eyePosition + forwardLine, eyePosition + backwardLine);
    }
}