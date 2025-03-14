using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PaperShooting : MonoBehaviour

{
    public GameObject bullet;
    public Transform bulletPos;
    public float attackCooldown; // Time between attacks
    private bool canAttack; // Whether the player can attack
    public float attackDamage; // Amount of damage dealt to trash per attack
    [SerializeField] public float range;
    //private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canAttack = true;
    }

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
            shoot();
            //animator.SetTrigger("AttackTrigger");
            StartCoroutine(AttackCooldown());
        }
    }

    private void shoot()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
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