using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwing : MonoBehaviour {

    public float angle;
    //public float throwingTime;
    public float maxThrowingVelocity;
    public GameObject ammo;
    GameObject player;
    float realAngle;
    float time;

	void Start () {
        angle *= Mathf.Deg2Rad;
        realAngle = angle;
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update ()
    {
        if ((player.transform.position.x - transform.position.x) >= 0)
        {
            realAngle = angle;
        }
        else
        {
            realAngle = Mathf.PI - angle;
        }
        /*
        time += Time.deltaTime;
        if (time > throwingTime)
        {
            time = 0;
            Throw();
        }*/
	}

    public void Throw()
    {
        Vector2 toPlayer = player.transform.position - transform.position;
        Vector2 playerSpeed = player.GetComponent<Rigidbody2D>().velocity;

        float a = Mathf.Pow(Mathf.Cos(realAngle), 2) * toPlayer.y - toPlayer.x * 0.5f * Mathf.Sin(2 * realAngle);
        float b = Mathf.Cos(realAngle) * playerSpeed.y * toPlayer.x - 2 * Mathf.Cos(realAngle) * toPlayer.y * playerSpeed.x + playerSpeed.x * toPlayer.x * Mathf.Sin(realAngle);
        float c = Mathf.Pow(playerSpeed.x, 2) * toPlayer.y - playerSpeed.x * playerSpeed.y * toPlayer.x - 0.5f * Physics2D.gravity.y * Mathf.Pow(toPlayer.x , 2); //g laskuissa positiivinen, tässä g on vektorinkomponentti, joten -, siitä - tuohon loppuunnnn
        float throwingVelocity = Functions.SecondDegreeSolver(a, b, c, false);

        if (!float.IsNaN(throwingVelocity))
        {
            if (throwingVelocity >= maxThrowingVelocity)
            {
                throwingVelocity = maxThrowingVelocity;
            }
            GameObject ammoInstance = Instantiate(ammo, transform.position, ammo.transform.rotation);
            ammoInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos(realAngle), Mathf.Sin(realAngle)) * throwingVelocity;
        }
    }
}
