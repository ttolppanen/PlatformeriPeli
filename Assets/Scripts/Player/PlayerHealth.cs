using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    public int health;
    public int shieldAbsorb;
    public float knockBackForce;
    public GameObject[] hearts = new GameObject[1];
    public GameObject halfHeart;
    public float damageHeartSpeed;
    Rigidbody2D rb;
    Animator anim;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        UpdateHearts();
    }

    public void TakeDamage(int damage, Transform enemy, bool isMelee)
    {
        float enemyToPlayerX = transform.position.x - enemy.transform.position.x;
        if (isMelee && anim.GetBool("Shielding") && (enemyToPlayerX / Mathf.Abs(enemyToPlayerX) != transform.localScale.x))
        {
            if (damage / shieldAbsorb > 0)
            {
                health -= damage / shieldAbsorb;
                StartCoroutine(SpawnDamageHearts(damage / shieldAbsorb));
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
            StartCoroutine(SpawnDamageHearts(damage));
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

    IEnumerator SpawnDamageHearts(int damage)
    {
        bool shouldSpawnRight = true;
        int heartSpawnAmount = Mathf.Min(health, damage);
        int startHealth = health;
        for (int i = 0; i < heartSpawnAmount; i++)
        {
            GameObject halfHeartInstance = Instantiate(halfHeart, transform.position + new Vector3(0, 1f, 0), Quaternion.identity);
            Rigidbody2D heartRb = halfHeartInstance.GetComponent<Rigidbody2D>();
            heartRb.velocity = damageHeartSpeed * new Vector2(Random.Range(-1f, 1f), 1);
            heartRb.angularVelocity = Random.Range(0, 3f) * 360;
            Destroy(halfHeartInstance, 1.5f);
            if (!shouldSpawnRight)
            {
                halfHeartInstance.transform.localScale = new Vector3(-1, 1, 1);
            }
            shouldSpawnRight = !shouldSpawnRight;
            if (damage < startHealth)
            {
                yield return new WaitForSeconds(0.02f);
            }
        }
    }
}
