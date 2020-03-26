/* The point of this script is to make sure the
 * button indicator appears above the door to tell
 * the player to press 'Up' to activate it. */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehavior : MonoBehaviour {

	// Public Variables
	public Sprite keyboardSprite, xboxSprite;
	public bool active; // This should be true when the player touches the Trigger BoxCollider2D
	public int sceneID = 0; // This is the scene that this door loads when it's triggered

	// Private Variables
	private SpriteRenderer mySR; // This object's SpriteRenderer component
	private Sprite activeSprite; // The sprite that displays when the player touches the door
	private Sprite inactiveSprite; // The sprite that's displayed when the player isn't touching the door

	private void Start () {
		// Grab the component
		try {
			mySR = GetComponent<SpriteRenderer> ();
			inactiveSprite = mySR.sprite;
		} catch {
			Debug.Log ("ERROR: There is no SpriteRenderer component attached to this object.");
		}

		try {
			if (Input.GetJoystickNames ()[0].Contains ("Xbox")) {
				activeSprite = xboxSprite;
			} else {
				activeSprite = keyboardSprite;
			}
		} catch {
			activeSprite = keyboardSprite;
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
		if (other.gameObject.CompareTag ("Player")) {
			active = true;
		}
	}

	private void OnTriggerExit2D (Collider2D other) {
		if (other.gameObject.CompareTag ("Player")) {
			active = false;
		}
	}

}
