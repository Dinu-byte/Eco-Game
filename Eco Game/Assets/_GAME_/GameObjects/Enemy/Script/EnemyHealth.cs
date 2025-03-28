using System.Collections;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour
{
    private AudioManager audioManager;
    private EnemyManager enemyManager;

    public float health;
    public float immunityTime;
    private float currentHealth;
    private bool canBeHit;
    private float timerHit;
    public EnemyHitAnimate animate;

    public int coinsDropped;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float strength, coolDown;

    public UnityEvent OnBegin, OnDone;

    private void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        enemyManager = GameObject.FindGameObjectWithTag("EnemyCounter").GetComponent<EnemyManager>();
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
            audioManager.playSFX(audioManager.SFX_MONSTER_HIT_normal);


            if (animate != null)
            {
                animate.HitAnimation();
            }
            else
            {
                Debug.LogWarning("Animate is not assigned in " + gameObject.name);
            }

            if (currentHealth <= 0)
            {
                Die(player);
            }
        }
    }

    public void Die(GameObject player)
    {
        Debug.Log("Enemy died!");
        // place here animation
        // place here sound

        audioManager.playSFX(audioManager.SFX_MONSTER_death);


        if (this.name == "Paper")
        {
            player.GetComponent<PlayerHealth>().addPaperKills();
        }
        else if (this.name == "Plastic")
        {
            player.GetComponent<PlayerHealth>().addPlasticKills();
        }
        else
        {
            player.GetComponent<PlayerHealth>().addTrashKills();
        }

        player.GetComponent<PlayerHealth>().addTotalKills();
        player.GetComponent<PlayerHealth>().addCoins(coinsDropped);
        enemyManager.addEnemiesKilled();
        enemyManager.checkEnemiesKilled();

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

    public float getCurrentHealth ()
    {

        return currentHealth;
    }
}
