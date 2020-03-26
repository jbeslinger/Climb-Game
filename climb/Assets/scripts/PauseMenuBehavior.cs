using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuBehavior : MonoBehaviour {

	// Public Variables
	public GameObject pausePanel;
	public bool cutsceneIsPlaying = false;

	// Private Variables
	private GameObject player;
//	private GameObject musicController;
//	private AudioSource myAudioSource;

	private void Start () {
//		musicController = GameObject.Find ("music_controller");
//		myAudioSource = musicController.GetComponent<AudioSource> ();
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	private void Update () {
		if (player == null) {
			player = GameObject.FindGameObjectWithTag ("Player");
		}

		if (InputManager.PauseButtonDown () && !cutsceneIsPlaying && !GameManager.navigatingFurnitureMenu) {
			if (GameManager.gameIsPaused && pausePanel.activeSelf) {
				Resume ();
			} else if (GameManager.gameIsPaused && !pausePanel.activeSelf) {
				return;
			} else {
				GameManager.PauseGame ();
			}
		}
	}

	// Seems useless, but it's in instance-wrapper for this static method
	// It's only use is to be called by the On Click () method on GUI objects
	public void Resume () {
		GameManager.ResumeGame ();
	}

}
