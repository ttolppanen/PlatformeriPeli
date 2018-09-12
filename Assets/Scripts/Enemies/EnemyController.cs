using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public float acceleration;
    public float maxSpeed;
    public float followDistance;
    public float hittingDistance;
    public int damage;
    Transform player;
    Rigidbody2D rb;
    Animator anim;
    Vector2 spawnPoint;

	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        spawnPoint = transform.position;
        anim = GetComponent<Animator>();
	}
	
	
	void Update () {
        if (PauseMenu.instance.isPaused)
        {
            return;
        }

        Vector2 directionToPlayer = player.position - transform.position;
        if (directionToPlayer.magnitude > followDistance)
        {
            transform.position = spawnPoint;
            rb.velocity = Vector2.zero;
            return;
        }
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Hitting")) { //jos ei lyödä niin liikutaan...
            if (directionToPlayer.x >= 0)
            {
                rb.AddForce(acceleration * rb.mass * Time.deltaTime * Vector2.right);
                transform.localScale = Vector3.one;
            }
            else
            {
                rb.AddForce(acceleration * rb.mass * Time.deltaTime * Vector2.left);
                transform.localScale = new Vector3(-1, 1, 1);
            }
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

        //Hitting
        if (directionToPlayer.magnitude <= hittingDistance)
        {
            anim.SetBool("Hitting", true);
        }
        else
        {
            anim.SetBool("Hitting", false);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            collision.transform.GetComponent<PlayerHealth>().TakeDamage(damage, transform.position, false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerHealth>().TakeDamage(damage, transform.position, true);
        }
    }
}
