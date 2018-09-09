using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float acceleration;
    public float maxSpeed;
    public float jumpForce;
    public Vector2 groundCheckBoxSize;
    public float groundCheckDistance;

    Rigidbody2D rb;
    Animator anim;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update ()
    {
        if (PauseMenu.instance.isPaused)
        {
            return;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (!Input.GetKey(KeyCode.C))
            {
                rb.AddForce(Vector2.right * acceleration * Time.deltaTime * rb.mass);
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
                rb.AddForce(Vector2.left * acceleration * Time.deltaTime * rb.mass);
            }
            if (transform.localScale.x == 1)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }

        //Jumping
        if (Input.GetKeyDown(KeyCode.UpArrow) && CheckIfGrounded())
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

    bool CheckIfGrounded()
    {
        RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position + new Vector3(0, -1.095f, 0), groundCheckBoxSize, 0, Vector2.down, groundCheckDistance); // JOS HYPPY EI TOIMI KUN NIIN MATKA VARMAAN VAIHTUNU TÄSSÄ KUVAN VAIHDON YHTEYDESSÄ
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.transform.tag == "Ground")
            {
                return true;
            }
        }
        return false;
    }
}
