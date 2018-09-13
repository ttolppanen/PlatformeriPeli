using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationHelper : MonoBehaviour {

    public GameObject hitBox;

    void TurnOnHitBox()
    {
        hitBox.SetActive(true);
    }
    void TurnOffHitBox()
    {
        hitBox.SetActive(false);
    }
}
