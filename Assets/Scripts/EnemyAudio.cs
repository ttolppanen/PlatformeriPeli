using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudio : MonoBehaviour {

    AudioSource audioSource;

	void Start () {
        audioSource = GetComponent<AudioSource>();
	}

    public void PlayAudio()
    {
        audioSource.Play();
    }
}
