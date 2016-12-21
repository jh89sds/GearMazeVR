using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTrigger : MonoBehaviour {

	public Animation wallAnimation;
	public OVRCameraRig targetCamera;

	public void Awake() {
		wallAnimation = GetComponent<Animation> ();
	}

	public void OnTriggerEnter(Collider other)
	{
		wallAnimation.Play();		
		targetCamera.GetComponent<CameraShake>().enabled = true;
		targetCamera.GetComponent<CameraShake>().shakeDuration = 0.5f;
	}
}
