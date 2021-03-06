/* Basically, this script gets ahold of the object's Animator
 * Component, then it waits for something to call the 
 * FadeToLevel function for when it's time to load a new scene,
 * THEN it triggers the scene_fadeout animation which contains
 * an event that calls OnFadeComplete (). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeOut : MonoBehaviour {

	//Components
	private Animator myAnimator;
	private PauseMenuBehavior pauseMenu;

	// Private Variables
	private int levelToLoad;

	private void Start () {
		myAnimator = GetComponent<Animator> ();

		try {
			pauseMenu = GameObject.FindObjectOfType<PauseMenuBehavior> ();
		} catch {
			pauseMenu = null;
		}
	}

	public void FadeToLevel (int levelIndex) {
		GameManager.SaveGame ();
		levelToLoad = levelIndex;
		myAnimator.SetTrigger ("Fade Out");
	}

	public void OnFadeComplete () {
		SceneManager.LoadScene (levelToLoad);
	}

	public void ToggleCutsceneState () {
		if (pauseMenu != null) {
			if (pauseMenu.cutsceneIsPlaying) {
				if (GameManager.newGame) {
					return;
				}
				pauseMenu.cutsceneIsPlaying = false;
			} else {
				pauseMenu.cutsceneIsPlaying = true;
			}
		}
	}

}
