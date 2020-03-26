using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedrunTimer : MonoBehaviour {

	[HideInInspector] public bool running = false;
	[HideInInspector] public float rawTime;
	public Text timeHHMMSS, timeMilliseconds; // The Text that will be updated
	public AudioSource musicPlayer;

	private void Update () {
		if (!running || GameManager.gameIsPaused) {
			return;
		}

		rawTime += Time.deltaTime;
		ParseTime (rawTime);
	}

	public void StartTimer () {
		running = true;
		musicPlayer.Play ();
	}

	public void StopTimer() {
		running = false;
		musicPlayer.Stop ();
	}

	// This will update the Text variables from the raw time captured by the timer
	private void ParseTime (float rawTime) {
		// Chop up the time into hours, minutes, and seconds
		int hours = (int)(rawTime / 3600);
		float choppedTime = rawTime % 3600;
		int minutes = (int)(choppedTime / 60);
		choppedTime %= 60;
		int seconds = (int)(choppedTime);
		choppedTime %= 1;

		// Check to see how long the numbers are and if they're 1 digit, add 1 leading 0
		// Basically pointless, but satsifying
		string hhmmssString = hours.ToString ("00") + ":";
		hhmmssString += minutes.ToString ("00") + ":";
		hhmmssString += seconds.ToString ("00");

		string millisecondsString = choppedTime.ToString ("0.00");
		millisecondsString = millisecondsString.Substring (1);

		timeHHMMSS.text = hhmmssString;
		timeMilliseconds.text = millisecondsString;
	}

}
