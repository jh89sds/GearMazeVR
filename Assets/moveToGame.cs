using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class moveToGame : MonoBehaviour {
	int button=0;
	bool isMovingToGame=false;
	float fTime=0f;

	public TextMesh hiddenText;
	public Canvas slideCanvas;
	public Slider slider;
	AsyncOperation asyncOper;

	void Start () {
		slideCanvas.gameObject.SetActive (false);
	}

	void Update () {
		if (Input.GetMouseButtonDown (button) && isMovingToGame == false) {
			StartCoroutine(StartLoad("GearMaze"));
		}

		if (isMovingToGame) {
			fTime += Time.deltaTime;
			slider.value = fTime;

			if (fTime >= 2)
			{
				asyncOper.allowSceneActivation = true;
			}
		}
	}


	public IEnumerator StartLoad(string strSceneName)
	{
		asyncOper = Application.LoadLevelAsync(strSceneName);
		asyncOper.allowSceneActivation = false;

		if (isMovingToGame == false)
		{
			isMovingToGame = true;
			hiddenText.gameObject.SetActive (false);
			slideCanvas.gameObject.SetActive (true);

			while (asyncOper.progress < 0.9f)
			{
//				slider.value = asyncOper.progress;
				yield return true;
			}
		}
	}
}
