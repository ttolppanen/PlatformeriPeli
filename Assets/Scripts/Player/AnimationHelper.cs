using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHelper : MonoBehaviour {

    public GameObject hitBox;

    public void TurnOnHitBox()
    {
        hitBox.SetActive(true);
    }
}
