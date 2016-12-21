using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;

public class CharacterMove : MonoBehaviour {
	ForwardStatus forwardStatus = ForwardStatus.NONE;
	ForwardStatus previousStatus = ForwardStatus.NONE;
	bool isBackToStart = false;
	int moveCount = 0;
	float moveSize = 0.06f;
	float cameraAngleY = 0;
	Vector3 startPos;
	Vector3 beforePos;
	public AudioSource stepAudioSource;

	[SerializeField] private VRInput m_VRInput;
	[SerializeField] private GameObject arrow;
	[SerializeField] private GameObject arrow3;
	[SerializeField] private GameObject rotateObject;
	[SerializeField] private GameObject wall;


	enum ForwardStatus
	{
		FRONT, BACK, RIGHT, LEFT, NONE
	}

	private void HandleClick(){
		beforePos = transform.position;
		forwardStatus = getCameraDirection();
	}

	// Use this for initialization
	void Start () {
		startPos = transform.position;
		m_VRInput.OnClick += HandleClick;
		previousStatus = ForwardStatus.FRONT;
	}

	// Update is called once per frame
	void Update () {
		if(forwardStatus == ForwardStatus.FRONT)
		{
			transform.Translate (0, 0, 0.25f);
			moveCount++;
			moveSize += 0.01f;
		}
		if (forwardStatus == ForwardStatus.BACK)
		{
			transform.Translate(0, 0, -0.25f);
			moveCount++;
		}
		if (forwardStatus == ForwardStatus.LEFT)
		{
			transform.Translate(-0.25f, 0, 0);
			moveCount++;
		}
		if (forwardStatus == ForwardStatus.RIGHT)
		{
			transform.Translate(0.25f, 0, 0);
			moveCount++;
		}

		if (moveCount >= 35) {
			statusDefault ();
		}  else if (moveCount == 1) {
			stepAudioSource.Play ();
		}
			
		ForwardStatus currentStatus = getCameraDirection();

		if(currentStatus == ForwardStatus.FRONT && previousStatus == ForwardStatus.LEFT){
			arrow3.transform.Rotate (0, -90, 0);
		} else if(currentStatus == ForwardStatus.FRONT && previousStatus == ForwardStatus.RIGHT){
			arrow3.transform.Rotate (0, 90, 0);
		} else if(currentStatus == ForwardStatus.LEFT && previousStatus == ForwardStatus.FRONT){
			arrow3.transform.Rotate (0, 90, 0);
		} else if (currentStatus > previousStatus) { // 시계방향
			arrow3.transform.Rotate (0, -90, 0);
		} else if (currentStatus < previousStatus){ // 반시계방향
			arrow3.transform.Rotate (0, 90, 0);
		}
		previousStatus = currentStatus;

		if (isBackToStart) {
			transform.position = Vector3.MoveTowards(transform.position, startPos, 100f * Time.deltaTime);
			if (transform.position == startPos) {
				isBackToStart = false;
			}
		}

	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Wall") {
			transform.position = beforePos;
		}else if (other.tag == "obstacle") {
//			Transform obstacle = wall.transform.Find("obstacle");
//			obstacle.GetComponent<Renderer>().enabled = true;
//			transform.position = startPos;
//			obstacle.GetComponent<Renderer>().enabled = false;
			StartCoroutine("showWallAndMoveStartPosition");
		}

		statusDefault ();
	}

	IEnumerator showWallAndMoveStartPosition()
	{
		switch (forwardStatus) {
			case ForwardStatus.FRONT:
				transform.Translate (0, 0, -3.0f);
				break;
			case ForwardStatus.BACK: 				transform.Translate (0, 0, 3.0f); 				break;
			case ForwardStatus.LEFT: 				transform.Translate (-3.0f, 0, 0); 				break;
			case ForwardStatus.RIGHT: 				transform.Translate (3.0f, 0, 0); 				break;
		}
		wall.GetComponent<Renderer>().enabled = true;
		yield return new WaitForSeconds(1);
//		transform.position = startPos;
		isBackToStart = true;
		wall.GetComponent<Renderer>().enabled = false;
	}


	void statusDefault()
	{
		forwardStatus = ForwardStatus.NONE;
		moveCount = 0;
		moveSize = 0.06f;
	}

	ForwardStatus getCameraDirection ()
	{
		ForwardStatus status = ForwardStatus.NONE;
		cameraAngleY = Camera.main.transform.eulerAngles.y;
		if (cameraAngleY > 315 || cameraAngleY <= 45) {
			status = ForwardStatus.FRONT;
		}else if (cameraAngleY > 45 && cameraAngleY <= 135) {
			status = ForwardStatus.RIGHT;
		}else if (cameraAngleY > 135 && cameraAngleY <= 225) {
			status = ForwardStatus.BACK;
		}else if (cameraAngleY > 225 && cameraAngleY <= 315) {
			status = ForwardStatus.LEFT;
		}
		return status;
	}
}