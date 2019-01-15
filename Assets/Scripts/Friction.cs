using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Friction : MonoBehaviour
{
    public bool isGrounded;
    Vector2 groundCheckBoxSize;
    Rigidbody2D rb;
    BoxCollider2D coll;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        groundCheckBoxSize = new Vector2(coll.size.x, 0.01f);
    }

    // Update is called once per frame
    private void Update()
    {
        isGrounded = CheckIfGrounded();
    }

    void FixedUpdate()
    {
        if (!isGrounded)
        {
            return;
        }

        int direction = 1;
        if (rb.velocity.x < 0)
        {
            direction = -1;
        }

        if (rb.velocity.x != 0)
        {
            if (Mathf.Abs(rb.velocity.x) - GameManager.instance.globalFriction * Time.fixedDeltaTime <= 0)
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
            else
            {
                rb.AddForce(new Vector2(-direction * GameManager.instance.globalFriction * rb.mass, 0));
            }
        }
    }

    bool CheckIfGrounded()
    {
        RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position + new Vector3(0, coll.offset.y - coll.size.y / 2f, 0), groundCheckBoxSize, 0, Vector2.down, 0.01f, LayerMask.GetMask("Ground")); // JOS HYPPY EI TOIMI KUN NIIN MATKA VARMAAN VAIHTUNU TÄSSÄ KUVAN VAIHDON YHTEYDESSÄ
        if (hits.Length == 0)
        {
            return false;
        }
        return true;
    }
}
