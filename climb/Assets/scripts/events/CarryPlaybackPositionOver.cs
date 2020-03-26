/* Kind of a hotfix to stop songs from repeating when they schouldn't
 * after the player loads a new scene.  It's only gonna be used like, twice.*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarryPlaybackPositionOver : MonoBehaviour {

	// Private Variables
	private GameObject oldMusicController;
	private GameObject newMusicController;
	private bool start = true;
	private int playbackPos;

	private void Awake () {
		DontDestroyOnLoad (this.gameObject);
		try {
			oldMusicController = GameObject.Find ("music_controller");
		} catch {
			Debug.Log ("Can't find the Music Controller");
		}
	}

	private void LateUpdate () {
		if (oldMusicController != null) {
			playbackPos = oldMusicController.GetComponent<AudioSource> ().timeSamples;
		}
	}

	void OnEnable () {
		// Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
		SceneManager.sceneLoaded += OnLevelFinishedLoading;
	}

	void OnDisable () {
		// Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as this script is disabled.
		// Remember to always have an unsubscription for every delegate you subscribe to!
		SceneManager.sceneLoaded -= OnLevelFinishedLoading;
	}

	void OnLevelFinishedLoading (Scene scene, LoadSceneMode mode) {
		if (!start) {
			newMusicController = GameObject.Find ("music_controller");
			newMusicController.GetComponent<AudioSource> ().timeSamples = playbackPos;
			DestroyObject (this.gameObject);
		} else {
			start = false;
		}
	}
}
