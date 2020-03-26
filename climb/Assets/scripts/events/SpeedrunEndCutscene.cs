using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SpeedrunEndCutscene : MonoBehaviour {

	public SpeedrunTimer timer;
	public GameObject endFlag;
	public Sprite engSprite, prtSprite; // Sprites for different languages
	public Text newRecord;

	private PlayerController player;
	private AudioSource myAudioSource;
//	private Animator myAnimator;

	private void OnEnable () {
		if (!GameManager.speedrunning) { // Turn this off if the player isn't speedrunning the level
			gameObject.SetActive (false);
			return;
		}

		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();
		myAudioSource = GetComponent<AudioSource> ();
//		myAnimator = GetComponent<Animator> ();

		endFlag.SetActive (true);
	}

	// If the current time is smaller than the best time for this scene, then set the gameData variable
	// Or, if the record hasn't been set yet, i.e. == 0
	public void CheckRecord () {
		if (timer.rawTime < GameManager.gameData.bestTimes [SceneManager.GetActiveScene ().buildIndex - 1] ||
			GameManager.gameData.bestTimes [SceneManager.GetActiveScene ().buildIndex - 1] == 0f) {

			newRecord.enabled = true;
			GameManager.gameData.bestTimes [SceneManager.GetActiveScene ().buildIndex - 1] = timer.rawTime;
		}
	}

	public void Play () {
		myAudioSource.Play ();
	}

	public void FadeToHouse () {
		GameObject.FindObjectOfType<FadeOut> ().FadeToLevel (6);
		GameManager.speedrunning = false;
	}

	private void OnTriggerEnter2D (Collider2D other) {
		timer.StopTimer ();
		player.pauseInput = true;
		GameObject.FindObjectOfType<PauseMenuBehavior> ().cutsceneIsPlaying = true;
	}

}
