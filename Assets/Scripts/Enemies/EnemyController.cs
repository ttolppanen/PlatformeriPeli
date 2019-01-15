using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public float acceleration;
    public float maxSpeed;
    public float followDistance;
    public float hittingDistance;
    public Collider2D hitBox;
    public int damage;
    float movementForce;
    Vector2 movementVector; //Yleinen voimaaaaa kato playercontrolleri.
    Transform player;
    Rigidbody2D rb;
    Animator anim;
    Vector2 spawnPoint;
    EnemyHealth ehScript;
    Friction fs; //Kitka scripti

	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        spawnPoint = transform.position;
        anim = GetComponent<Animator>();
        ehScript = GetComponent<EnemyHealth>();
        movementForce = rb.mass * (acceleration + GameManager.instance.globalFriction);
        fs = GetComponent<Friction>();
    }
	
	
	void Update () {
        if (PauseMenu.instance.isPaused || !ehScript.isAlive)
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
        movementVector = Vector2.zero;
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Hitting") && fs.isGrounded) { //jos ei lyödä niin liikutaan...
            if (directionToPlayer.x >= 0)
            {
                movementVector = movementForce * Vector2.right;
            }
            else
            {
                movementVector = movementForce * Vector2.left;
            }
            FaceRightDirection(directionToPlayer);
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
            FaceRightDirection(directionToPlayer);
        }
        else
        {
            anim.SetBool("Hitting", false);
        }
    }

    private void FixedUpdate()
    {
        rb.AddForce(movementVector);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            collision.transform.GetComponent<PlayerHealth>().TakeDamage(damage, transform, false);
        }
    }

    void FaceRightDirection(Vector2 directionToPlayer)
    {
        if (directionToPlayer.x >= 0)
        {
            transform.localScale = Vector3.one;
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
