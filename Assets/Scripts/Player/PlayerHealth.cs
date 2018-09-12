using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    public int health;
    public int shieldAbsorb;
    public float knockBackForce;
    public GameObject[] hearts = new GameObject[1];
    Rigidbody2D rb;
    Animator anim;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        UpdateHearts();
    }

    public void TakeDamage(int damage, Vector2 enemyPosition, bool isMelee)
    {
        float enemyToPlayerX = transform.position.x - enemyPosition.x;
        if (isMelee && anim.GetBool("Shielding") && (enemyToPlayerX / Mathf.Abs(enemyToPlayerX) != transform.localScale.x))
        {
            if (damage / shieldAbsorb > 0)
            {
                health -= damage / shieldAbsorb;
                if (GetComponent<DMGFlash>() == null)
                {
                    gameObject.AddComponent<DMGFlash>();
                }
            }
           
            //Knockback
            if (enemyToPlayerX > 0)
            {
                rb.AddForce(knockBackForce * rb.mass * Vector2.one.normalized / 2, ForceMode2D.Impulse);
            }
            else
            {
                rb.AddForce(knockBackForce * rb.mass * new Vector2(-1, 1).normalized / 2, ForceMode2D.Impulse);
            }
        }
        else
        {
            health -= damage;
            if (GetComponent<DMGFlash>() == null)
            {
                gameObject.AddComponent<DMGFlash>();
            }

            //Knockback
            if (enemyToPlayerX > 0)
            {
                rb.AddForce(knockBackForce * rb.mass * Vector2.one.normalized, ForceMode2D.Impulse);
            }
            else
            {
                rb.AddForce(knockBackForce * rb.mass * new Vector2(-1, 1).normalized, ForceMode2D.Impulse);
            }
        }
        UpdateHearts();
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i <= health - 1)
            {
                hearts[i].SetActive(true);
            }
            else
            {
                hearts[i].SetActive(false);
            }
        }
    }
}
