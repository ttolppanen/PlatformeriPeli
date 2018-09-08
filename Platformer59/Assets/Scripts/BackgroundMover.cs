using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMover : MonoBehaviour {

    public float followSpeed;
    Transform camera;
    Vector3 lastPlayerPosition;

	void Start () {
        camera = GameObject.FindGameObjectWithTag("MainCamera").transform;
        lastPlayerPosition = camera.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        float horizontalDistance = camera.position.x - transform.position.x;
        if (horizontalDistance > 32)
        {
            transform.position += new Vector3(64, 0, 0);
        }
        else if (horizontalDistance < -32)
        {
            transform.position -= new Vector3(64, 0, 0);
        }

        //Update position
        transform.position += (camera.transform.position - lastPlayerPosition) * followSpeed;
        lastPlayerPosition = camera.transform.position;
    }
}
