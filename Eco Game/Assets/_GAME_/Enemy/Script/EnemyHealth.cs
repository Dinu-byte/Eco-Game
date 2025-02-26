using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] public float health = 100f;  // Default health for each trash piece

    public void TakeDamage(float damage)
    {
        health -= damage;  // Reduce health by damage amount
        if (health <= 0)
        {
            Destroy(gameObject);  // Destroy the trash when health reaches 0
        }
    }

    public float getHealth()
    {
        return health;
    }
}
