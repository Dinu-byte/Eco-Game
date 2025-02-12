using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    public float attackCooldown = 0.0f; // Time between attacks
    private bool canAttack = true; // Whether the player can attack
    public float attackDamage = 10f; // Amount of damage dealt to trash per attack
    private Animator animator;

    private PlayerInput inputActions; // Reference to PlayerInput

    private void Awake()
    {
        inputActions = new PlayerInput(); // Initialize PlayerInput
        animator = GetComponent<Animator>(); // Get the Animator component
    }

    private void OnEnable()
    {
        // Subscribe to the "Attack" performed event (triggered when the "F" key is pressed)
        inputActions.Player.Attack.performed += OnAttackPerformed;
        inputActions.Enable(); // Enable the input actions
    }

    private void OnDisable()
    {
        // Unsubscribe to avoid memory leaks
        inputActions.Player.Attack.performed -= OnAttackPerformed;
        inputActions.Disable(); // Disable the input actions
    }

    // This method gets triggered when the "Attack" action (F key press) is performed
    private void OnAttackPerformed(InputAction.CallbackContext context)
    {
        Debug.Log("Attack Performed");
        if (canAttack)
        {
            animator.ResetTrigger("AttackTrigger");
            Attack();
            animator.SetTrigger("AttackTrigger");
            StartCoroutine(AttackCooldown());
        }
    }

    private void Attack()
    {

        // Check for nearby trash objects using a circle detection
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1f); // Check for trash in range
        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Trash"))
            {
                // Get the TrashHealth component and deal damage
                TrashHealth trashHealth = collider.GetComponent<TrashHealth>();
                if (trashHealth != null)
                {
                    trashHealth.TakeDamage(attackDamage);
                    Debug.Log("Health remaining: " + trashHealth.getHealth());// Deal damage to trash
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
        animator.ResetTrigger("AttackTrigger");
    }
}