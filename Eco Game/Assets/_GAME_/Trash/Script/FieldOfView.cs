using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float viewRadius = 5f;
    [Range(0, 360)] public float viewAngle = 60f;
    public LayerMask targetMask;
    public LayerMask obstacleMask;

    public bool PlayerVisible { get; private set; }
    public Vector2 LastKnownPosition { get; private set; }

    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        UpdateFieldOfView();
    }

    void UpdateFieldOfView()
    {
        Vector2 directionToPlayer = (player.position - transform.position).normalized;
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        PlayerVisible = false;

        if (distanceToPlayer <= viewRadius)
        {
            float angleToPlayer = Vector2.Angle(transform.right, directionToPlayer);

            if (angleToPlayer < viewAngle / 2)
            {
                RaycastHit2D hit = Physics2D.Raycast(
                    transform.position,
                    directionToPlayer,
                    viewRadius,
                    obstacleMask
                );

                if (!hit || hit.collider.CompareTag("Player"))
                {
                    PlayerVisible = true;
                    LastKnownPosition = player.position;
                }
            }
        }
    }

    public Vector2 DirFromAngle(float angleInDegrees)
    {
        return new Vector2(
            Mathf.Cos(angleInDegrees * Mathf.Deg2Rad),
            Mathf.Sin(angleInDegrees * Mathf.Deg2Rad)
        );
    }
}