/* This is the script that's called when
 * the New Game button is clicked on the title screen. */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class LoadGameBehavior : MonoBehaviour {

	public int fileNumber;

    // Load a specified scene based on index
    public void LoadByIndex (int sceneIndex) {
		GameManager.newGame = true;
		GameManager.fileNumber = fileNumber;
		GameManager.DeleteGame (fileNumber);
		SceneManager.LoadScene (sceneIndex);
    }

	public void LoadNewGame () {
		GameManager.newGame = true; // Tell the game it's a new game
		GameManager.fileNumber = fileNumber; // Set the current file number to this one's
		GameManager.DeleteGame (fileNumber); // Delete it
		GameManager.gameData = new GameData ();
		SceneManager.LoadScene (1); // Load World 1
	}

	public void LoadFromFile () {
		if (!File.Exists (Application.persistentDataPath + "/climbSave" + fileNumber + ".sav")) {
			return;
		}

		GameManager.newGame = false; // Tell the game it's NOT a new game
		GameManager.fileNumber = fileNumber; // Set the current file number to this one's
		GameManager.LoadGame (); // Deserialize the gameData
		SceneManager.LoadScene (GameManager.gameData.playerSceneID); // Load the last world the player was in
	}

	public void DeleteThisFile () {
		GameManager.DeleteGame (fileNumber);
	}

}
