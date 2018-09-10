using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationHelper : MonoBehaviour {

    public Collider2D hitBox;

    void ToggleHitBox()
    {
        hitBox.enabled = !hitBox.enabled;
    }
}
