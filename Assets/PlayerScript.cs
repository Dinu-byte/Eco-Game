using System;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerScript : MonoBehaviour
{
    public Rigidbody2D playerRigidbody;
    public float acceleration;
    public float maxVel;
    private float velocity = 0;
    private int dir = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        // MOVIMENTO ORIZZONTALE:

        dir = Convert.ToInt32((Input.GetKey(KeyCode.A))) - Convert.ToInt32((Input.GetKey(KeyCode.D)));

        if (dir == 1)
        {
            velocity += acceleration;
            if (velocity > maxVel) velocity = maxVel;
            transform.position = transform.position + (Vector3.left * velocity) * Time.deltaTime;
        }
        else if (dir == -1)
        {
            velocity += acceleration;
            if (velocity > maxVel) velocity = maxVel;
            transform.position = transform.position + (Vector3.right * velocity) * Time.deltaTime;
        }
        else
        {
            velocity = 0;
        }

    }
}
