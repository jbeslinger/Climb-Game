using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockLevelSelect : MonoBehaviour {

	// Public Variables
	public Button [] levelSelectButtons = new Button [6]; // Level select buttons

	// Upon scene load, set buttons active with the player's unlocked levels
	private void Start () {
		levelSelectButtons [0].interactable = GameManager.gameData.unlockedLevels [0];
		levelSelectButtons [1].interactable = GameManager.gameData.unlockedLevels [1];
		levelSelectButtons [2].interactable = GameManager.gameData.unlockedLevels [2];
		levelSelectButtons [3].interactable = GameManager.gameData.unlockedLevels [3];
		levelSelectButtons [4].interactable = GameManager.gameData.unlockedLevels [4];
		levelSelectButtons [5].interactable = GameManager.gameData.playerHasBeatenGame;
	}

	public void LoadLevel (int sceneID) {
		GameManager.loadingGame = true;
		GameObject.FindObjectOfType<FadeOut> ().FadeToLevel (sceneID);
	}

}
