using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedrunFlagBehavior : MonoBehaviour {

	public SpeedrunTimer timer;
	public Animator speedrunEndCutscene;

	private PlayerController player;

	private void OnEnable () {
		if (!GameManager.speedrunning) {
			gameObject.SetActive (false);
			return;
		}

		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();
	}

	private void OnTriggerEnter2D (Collider2D other) {
		timer.StopTimer ();
		player.pauseInput = true;
		GameObject.FindObjectOfType<PauseMenuBehavior> ().cutsceneIsPlaying = true;
		speedrunEndCutscene.enabled = true;
	}

}
