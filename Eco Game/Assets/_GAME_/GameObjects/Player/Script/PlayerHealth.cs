using System.Collections;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] public float health;  // Default health for each trash piece

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float strength, coolDown;
    [SerializeField] private float knockbackThreshold; /* this is the value of how much damage an enemy has to do to perform a big knockback on the player.
                                                        *  It is relative to the health of the player so it's in percentage (%) and IT MUST BE FROM 0 TO 1. */
    [SerializeField] private float immunityTime;
    private float timer;
    private bool canBeHit;

    [SerializeField] private float knockbackMultiplier;
    public UnityEvent OnBegin, OnDone;

    private void Start()
    {
        timer = 0;
        canBeHit = true;
    }
    private void Update()
    {
        if (!canBeHit) timer += Time.deltaTime;
        if (timer > immunityTime)
        {
            canBeHit = true;
            timer = 0;
        }
    }
    public void TakeDamage(GameObject enemy, float damage)
    {
        if (canBeHit)
        {
            Debug.Log("Player health:" + health);
            health -= damage;  // Reduce health by damage amount
            PlayFeedback(enemy, damage);

            canBeHit = false;

            if (health <= 0)
            {
                Die();  // Destroy the trash when health reaches 0
            }
        }
        
    }

    public float getHealth()
    {
        return health;
    }

    public void Die()
    {
        Debug.Log("Player died!");

        // place here animation


        Destroy(gameObject); // destroy the player

    }

    public void PlayFeedback(GameObject sender, float damage)
    {
        StopAllCoroutines();
        OnBegin?.Invoke();
        Vector2 direction = (transform.position - sender.transform.position).normalized;
        if (damage / health >= knockbackThreshold)
        {
            rb.AddForce(direction * strength * knockbackMultiplier, ForceMode2D.Impulse);
        }
        else
        {
            rb.AddForce(direction * strength, ForceMode2D.Impulse);
        }
        StartCoroutine(Reset());

    }

    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(coolDown);
        rb.linearVelocity = Vector3.zero;
        OnDone?.Invoke();
    }
}
