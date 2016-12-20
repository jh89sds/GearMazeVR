using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrigger : MonoBehaviour {

	public AudioSource audioSource;

	public void Awake() {
		audioSource = GetComponent<AudioSource> ();
	}

	public void OnTriggerEnter(Collider other)
	{
		audioSource.Play ();
	}
}
