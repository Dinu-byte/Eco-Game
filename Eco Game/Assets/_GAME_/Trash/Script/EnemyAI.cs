using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public enum State { Idle, Alerted, Attacking }
    public State currentState;

    public float sightRadius = 10f;  // Distanza di visibilità
    public float attackRadius = 2f;  // Distanza di attacco
    public float fieldOfViewAngle = 60f;  // Angolo di visibilità
    public Transform player;  // Riferimento al giocatore

    private Vector2 lastKnownPlayerPosition;  // Posizione del giocatore quando è stato visto

    private void Start()
    {
        currentState = State.Idle;
    }

    private void Update()
    {
        switch (currentState)
        {
            case State.Idle:
                CheckForPlayer();
                break;
            case State.Alerted:
                PursuePlayer();
                break;
            case State.Attacking:
                AttackPlayer();
                break;
        }
    }

    // Controlla se il giocatore è nel campo visivo
    void CheckForPlayer()
    {
        Vector2 directionToPlayer = player.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;

        // Se il giocatore è nel raggio di visibilità
        if (distanceToPlayer <= sightRadius && IsPlayerInSight(directionToPlayer))
        {
            lastKnownPlayerPosition = player.position;
            currentState = State.Alerted;
        }
    }

    // Insegui il giocatore se è all'interno del campo visivo
    void PursuePlayer()
    {
        Vector2 directionToPlayer = player.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;

        // Ruota il nemico verso il giocatore (solo in 2D)
        float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        // Insegui il giocatore se è dentro il raggio di visibilità
        if (distanceToPlayer <= sightRadius && IsPlayerInSight(directionToPlayer))
        {
            // Muovi verso il giocatore
            transform.position = Vector2.MoveTowards(transform.position, player.position, Time.deltaTime * 2f);

            // Se il giocatore è abbastanza vicino, passa allo stato Attacking
            if (distanceToPlayer <= attackRadius)
            {
                currentState = State.Attacking;
            }
        }
        else
        {
            // Se il giocatore non è più nel campo visivo, torna in Idle
            currentState = State.Idle;
        }
    }

    // Attacca il giocatore se è abbastanza vicino
    void AttackPlayer()
    {
        // Il nemico rimane fermo
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer > attackRadius)
        {
            currentState = State.Alerted;
        }
    }

    // Verifica se il giocatore è nel campo visivo
    bool IsPlayerInSight(Vector2 directionToPlayer)
    {
        // Calcola l'angolo tra la direzione del nemico e la direzione verso il giocatore
        float angle = Vector2.Angle(transform.right, directionToPlayer);

        // Verifica se il giocatore è nel campo visivo del nemico (60 gradi)
        return angle <= fieldOfViewAngle / 2f;
    }
}
