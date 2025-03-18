using Unity.VisualScripting;
using UnityEngine;

public class BoomerangScript : MonoBehaviour
{
    public GameObject player; // the player
    private Camera mainCam; // main camera
    private Vector3 mousePos; // cursor cordinates
    private Rigidbody2D rb;
    private float timer; // timer to count when it should return to the player
    private bool returning; // boolean for when it's returning
    public float force; // the force (basically the speed) applied to the boomerang.
    public float damage; // damage dealt to enemies.
    public float timeBeforeReturn; // time before it returns to the player
    public float distanceDestroyed; // developer things, too complicated to explain (it solves a problem so it is usefull, DO NOT DELETE).

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // SETTING ALL THE VARIABLES AND OBJECTS:

        player = GameObject.FindGameObjectWithTag("Player");
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        rb = GetComponent<Rigidbody2D>();
        returning = false;
        timer = 0;

        Vector3 direction = mousePos - transform.position;
        rb.linearVelocity = new Vector2(direction.x, direction.y).normalized * force;
    }

    // Update is called once per frame
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
            if (Mathf.Abs(player.transform.position.x - transform.position.x) < distanceDestroyed && player.transform.position.y - transform.position.y < distanceDestroyed)
            {
                destroyAndSetTrue();
            }
        }
    }

    void moveTowardsPlayer()
    {
        rb.linearVelocity = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y).normalized * force;
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

    private void destroyAndSetTrue ()
    {
        GameObject.FindGameObjectWithTag("RotatePoint").GetComponent<Shooting>().setBoomerangReturned(true);
        Destroy(gameObject);
    }

}
