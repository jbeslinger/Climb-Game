using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEndCutscene : MonoBehaviour {

	// Public Variables
	public GameObject playerObj, graveObj, cameraObj, keyPromptTextObj, speedrunUnlockTextObj, creditsTextObj; // The script will activate the Animator components of these objects
	public PauseMenuBehavior pauseMenu;
	public bool triggered = false;

	private void Start () {
		if (GameManager.gameData.playerHasBeatenGame) {
			speedrunUnlockTextObj.SetActive (false);
		}
	}

	private void Update () {
		if (!triggered) {
			return;
		} else {
			pauseMenu.cutsceneIsPlaying = true;
			GameObject.FindGameObjectWithTag ("Player").GetComponent<Rigidbody2D> ().simulated = false;
			GameObject.FindGameObjectWithTag ("Player").GetComponent<SpriteRenderer> ().enabled = false;
			GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController>().pauseInput = true;
			GameObject.Find ("ortho_camera").GetComponent<Camera> ().enabled = false;
			GameObject.Find ("ortho_camera").GetComponent<AudioListener> ().enabled = false;
			GameObject.Find ("ortho_camera").GetComponent<CamHandler> ().enabled = false;
			GameObject.Find ("music_controller").GetComponent<AudioSource> ().enabled = false;

			playerObj.GetComponent<Animator> ().enabled = true;
			graveObj.GetComponent<Animator> ().enabled = true;
			cameraObj.GetComponent<Animator> ().enabled = true;
			keyPromptTextObj.GetComponent<Animator> ().enabled = true;
			speedrunUnlockTextObj.GetComponent<Animator> ().enabled = true;
			creditsTextObj.GetComponent<Animator> ().enabled = true;
		}
	}

	private void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.CompareTag ("Player")) {
			triggered = true;
		}
	}

}
