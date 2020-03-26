using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedrunStartCutscene : MonoBehaviour {

	public SpeedrunTimer timer;
	public GameObject restartButton, levelSelectButton; // This script swaps these buttons
	public Sprite engSprite, prtSprite; // Sprites for different languages
	public AudioSource musicController;

	private PlayerController player;
	private AudioSource myAudioSource;

	private void OnEnable () {
		if (!GameManager.speedrunning) { // Turn this off if the player isn't speedrunning the level
			gameObject.SetActive (false);
			return;
		}

		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();
		player.pauseInput = true;
		GameObject.FindObjectOfType<PauseMenuBehavior> ().cutsceneIsPlaying = true;

		myAudioSource = GetComponent<AudioSource> ();

		timer.gameObject.SetActive (true);

		levelSelectButton.SetActive (false);
		restartButton.SetActive (true);
	}

	private void Start () {
		musicController.Stop ();
	}

	public void Play (AudioClip clip) {
		myAudioSource.clip = clip;
		myAudioSource.Play ();
	}

	public void UnpausePlayer () {
		player.pauseInput = false;
		GameObject.FindObjectOfType<PauseMenuBehavior> ().cutsceneIsPlaying = false;
	}

	public void StartTimer () {
		timer.StartTimer ();
	}

}
