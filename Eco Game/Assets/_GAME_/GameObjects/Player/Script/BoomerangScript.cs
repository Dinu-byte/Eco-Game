using UnityEngine;

public class BoomerangScript : MonoBehaviour
{
    public GameObject player; // The player
    private Camera mainCam; // Main camera
    private Vector3 mousePos; // Cursor coordinates
    private Rigidbody2D rb;
    private float timer; // Timer to count when it should return to the player
    private float timeAlive;
    private bool returning; // Boolean for when it's returning
    private float damage; // Damage dealt to enemies

    public float speed = 1f; // Adjust this in Inspector to control speed
    public float timeBeforeReturn; // Time before it returns to the player
    public float timeBeforeDestroyed = 2.6f;
    public float distanceDestroyed; // Used to check when to destroy
    private float force; // The force (speed) applied to the boomerang

    void Start()
    {
        // Setting all variables and objects
        player = GameObject.FindGameObjectWithTag("Player");
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        rb = GetComponent<Rigidbody2D>();
        returning = false;
        timer = 0;
        timeAlive = 0;
        damage = player.GetComponent<PlayerHealth>().boomerangDamage;
        force = player.GetComponent<PlayerHealth>().boomerangSpeed;

        Vector3 direction = mousePos - transform.position;
        rb.linearVelocity = new Vector2(direction.x, direction.y).normalized * force * speed;

        GetComponent<Animator>().enabled = true;
        GetComponent<Animator>().Play("BoomerangThrow");
    }

    void Update()
    {
        if (timer >= timeBeforeReturn)
        {
            returning = true;
        }
        else
        {
            timer += Time.deltaTime;
        }

        if (returning)
        {
            moveTowardsPlayer();
            timeAlive += Time.deltaTime;
            if (Vector2.Distance(transform.position, player.transform.position) <= distanceDestroyed || timeAlive >= timeBeforeDestroyed)
            {
                destroyAndSetTrue();
            }
        }
    }

    void moveTowardsPlayer()
    {
        rb.linearVelocity = (player.transform.position - transform.position).normalized * force * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyHealth>().takeDamage(player, damage);
            returning = true;
        }
        else if (other.gameObject.CompareTag("HitBox"))
        {
            returning = true;
        }
        else if (other.gameObject.CompareTag("Player") && returning)
        {
            destroyAndSetTrue();
        }
    }

    private void destroyAndSetTrue()
    {
        GameObject.FindGameObjectWithTag("RotatePoint").GetComponent<Shooting>().setBoomerangReturned(true);
        timeAlive = 0;
        Destroy(gameObject);
    }
}
