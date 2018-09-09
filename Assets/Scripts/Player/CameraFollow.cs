using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public float followForce;
    public Vector3 positionFromPlayer;
    Transform player;
    Rigidbody2D rb;

	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
	}

	void Update () {
        Vector3 direction = player.position + positionFromPlayer - transform.position;
        rb.AddForce(direction * Time.deltaTime * followForce);
	}
}
