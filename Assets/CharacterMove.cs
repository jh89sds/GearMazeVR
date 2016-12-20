﻿using System.Collections; using System.Collections.Generic; using UnityEngine; using VRStandardAssets.Utils;  public class CharacterMove : MonoBehaviour { 	ForwardStatus forwardStatus = ForwardStatus.NONE; 	ForwardStatus previousStatus = ForwardStatus.NONE; 	int moveCount = 0; 	float moveSize = 0.06f; 	float cameraAngleY = 0; 	Vector3 startPos; 	Vector3 beforePos; 	public AudioSource stepAudioSource;  	[SerializeField] private VRInput m_VRInput; 	[SerializeField] private GameObject arrow; 	[SerializeField] private GameObject arrow3; 	[SerializeField] private GameObject rotateObject;  	enum ForwardStatus 	{ 		FRONT, BACK, RIGHT, LEFT, NONE 	}  	private void HandleClick(){ 		beforePos = transform.position; 		forwardStatus = getCameraDirection(); 	}  	// Use this for initialization 	void Start () { 		startPos = transform.position; 		m_VRInput.OnClick += HandleClick; 	}  	// Update is called once per frame 	void Update () { 		if(forwardStatus == ForwardStatus.FRONT) 		{ 			transform.Translate (0, 0, 0.25f); 			moveCount++; 			moveSize += 0.01f; 		} 		if (forwardStatus == ForwardStatus.BACK) 		{ 			transform.Translate(0, 0, -0.25f); 			moveCount++; 		} 		if (forwardStatus == ForwardStatus.RIGHT) 		{ 			transform.Translate(-0.25f, 0, 0); 			moveCount++; 		} 		if (forwardStatus == ForwardStatus.LEFT) 		{ 			transform.Translate(0.25f, 0, 0); 			moveCount++; 		}  		if (moveCount >= 35) { 			statusDefault (); 		} else if (moveCount == 1) { 			stepAudioSource.Play (); 		}  		ForwardStatus currentStatus = getCameraDirection(); 		if (currentStatus != previousStatus) { 			if(currentStatus == ForwardStatus.FRONT) 			{ 				arrow3.transform.Rotate (0, 0, 0); 			} 			if (currentStatus == ForwardStatus.BACK) 			{ 				arrow3.transform.Rotate (0, 180, 0); 			} 			if (currentStatus == ForwardStatus.RIGHT) 			{ 				arrow3.transform.Rotate (0, 90, 0); 				rotateObject.transform.Rotate(0, 90, 0); 			} 			if (currentStatus == ForwardStatus.LEFT) 			{ 				arrow3.transform.Rotate (0, -90, 0); 			} 		} 		previousStatus = currentStatus;  	}  	void OnTriggerEnter(Collider other) 	{ 		statusDefault (); 		if (other.tag == "Wall") { 			transform.position = beforePos; 		}  else if (other.tag == "obstacle") { 			transform.position = startPos; 		} 	}  	void statusDefault() 	{ 		forwardStatus = ForwardStatus.NONE; 		moveCount = 0; 		moveSize = 0.06f; 	}  	ForwardStatus getCameraDirection () 	{ 		ForwardStatus status = ForwardStatus.NONE; 		cameraAngleY = Camera.main.transform.eulerAngles.y; 		if (cameraAngleY > 315 || cameraAngleY <= 45) { 			status = ForwardStatus.FRONT; 		}else if (cameraAngleY > 45 && cameraAngleY <= 135) { 			status = ForwardStatus.LEFT; 		}else if (cameraAngleY > 135 && cameraAngleY <= 225) { 			status = ForwardStatus.BACK; 		}else if (cameraAngleY > 225 && cameraAngleY <= 315) { 			status = ForwardStatus.RIGHT; 		} 		return status; 	} }  