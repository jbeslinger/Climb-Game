using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedrunPlaqueBehavior : MonoBehaviour {

	public GameObject speedrunRecordPanel;
	public Sprite activeSprite; // The sprite that displays when the player touches the object

	private bool active;
	private Sprite inactiveSprite; // The sprite that's displayed when the player isn't touching the object
	private SpriteRenderer mySpriteRenderer;

	private void Awake () {
		mySpriteRenderer = GetComponent<SpriteRenderer> ();
		inactiveSprite = mySpriteRenderer.sprite;
	}

	private void Update () {
		if (active && InputManager.UpButton ()) {
			Activate ();
		}

		if (active) {
			mySpriteRenderer.sprite = activeSprite;
		} else {
			mySpriteRenderer.sprite = inactiveSprite;
		}

		gameObject.SetActive (GameManager.gameData.playerHasBeatenGame);
	}

	private void OnTriggerEnter2D (Collider2D other) {
		if (other.CompareTag ("Player")) {
			active = true;
		}
	}

	private void OnTriggerExit2D (Collider2D other) {
		if (other.CompareTag ("Player")) {
			active = false;
		}
	}

	// This is called when the player presses up on the keyboard
	public void Activate () {
		GameManager.navigatingFurnitureMenu = true;
		GameManager.PauseGame ();
		speedrunRecordPanel.SetActive (true);
	}

	public void Deactivate () {
		GameManager.navigatingFurnitureMenu = false;
		GameManager.ResumeGame ();
		speedrunRecordPanel.SetActive (false);
	}

}
