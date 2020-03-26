using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitToTitleBehavior : MonoBehaviour {

	public void QuitToTitleScreen () {
		Destroy (GameObject.Find ("GameController"));
		GameManager.speedrunning = false;
		GameManager.gameIsPaused = false;
		GameManager.gameData = null;
		SceneManager.LoadScene (0);
	}

}
