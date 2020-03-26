using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairBehavior : MonoBehaviour {

	// Public Variables
	public bool active;
	public Sprite activeSprite; // The sprite that displays when the player touches the door

	// Private Variables
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

}
