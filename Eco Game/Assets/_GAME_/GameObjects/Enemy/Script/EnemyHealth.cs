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

    public void Die ()
    {
        Debug.Log("Enemy died!");

        // place here animation

        // disable or destroy the enemy
        
        Destroy(gameObject);

    }
}
