using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrigger : MonoBehaviour {

	public AudioSource audioSource;
	public GameObject knightObj;

	public void Awake() {
		audioSource = GetComponent<AudioSource> ();
	}

	public void OnTriggerEnter(Collider other) {
		if (!knightObj.GetComponent<CharacterMove> ().isBackToStart) {
			audioSource.Play ();
		}
	}
}
