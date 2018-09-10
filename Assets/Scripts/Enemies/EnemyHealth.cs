using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    public int health;

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (GetComponent<DMGFlash>() == null)
        {
            gameObject.AddComponent<DMGFlash>();
        }
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
