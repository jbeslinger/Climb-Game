using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaytimeController : MonoBehaviour {

	// Public Variables
	public float playtimeThisSession = 0.0f; // The raw amount of seconds since last frame

	// Private Variables
	private float maxPlaytime = 359999; // The timer will stop here; there's no point in counting further

	private void OnEnable () {
		playtimeThisSession = GameManager.gameData.playtime;
	}

	private void Update () {
		if (!GameManager.gameIsPaused && playtimeThisSession <= maxPlaytime) {
			playtimeThisSession += Time.deltaTime;
		}
	}

}
