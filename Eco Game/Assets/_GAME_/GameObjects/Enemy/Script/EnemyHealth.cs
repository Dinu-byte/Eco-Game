using System.Collections;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour
{
    public float health;
    public float immunityTime;
    private float currentHealth;
    private bool canBeHit;
    private float timerHit;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float strength, coolDown;

    public UnityEvent OnBegin, OnDone;

    private void Start()
    {
        timerHit = 0;
        canBeHit = true;
        currentHealth = health;
    }

    private void Update()
    {
        if (!canBeHit) timerHit += Time.deltaTime;
        if (timerHit > immunityTime)
        {
            canBeHit = true;
            timerHit = 0;
        }
    }

    public void takeDamage(GameObject player, float damage)
    {
        if (canBeHit)
        {
            currentHealth -= damage;
            PlayFeedback(player);


            // place here animation for enemy

            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    public void Die ()
    {
        Debug.Log("Enemy died!");

        // place here animation

        
        Destroy(gameObject); // destroy the enemy

    }

    public void PlayFeedback(GameObject sender)
    {
        StopAllCoroutines();
        OnBegin?.Invoke();
        Vector2 direction = (transform.position - sender.transform.position).normalized;

        rb.AddForce(direction * strength, ForceMode2D.Impulse);
        StartCoroutine(Reset());

    }

    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(coolDown);
        rb.linearVelocity = Vector3.zero;
        OnDone?.Invoke();
    }
}
