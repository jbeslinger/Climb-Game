using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartKeyListener : MonoBehaviour {

	// Public Variables
	public bool listening = false;

	private void Update () {
		if (listening && InputManager.PauseButtonDown ()) {
			GameManager.gameData.playerHasBeatenGame = true;
			GameManager.gameData.playerSceneID = 1;
			GameManager.gameData.playerSpawnPos [0] = -55;
			GameManager.gameData.playerSpawnPos [1] = -20;
			GameManager.gameData.playerSpawnPos [2] = 0;
			GameManager.endOfGameFlag = true;
			GameManager.SaveGame ();
			GameObject.FindObjectOfType<FadeOut> ().FadeToLevel (0);
		}
	}

}
