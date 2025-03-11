using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health;
    private float currentHeath;

    private void Start()
    {
        currentHeath = health;
    }

    public void takeDamage(float damage)
    {
        currentHeath -= damage;

        // place here animation for enemy

        if (currentHeath <= 0)
        {
            Die();
        }
    }

    void Die ()
    {
        Debug.Log("Enemy died!");

        // place here animation

        // disable or destroy the enemy

        this.enabled = false; // enemy health script
        GetComponent<EnemyAI>().enabled = false;
        GetComponent<EnemyAttack>().enabled = false;
        GetComponent<Collider2D>().enabled = false;

    }
}
