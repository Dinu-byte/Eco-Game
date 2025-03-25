using JetBrains.Annotations;
using System;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 mousePos;
    public Transform attackTransform;

    public LayerMask enemyLayer;
    private GameObject player;

    private float timerAttack; // main attack variables
    private bool canAttack;
    public float attackRange;
    public float coolDownAttack;
    private float attackDamage;

    private float timerBoomerang; // boomerang variables
    private bool canBoomerang;
    private bool boomerangReturned;
    public float coolDownBoomerang;
    public GameObject boomerang;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        player = GameObject.FindGameObjectWithTag("Player");
        canAttack = true;
        canBoomerang = true;
        boomerangReturned = true;
        attackDamage = player.GetComponent<PlayerHealth>().damage;
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition); // trovare la posizione del mouse nel gioco (x,y,z).

        Vector3 rotation = mousePos - transform.position; // sottraiamo la posizione dell'oggetto.

        float rotZ = Mathf.Atan2(rotation.y,rotation.x) * Mathf.Rad2Deg; // gradi di rotazione nella z.

        transform.rotation = Quaternion.Euler(0,0,rotZ);

        if (!canAttack)
        {
            timerAttack += Time.deltaTime;
            if (timerAttack >= coolDownAttack) canAttack = true;
        }
        if (Input.GetMouseButtonDown(0) && canAttack)
        {
            canAttack = false;
            attack();

        }

        if (boomerangReturned) // boomerangReturned will be set to true by the boomerang object.
        {
            if (canBoomerang)
            {
                if (Input.GetMouseButton(1))
                {
                    launchBoomerang();
                    canBoomerang = false;
                    boomerangReturned = false;
                }
            }
            else
            {
                timerBoomerang += Time.deltaTime;
                if (timerBoomerang >= coolDownBoomerang)
                {
                    canBoomerang = true;
                    timerBoomerang = 0;
                }
            }
        }
    }

    void attack ()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackTransform.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                Debug.Log("Enemy hit");
                enemy.GetComponent<EnemyHealth>().takeDamage(player, attackDamage);
            }
        }
    }

    void launchBoomerang ()
    {
        Instantiate(boomerang, player.transform.position, Quaternion.identity);
    }

    public void setBoomerangReturned (bool b)
    {
        boomerangReturned = b;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackTransform == null) return;
        
        Gizmos.DrawWireSphere(attackTransform.position, attackRange);
    }

    public void addDamage ()
    {
        attackDamage += 5;
    }

    public void decreaseCoolDown()
    {
        coolDownAttack -= 0.1f;
        coolDownBoomerang -= 0.05f;

    }

}
