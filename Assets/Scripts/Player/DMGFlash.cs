using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DMGFlash : MonoBehaviour {

    Material originalMat;
    SpriteRenderer sr;

	void Start () {
        sr = GetComponent<SpriteRenderer>();
        originalMat = sr.material;
        sr.material = GameManager.instance.flashMat;
        StartCoroutine(StopFlashing());
    }

    IEnumerator StopFlashing()
    {
        yield return new WaitForSeconds(0.1f);
        sr.material = originalMat;
        Destroy(this);
    }
}
