using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;

public class MainMenuBehaviors : MonoBehaviour {

	public int fileNumber;

	public void SetFileNumber () {
		GameManager.fileNumber = fileNumber; // Set the current file number to this one's
	}

	public void StartNewGame (InputField input) {
		GameManager.newGame = true; // Tell the game it's a new game
		GameManager.DeleteGame (GameManager.fileNumber);
		GameManager.gameData = new GameData ();
		GameManager.gameData.fileName = input.text;

		SceneManager.LoadScene (1); // Load World 1
	}

	public void LoadFromFile () {
		if (!File.Exists (Application.persistentDataPath + "/climbSave" + fileNumber + ".sav")) {
			return;
		}

		GameManager.newGame = false; // Tell the game it's NOT a new game
		GameManager.loadingGame = true; // Tell the game it's loading from a file
		GameManager.fileNumber = fileNumber; // Set the current file number to this one's
		GameManager.LoadGame (); // Deserialize the gameData
		SceneManager.LoadScene (GameManager.gameData.playerSceneID); // Load the last world the player was in
	}

	public void DeleteThisFile () {
		GameManager.DeleteGame (fileNumber);
	}

}
