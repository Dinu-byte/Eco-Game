using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    public float attackCooldown = 0.0f; // Time between attacks
    private bool canAttack = true; // Whether the player can attack
    public float attackDamage = 5f; // Amount of damage dealt to trash per attack
    [SerializeField] public float range = 2f;
    //private Animator animator;

    private void Awake()
    {
        //animator = GetComponent<Animator>(); // Get the Animator component
    }

    // This method gets triggered when the "Attack" action (F key press) is performed
    private void Update()
    {
        if (canAttack)
        {
            //animator.ResetTrigger("AttackTrigger");
            Attack();
            //animator.SetTrigger("AttackTrigger");
            StartCoroutine(AttackCooldown());
        }
    }

    private void Attack()
    {

        // Check for nearby trash objects using a circle detection
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, range); // Check for Player in range
        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                // Get the TrashHealth component and deal damage
                PlayerHealth playerHealth = collider.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(attackDamage);
                    Debug.Log("Health remaining Player: " + playerHealth.getHealth());// Deal damage to Player
                }
            }
        }

    }

    private IEnumerator AttackCooldown()
    {
        canAttack = false; // Disable further attacks during cooldown
        yield return new WaitForSeconds(attackCooldown); // Wait for the cooldown duration
        canAttack = true; // Re-enable attacks after cooldown

        // Reset the attack trigger after the cooldown
        //animator.ResetTrigger("AttackTrigger");
    }
}