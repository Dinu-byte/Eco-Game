using System.Collections;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private TMP_Text coinCount;

    private AudioManager audioManager;

    [SerializeField] public float health;  // Default health for each trash piece

    private int totalKills; // statistics
    private int paperKills;
    private int plasticKills;
    private int trashKills;
    public int coins;

    public float damage; // player skills
    public float boomerangDamage;
    public float boomerangSpeed;

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
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        timerHit = 0;
        timerHeal = 0;
        canBeHit = true;
        canHeal = true;
        currentHealth = health;
        healthBarScript.setMaxHealth(health);

        totalKills = 0;
        paperKills = 0;
        plasticKills = 0;
        trashKills = 0;
        coinCount.text = "Coins: " + coins.ToString();
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
            Debug.Log("Player kills:" + totalKills);
            PlayFeedback(enemy, damage);

            canBeHit = false;

            if (currentHealth <= 0)
            {
                Die();  // Destroy the player when health reaches 0
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
        audioManager.playSFX(audioManager.SFX_HEAL);

        Debug.Log("Player healed! Player health: " + currentHealth);
    }

    public void Die()
    {
        Debug.Log("Player died!");

        // place here animation

        // load game over screen and music/sound effects.
        audioManager.playSFX(audioManager.SFX_UI_gameOver);

        gameOverCanvas.SetActive(true);

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
            audioManager.playSFX(audioManager.SFX_PLAYER_HIT_hard);
        }
        else
        {
            rb.AddForce(direction * strength, ForceMode2D.Impulse);
            audioManager.playSFX(audioManager.SFX_PLAYER_HIT_normal);
        }
        StartCoroutine(Reset());

    }

    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(coolDownKnockback);
        rb.linearVelocity = Vector3.zero;
        OnDone?.Invoke();
    }

    public void addTotalKills() // methods for stats and money
    {
        totalKills++;
    }

    public void addPaperKills()
    {
        paperKills++;
    }

    public void addPlasticKills()
    {
        plasticKills++;
    }

    public void addTrashKills()
    {
        trashKills++;
    }

    public void addCoins (int coinsDropped)
    {
        coins += coinsDropped;
        coinCount.text = "Coins: " + coins.ToString();
    }

    public void addBoomerangDamage ()
    {
        boomerangDamage += 5;

    }

    public void addMaxHealth ()
    {
        health += 25;
        currentHealth = health;
        healthBarScript.setMaxHealth(health);
        healthBarScript.setHealth(currentHealth);
        healValue += 15;

    }

    public void decreaseCoolDown ()
    {
        coolDownHeal -= 0.5f;
    }

    public void addBoomerangSpeed()
    {
        boomerangSpeed += 0.75f;
    }

    public void addImmunity ()
    {
        immunityTime += 0.1f;
    }
}
