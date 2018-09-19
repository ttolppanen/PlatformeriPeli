using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagingEnemies : MonoBehaviour {

    public int damage;
    public GameObject damageEffect;
    Animator anim;
    BoxCollider2D hitBox;

    private void Start()
    {
        anim = transform.parent.GetComponent<Animator>();
        hitBox = GetComponent<BoxCollider2D>();
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
            Destroy(Instantiate(damageEffect, FindDamagePoint(collision.transform), damageEffect.transform.rotation), 0.1f); //Luodaan damageEffect ja annetaan samalla käskyä tuhota se 0.1s päästä
            collision.GetComponent<EnemyHealth>().TakeDamage(damage);
        }
    }

    Vector2 FindDamagePoint(Transform other)
    {
        Vector2 southWestCorner = (Vector2)hitBox.bounds.center - hitBox.size / 2;
        float circleRadius = hitBox.size.y / 2;

        Vector2[] points = new Vector2[4];

        for (int y = 0; y < 2; y++)
        {
            for (int x = 0; x < 2; x++)
            {
                points[2 * y + x] = CircleCastAndFindThis(southWestCorner + new Vector2(-(1 - 2 * x) * circleRadius + hitBox.size.x * x, hitBox.size.y * y), circleRadius, new Vector2(1 - 2 * x, 0), hitBox.size.x, other); // Tässä siis käydään kaikki hitboxin 4 nurkkaa läpi.
            }
        }
        Vector2 sum = Vector2.zero;
        int howManyTimesSummed = 0;
        foreach (Vector2 point in points)
        {
            if (point != new Vector2(-9999, -9999))
            {
                sum += point;
                howManyTimesSummed++;
            }
        }
        if (howManyTimesSummed != 0)
        {
            return sum / howManyTimesSummed;
        }
        return hitBox.bounds.center;
    }

    Vector2 CircleCastAndFindThis(Vector2 origin, float radius, Vector2 direction, float distance, Transform whatToFind)
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(origin, radius, direction, distance);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.transform == whatToFind)
            {
                return hit.point;
            }
        }
        return new Vector2(-9999, -9999); //Paikka minne ei pitäisi koskaan päästä...
    }
}
