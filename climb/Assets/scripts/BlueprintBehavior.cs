using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlueprintBehavior : MonoBehaviour {

	// Public Variables
	public Sprite activeSprite; // The sprite that displays when the player touches the door
	public GameObject blueprintMenu;
	public GameObject pausePanel;

	// Private Variables
	private bool active;
	private SpriteRenderer mySR; // This object's SpriteRenderer component
	private Sprite inactiveSprite; // The sprite that's displayed when the player isn't touching the door

	private void Start () {
		// Grab the component
		try {
			mySR = GetComponent<SpriteRenderer> ();
			inactiveSprite = mySR.sprite;
		} catch {
			Debug.Log ("ERROR: There is no SpriteRenderer component attached to this object.");
		}
	}

	private void Update () {
		if (active) {
			mySR.sprite = activeSprite;
			if (InputManager.UpButton () && !GameManager.navigatingFurnitureMenu) {
				Activate ();
			}
		} else {
			mySR.sprite = inactiveSprite;
		}
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
		pausePanel.SetActive (false);
		blueprintMenu.SetActive (true);
	}

	public void Deactivate () {
		GameManager.ResumeGame ();
		blueprintMenu.SetActive (false);
		GameManager.navigatingFurnitureMenu = false;
	}

}
