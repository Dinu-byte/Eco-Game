using System.Collections;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour
{
    public AudioManager audioManager;
    private Animator animator;

    public float health;
    public float immunityTime;
    private float currentHealth;
    private bool canBeHit;
    private float timerHit;

    public int coinsDropped;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float strength, coolDown;

    public UnityEvent OnBegin, OnDone;

    private void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        animator = GetComponentInChildren<Animator>();
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


            // place here animation for enemy
            animator.SetTrigger("EnemyHit");

            if (currentHealth <= 0)
            {
                Die(player);
                animator.SetTrigger("EnemyDie");
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
