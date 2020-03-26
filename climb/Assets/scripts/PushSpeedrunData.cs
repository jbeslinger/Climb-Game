using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PushSpeedrunData : MonoBehaviour {

	public Text [] speedrunTimeTexts = new Text [5];

	private void OnEnable () {
		// If the player hasn't beaten the game, skip this
		if (!GameManager.gameData.playerHasBeatenGame) {
			return;
		}

		// Run through each Text and parse the time into a string
		for (int i = 0; i < speedrunTimeTexts.Length; i++) {
			speedrunTimeTexts [i].text = ParseTime (GameManager.gameData.bestTimes [i]);
		}
	}

	private string ParseTime (float rawTime) {
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

		return (hhmmssString + millisecondsString);
	}

}
