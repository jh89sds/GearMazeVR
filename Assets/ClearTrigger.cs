using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearTrigger : MonoBehaviour {
	public AudioSource audioSource;
	public ParticleSystem particleSystem;
	public GameObject goalIcon;
	bool isMovingToClear = false;
	float fTime = 0f;

	AsyncOperation asyncOper;

	public void Awake() {
		audioSource = GetComponent<AudioSource> ();
	}

	public void OnTriggerEnter(Collider other) {
		StartCoroutine(StartLoad("ClearMaze"));
	}

	void Update () {
		if (isMovingToClear) {
			fTime += Time.deltaTime;

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

		if (isMovingToClear == false)
		{
			isMovingToClear = true;

			audioSource.Play ();
			goalIcon.SetActive (false);
			particleSystem.Play ();

			while (asyncOper.progress < 0.9f)
			{
				yield return true;
			}
		}
	}
}
