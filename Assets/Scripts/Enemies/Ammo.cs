using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour {

    public int damage;
    public float ammoWidth;
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Player")
        {
            if (coll.GetComponent<Animator>().GetBool("Shielding"))
            {
                Vector2 playerToAmmo = transform.position - coll.transform.position;
                if (Mathf.Abs(playerToAmmo.y) < 0.4f + ammoWidth && playerToAmmo.x / Mathf.Abs(playerToAmmo.x) == coll.transform.localScale.x) // On samalla puolella
                {
                    rb.velocity = new Vector2(-rb.velocity.x / 4, Mathf.Abs(rb.velocity.y)); //mmmmmmmm 
                    return;
                }
            }
            //Take damage
            coll.GetComponent<PlayerHealth>().TakeDamage(damage, transform.position, false);
        }
    }
}
