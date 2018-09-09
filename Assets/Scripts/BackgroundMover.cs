using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMover : MonoBehaviour {

    public float followSpeed;
    Transform cameraTransform;
    Vector3 lastPlayerPosition;

	void Start () {
        cameraTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
        lastPlayerPosition = cameraTransform.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        float horizontalDistance = cameraTransform.position.x - transform.position.x;
        if (horizontalDistance > 32)
        {
            transform.position += new Vector3(64, 0, 0);
        }
        else if (horizontalDistance < -32)
        {
            transform.position -= new Vector3(64, 0, 0);
        }

        //Update position
        transform.position += (cameraTransform.transform.position - lastPlayerPosition) * followSpeed;
        lastPlayerPosition = cameraTransform.transform.position;
    }
}
