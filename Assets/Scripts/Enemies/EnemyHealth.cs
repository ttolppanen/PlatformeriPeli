using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    public int health;
    public bool isAlive;
    public float dieingForce;
    public Material dissolveMat;

    private void Start()
    {
        isAlive = true;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (GetComponent<DMGFlash>() == null)
        {
            gameObject.AddComponent<DMGFlash>();
        }
        if (health <= 0)
        {
            isAlive = false;
            Kill();
        }
    }

    void Kill()
    {
        
        foreach (Collider2D coll in GetComponents<Collider2D>())
        {
            Destroy(coll);
        }
        Destroy(transform.GetChild(0).gameObject);

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = -0.1f;
        Vector2 directionFromPlayer = transform.position - GameObject.FindGameObjectWithTag("Player").transform.position;
        directionFromPlayer.y = 0;
        rb.velocity = directionFromPlayer * dieingForce;

        dissolveMat.SetFloat("timeSinceLevelLoaded", Time.timeSinceLevelLoad);
        GetComponent<SpriteRenderer>().material = dissolveMat;
        Destroy(gameObject, 5);
        foreach (ParticleSystem smoke in transform.GetComponentsInChildren<ParticleSystem>())
        {
            smoke.Play();
        }
    }
}
