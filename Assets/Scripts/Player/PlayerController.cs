using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    static public PlayerController instance;

    public float acceleration;
    public float maxSpeed;
    public float jumpForce;

    Friction fs; //Friction scripti...
    Rigidbody2D rb;
    Animator anim;
    float movementForce;
    float airForce;
    Vector2 movementVector; //Yleinen vektori mihin on laskettu kaikki liikkumiisen tarvittavat jutut.

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        fs = GetComponent<Friction>();
        movementForce = rb.mass * (acceleration + GameManager.instance.globalFriction);
        airForce = rb.mass * acceleration;
    }

    void Update ()
    {
        if (PauseMenu.instance.isPaused)
        {
            return;
        }

        movementVector = Vector2.zero;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (!Input.GetKey(KeyCode.C))
            {
                if (!fs.isGrounded)
                {
                    movementVector = Vector2.right * airForce;
                }
                else
                {
                    movementVector = Vector2.right * movementForce;
                }
            }
            if (transform.localScale.x == -1)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (!Input.GetKey(KeyCode.C))
            {
                if (!fs.isGrounded)
                {
                    movementVector = Vector2.left * airForce;
                }
                else
                {
                    movementVector = Vector2.left * movementForce;
                }
            }
            if (transform.localScale.x == 1)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }

        //Jumping
        if (Input.GetKeyDown(KeyCode.UpArrow) && fs.isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce * rb.mass, ForceMode2D.Impulse);
        }

        //Max speed....
        if (rb.velocity.x > maxSpeed)
        {
            rb.velocity = new Vector2(maxSpeed, rb.velocity.y);
        }
        else if (rb.velocity.x < -maxSpeed)
        {
            rb.velocity = new Vector2(-maxSpeed, rb.velocity.y);
        }

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
        {
            anim.SetBool("Running", true);
        }
        else
        {
            anim.SetBool("Running", false);
        }

        //Hitting
        if (Input.GetKeyDown(KeyCode.X) && !anim.GetCurrentAnimatorStateInfo(0).IsName("UkkoHit"))
        {  
            anim.SetTrigger("Hit");
        }

        //Shielding...
        if (Input.GetKey(KeyCode.C) && !anim.GetCurrentAnimatorStateInfo(0).IsName("UkkoHit"))
        {
            anim.SetBool("Shielding", true);
        }
        else
        {
            anim.SetBool("Shielding", false);
        }
	}

    private void FixedUpdate()
    {
        rb.AddForce(movementVector);
    }
}
