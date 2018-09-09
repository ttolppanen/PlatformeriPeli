using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagingEnemies : MonoBehaviour {

    public int damage;
    Animator anim;

    private void Start()
    {
        anim = transform.parent.GetComponent<Animator>();
    }

    private void Update()
    {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("UkkoHit"))
        {
            gameObject.SetActive(false);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<EnemyHealth>().TakeDamage(damage);
        }
    }
}
