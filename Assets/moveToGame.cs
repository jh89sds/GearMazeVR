using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveToGame : MonoBehaviour {
	int button=0;
	bool isMovingToGame=false;

	void Update () {
		if (Input.GetMouseButtonDown (button) && isMovingToGame == false) {
			isMovingToGame = true;
			Application.LoadLevel ("GearMaze");
		}
	}
}
