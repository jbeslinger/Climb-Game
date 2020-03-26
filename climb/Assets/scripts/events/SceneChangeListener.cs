using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeListener : MonoBehaviour {

	// Public Variables
	public GameObject [] ponchoPrefabs = new GameObject [7]; // This array holds all of the poncho prefabs in order (According to the unlockedCostumes array in gameData
	public GameObject lampPrefab; // If it's a Cave level, then instantiate this lamp prefab

	private void OnEnable () {
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	private void OnDisable () {
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	private void OnSceneLoaded (Scene scene, LoadSceneMode mode) {
		if (scene.buildIndex != 0) {
			GameManager.LoadGame ();
		}

		// If it's the title screen, destroy the GameController instance & nullify the instance var
		if (scene.buildIndex == 0 && GameController.instance != null) {
			Destroy (GameController.instance.gameObject);
			GameController.instance = null;
			return;
		} else if (GameController.instance != null) {
//			GameManager.LoadGame ();
			GameController.instance.CheckTriggers ();
		}

		if (scene.buildIndex == 0 || GameManager.newGame) {
			GameManager.endOfGameFlag = false;
			return;
		}

		// Special instructions for loading the player house level
		if (scene.buildIndex == 6) {
			GameManager.LoadGame ();

			// Checking which player costume to load
			GameObject currentPlayer = GameObject.FindGameObjectWithTag ("Player");
			GameObject replacementPlayer = Instantiate (ponchoPrefabs [GameManager.gameData.playerCostume]);
			replacementPlayer.GetComponent<SpriteRenderer> ().color = currentPlayer.GetComponent<SpriteRenderer> ().color;
			replacementPlayer.GetComponent<PlayerController>().numberOfCoins = GameManager.gameData.coinCount;
			replacementPlayer.transform.position = currentPlayer.transform.position;
			Destroy (currentPlayer);
		}

		// Change the player start position if loading from a file
		if (GameManager.loadingGame) {
			// Checking which player costume to load
			GameObject currentPlayer = GameObject.FindGameObjectWithTag ("Player");
			GameObject replacementPlayer = Instantiate (ponchoPrefabs [GameManager.gameData.playerCostume]);
			replacementPlayer.GetComponent<SpriteRenderer> ().color = currentPlayer.GetComponent<SpriteRenderer> ().color;
			replacementPlayer.GetComponent<PlayerController>().numberOfCoins = GameManager.gameData.coinCount;

			if (scene.buildIndex == 4) { // If it's the cave, then put a lamp on his hip
				GameObject replacementLamp = Instantiate (lampPrefab);
				replacementLamp.transform.parent = replacementPlayer.transform;
				replacementLamp.transform.localPosition = lampPrefab.transform.position;
			}

			if (GameManager.gameData.playerSceneID == scene.buildIndex) {
				Vector3 replacementSpawnPos = new Vector3 (
	                GameManager.gameData.playerSpawnPos [0],
	                GameManager.gameData.playerSpawnPos [1],
					GameManager.gameData.playerSpawnPos [2]);

				replacementPlayer.transform.position = replacementSpawnPos;
			} else {
				replacementPlayer.transform.position = currentPlayer.transform.position;
			}

			Destroy (currentPlayer);
			GameManager.loadingGame = false;
		}

		// Unlock the level in the unlocked level list & save
		if (scene.buildIndex != 6) {
			GameManager.gameData.unlockedLevels [scene.buildIndex - 1] = true;
			//GameManager.SaveGame ();
		}
	}

}
