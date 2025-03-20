using System.Collections;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] public float health;  // Default health for each trash piece

    public HealthBarScript healthBarScript;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float strength, coolDownKnockback;
    [SerializeField] private float knockbackThreshold; /* this is the value of how much damage an enemy has to do to perform a big knockback on the player.
                                                        *  It is relative to the health of the player so it's in percentage (%) and IT MUST BE FROM 0 TO 1. */
    [SerializeField] private float immunityTime;
    [SerializeField] private float coolDownHeal;
    [SerializeField] private float healValue;
    private float currentHealth;
    private float timerHit;
    private float timerHeal;
    private bool canBeHit;
    private bool canHeal;

    [SerializeField] private float knockbackMultiplier;
    public UnityEvent OnBegin, OnDone;

    private void Start()
    {
        timerHit = 0;
        timerHeal = 0;
        canBeHit = true;
        canHeal = true;
        currentHealth = health;
        healthBarScript.setMaxHealth(health);
    }
    private void Update()
    {
        if (!canBeHit) timerHit += Time.deltaTime;
        if (timerHit > immunityTime)
        {
            canBeHit = true;
            timerHit = 0;
        }

        if (canHeal && Input.GetKey(KeyCode.Space) && currentHealth < health)
        {
            heal();
        }
        else
        {
            timerHeal += Time.deltaTime;
            if (timerHeal >= coolDownHeal)
            {
                canHeal = true;
                timerHeal = 0;
            }
        }
    }
    public void TakeDamage(GameObject enemy, float damage)
    {
        if (canBeHit)
        {
            currentHealth -= damage;  // Reduce health by damage amount
            healthBarScript.setHealth(currentHealth);
            Debug.Log("Player hit! Player health: " + currentHealth);
            PlayFeedback(enemy, damage);

            canBeHit = false;

            if (currentHealth <= 0)
            {
                Die();  // Destroy the trash when health reaches 0
            }
        }
        
    }

    public float getHealth()
    {
        return currentHealth;
    }

    private void heal ()
    {
        currentHealth += healValue;
        if (currentHealth > health)
        {
            currentHealth = health;
        }
        healthBarScript.setHealth(currentHealth);
        canHeal = false;

        Debug.Log("Player healed! Player health: " + currentHealth);
    }

    public void Die()
    {
        Debug.Log("Player died!");

        // place here animation

        // load game over screen and music/sound effects.

        gameObject.SetActive(false); // disable the player

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
        yield return new WaitForSeconds(coolDownKnockback);
        rb.linearVelocity = Vector3.zero;
        OnDone?.Invoke();
    }
}
