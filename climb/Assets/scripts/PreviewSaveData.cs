using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class PreviewSaveData : MonoBehaviour {

	public Text[] file1Texts = new Text[5]; // 0 - File name, 1 - Gem count, 2 - Playtime, 3 - Coin count, 4 - Placeholder text
	public Text[] file2Texts = new Text[5];
	public Text[] file3Texts = new Text[5];

	public SpriteRenderer file1CoinIcon, file2CoinIcon, file3CoinIcon; // The three coin icons for each file
	public Image file1GemIcon, file2GemIcon, file3GemIcon; // The three gem icons for each file

	public void PushSaveDataToPanel () {
		// Checking for File 1
		if (File.Exists (Application.persistentDataPath + "/climbSave1.sav")) {
			// Try to deserialize the gameData into a temporary variable
			try {
				BinaryFormatter binaryFormatter = new BinaryFormatter ();
				FileStream file = File.Open (Application.persistentDataPath + "/climbSave1.sav", FileMode.Open);
				GameData gameData = (GameData)binaryFormatter.Deserialize (file);
				file.Close ();

				// Then, set up the Text boxes and such
				file1CoinIcon.gameObject.SetActive (true);
				file1GemIcon.gameObject.SetActive (true);
				file1Texts [0].text = gameData.fileName;
				file1Texts [0].gameObject.SetActive (true);
				file1Texts [1].text = gameData.gemCount + "/16";
				file1Texts [1].gameObject.SetActive (true);
				file1Texts [2].text = PlaytimeToString (gameData.playtime);
				file1Texts [2].gameObject.SetActive (true);
				file1Texts [3].text = gameData.coinCount.ToString ();
				file1Texts [3].gameObject.SetActive (true);
				file1Texts [4].gameObject.SetActive (false);
			} catch {
				return;
			}
		} else {
			file1CoinIcon.gameObject.SetActive (false);
			file1GemIcon.gameObject.SetActive (false);
			file1Texts [0].text = "";
			file1Texts [0].gameObject.SetActive (false);
			file1Texts [1].text = "";
			file1Texts [1].gameObject.SetActive (false);
			file1Texts [2].text = "";
			file1Texts [2].gameObject.SetActive (false);
			file1Texts [3].text = "";
			file1Texts [3].gameObject.SetActive (false);
			file1Texts [4].gameObject.SetActive (true);
		}

		// Checking for File 2
		if (File.Exists (Application.persistentDataPath + "/climbSave2.sav")) {
			// Try to deserialize the gameData into a temporary variable
			try {
				BinaryFormatter binaryFormatter = new BinaryFormatter ();
				FileStream file = File.Open (Application.persistentDataPath + "/climbSave2.sav", FileMode.Open);
				GameData gameData = (GameData)binaryFormatter.Deserialize (file);
				file.Close ();

				// Then, set up the Text boxes and such
				file2CoinIcon.gameObject.SetActive (true);
				file2GemIcon.gameObject.SetActive (true);
				file2Texts [0].text = gameData.fileName;
				file2Texts [0].gameObject.SetActive (true);
				file2Texts [1].text = gameData.gemCount + "/16";
				file2Texts [1].gameObject.SetActive (true);
				file2Texts [2].text = PlaytimeToString (gameData.playtime);
				file2Texts [2].gameObject.SetActive (true);
				file2Texts [3].text = gameData.coinCount.ToString ();
				file2Texts [3].gameObject.SetActive (true);
				file2Texts [4].gameObject.SetActive (false);
			} catch {
				return;
			}
		} else {
			file2CoinIcon.gameObject.SetActive (false);
			file2GemIcon.gameObject.SetActive (false);
			file2Texts [0].text = "";
			file2Texts [0].gameObject.SetActive (false);
			file2Texts [1].text = "";
			file2Texts [1].gameObject.SetActive (false);
			file2Texts [2].text = "";
			file2Texts [2].gameObject.SetActive (false);
			file2Texts [3].text = "";
			file2Texts [3].gameObject.SetActive (false);
			file2Texts [4].gameObject.SetActive (true);
		}

		// Checking for File 3
		if (File.Exists (Application.persistentDataPath + "/climbSave3.sav")) {
			// Try to deserialize the gameData into a temporary variable
			try {
				BinaryFormatter binaryFormatter = new BinaryFormatter ();
				FileStream file = File.Open (Application.persistentDataPath + "/climbSave3.sav", FileMode.Open);
				GameData gameData = (GameData)binaryFormatter.Deserialize (file);
				file.Close ();

				// Then, set up the Text boxes and such
				file3CoinIcon.gameObject.SetActive (true);
				file3GemIcon.gameObject.SetActive (true);
				file3Texts [0].text = gameData.fileName;
				file3Texts [0].gameObject.SetActive (true);
				file3Texts [1].text = gameData.gemCount + "/16";
				file3Texts [1].gameObject.SetActive (true);
				file3Texts [2].text = PlaytimeToString (gameData.playtime);
				file3Texts [2].gameObject.SetActive (true);
				file3Texts [3].text = gameData.coinCount.ToString ();
				file3Texts [3].gameObject.SetActive (true);
				file3Texts [4].gameObject.SetActive (false);
			} catch {
				return;
			}
		} else {
			file3CoinIcon.gameObject.SetActive (false);
			file3GemIcon.gameObject.SetActive (false);
			file3Texts [0].text = "";
			file3Texts [0].gameObject.SetActive (false);
			file3Texts [1].text = "";
			file3Texts [1].gameObject.SetActive (false);
			file3Texts [2].text = "";
			file3Texts [2].gameObject.SetActive (false);
			file3Texts [3].text = "";
			file3Texts [3].gameObject.SetActive (false);
			file3Texts [4].gameObject.SetActive (true);
		}
	}

	public void ResetPanelData () {
		file1GemIcon.gameObject.SetActive (false);
		file1Texts [0].text = "";
		file1Texts [0].gameObject.SetActive (false);
		file1Texts [1].text = "";
		file1Texts [1].gameObject.SetActive (false);
		file1Texts [2].text = "";
		file1Texts [2].gameObject.SetActive (false);
		file1Texts [3].text = "";
		file1Texts [3].gameObject.SetActive (false);
		file1Texts [4].gameObject.SetActive (true);

		file2GemIcon.gameObject.SetActive (false);
		file2Texts [0].text = "";
		file2Texts [0].gameObject.SetActive (false);
		file2Texts [1].text = "";
		file2Texts [1].gameObject.SetActive (false);
		file2Texts [2].text = "";
		file2Texts [2].gameObject.SetActive (false);
		file2Texts [3].text = "";
		file2Texts [3].gameObject.SetActive (false);
		file2Texts [4].gameObject.SetActive (true);

		file3GemIcon.gameObject.SetActive (false);
		file3Texts [0].text = "";
		file3Texts [0].gameObject.SetActive (false);
		file3Texts [1].text = "";
		file3Texts [1].gameObject.SetActive (false);
		file3Texts [2].text = "";
		file3Texts [2].gameObject.SetActive (false);
		file3Texts [3].text = "";
		file3Texts [3].gameObject.SetActive (false);
		file3Texts [4].gameObject.SetActive (true);
	}

	private string PlaytimeToString (float rawTime) {
		// Chop up the time into hours, minutes, and seconds
		int hours = (int)(rawTime / 3600);
		float choppedTime = rawTime % 3600;
		int minutes = (int)(choppedTime / 60);
		choppedTime %= 60;
		int seconds = (int)choppedTime;

		// Check to see how long the numbers are and if they're 1 digit, add 1 leading 0
		// Basically pointless, but satsifying
		string sHours = hours.ToString ("00");
		string sMinutes = minutes.ToString ("00");
		string sSeconds = seconds.ToString ("00");

		return sHours + ":" + sMinutes + ":" + sSeconds;
	}

}
