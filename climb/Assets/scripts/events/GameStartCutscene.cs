using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartCutscene : MonoBehaviour {

	// Public Variables
	public AnimationClip openingCutscene;
	public GameObject cactus;
	public Sprite cactusWithoutFlower;
	public PauseMenuBehavior pauseMenu;
	public GameObject cutsceneCam;
	public bool triggered = false;
	public AudioSource walkSound, pickupSound;

	// Private Variables
	private GameObject player;
	private SpriteRenderer mySpriteRenderer;
	private Animator myAnimator;

	private void Awake () {
		triggered = GameManager.gameData.startGameCutsceneTriggered;
	}

	private void Start () {
		try {
			mySpriteRenderer = GetComponent<SpriteRenderer> ();
			myAnimator = GetComponent<Animator> ();
			player = GameObject.FindGameObjectWithTag ("Player");
		} catch {
			Debug.Log ("This object doesn't have a SpriteRenderer, Animator, or AudioSource.");
		}

		if (!triggered) {
			player.GetComponent<SpriteRenderer> ().enabled = false;
			mySpriteRenderer.enabled = true;

			StartCoroutine (InputManager.BlockInput (openingCutscene.length));
			StartCoroutine (ReactivatePlayerSprite ());
			StartCoroutine (ToggleCutsceneState ());
			StartCoroutine (PlayCutscene ());
		} else {
			cutsceneCam.GetComponent<Animator> ().enabled = false;
			cutsceneCam.GetComponent<Camera> ().enabled = false;
			cactus.GetComponent<SpriteRenderer> ().sprite = cactusWithoutFlower;
			mySpriteRenderer.enabled = false;
			myAnimator.enabled = false;
		}
	}


	private IEnumerator PlayCutscene () {
		cactus.GetComponent<Animator> ().enabled = true;
		yield return new WaitForSecondsUnpaused (openingCutscene.length); // Wait for as long as the animation plays
		cactus.GetComponent<Animator> ().enabled = false;
		myAnimator.enabled = false;
		mySpriteRenderer.enabled = false;
		pauseMenu.cutsceneIsPlaying = false;
		triggered = true;
	}

	// This is only to prevent the 'flicker' that occurs when the player
	// and the cutscene swap places
	private IEnumerator ReactivatePlayerSprite () {
		yield return new WaitForSecondsUnpaused (openingCutscene.length - .05f);
		player.GetComponent<SpriteRenderer> ().enabled = true;
		cutsceneCam.GetComponent<Animator> ().enabled = false;
	}

	// This is a stupid hotfix for the start of the game
	// Basically, it waits for just long enough for the FadeIn animation to play and turn the cutsceneIsPlaying bool to false
	// Then it turns it back immediately.
	private IEnumerator ToggleCutsceneState () {
		yield return new WaitForSecondsUnpaused (1.01f);
		pauseMenu.cutsceneIsPlaying = true;
	}

	private void PlayWalkAudio () {
		walkSound.Play ();
	}

	private void PlayPickupAudio () {
		pickupSound.Play ();
	}

	private void StopWalkAudio () {
		walkSound.Stop ();
	}

}
