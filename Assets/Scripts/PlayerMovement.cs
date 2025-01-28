using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer, wallLayer;
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float wallJumpCooldown;
    private bool canJump = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        //Grab references for rifigbody, animator e boxCollider from object "Player" uwu baka
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");


        //Flip character
        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        //Set animator parameters "run" in questo caso nella pagina animator
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded());

        //Controllo cooldown di WallJump
        if (wallJumpCooldown > 0.2f || isGrounded())
        {
            body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);

            if (Input.GetKey(KeyCode.Space) && canJump)
            {
                canJump = false;
                Jump();
            }
        }
        else
        {
            wallJumpCooldown += Time.deltaTime;
        }

        //Cambio gravita se onWall per cadere lento
        if (onWall() && !isGrounded())
        {
            body.gravityScale = 0.5f; // Gravità più bassa per scivolare lentamente
        }
        else
        {
            body.gravityScale = 2;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            canJump = true;
        }
        print(onWall());
    }

    private void Jump()
    {
        if (isGrounded())
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpPower);
            anim.SetTrigger("jump");
        }
        //Climbing on wall
        else if (onWall() && !isGrounded())
        {
            wallJumpCooldown = 0;
            body.linearVelocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 5, 10); // Salta in direzione opposta al muro
            anim.SetTrigger("jump");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }
}

//sex on the beach with snow bunnies