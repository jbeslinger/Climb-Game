/* Basically, this script is attached to an
 * object with a BoxCollider2D trigger.
 * It scans the scene for GameObjects with SpriteRenderers
 * and subtracts a certain amount of Green & Blue
 * to simulate a sunset. This will be placed on several
 * triggers throughout World 2. */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunsetTransition : MonoBehaviour {

	// Public Variables
	public float colorDifference = 0.1f; // The amount to be subtracted from G & B
	public bool trigger = false; // Used to trigger this event

	// Private Variables
	private static SpriteRenderer[] objectsWithSprites = null;
	private GameObject player;

	// When the program stars, scan every GameObject in the scene and
	// if the object has a SpriteRenderer component, add it to the
	// array of objectsWithSprites.  If all of this has been done once before,
	// then skip all of this code.
	private void Awake () {
		if (objectsWithSprites != null) {
			return;
		}

		objectsWithSprites = FindObjectsOfType<SpriteRenderer> ();
	}

	private void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	// Wait every frame for a trigger to occur, then call the
	// SubtractColor() function, then set the gameObject to inactive
	// so it doesn't get triggered again
	private void Update () {
		if (player == null) {
			player = GameObject.FindGameObjectWithTag ("Player");
		}

		// If the player passes by this object, it's triggered (lol)
		if (player.transform.position.x < transform.position.x) {
			trigger = true;
		}

		// Once triggered, it subtracts the color from all objects with Sprites in the scene
		if (trigger) {
			SubtractColor ();
			transform.gameObject.SetActive (false);
		}
	}

	// Go through all of the sprites in the scene and adjust
	// the Green & Blue values of their color attributes
	// to make the scene darker and redder
	private void SubtractColor () {
		foreach (SpriteRenderer sr in objectsWithSprites) {
			if (sr != null && !sr.gameObject.name.Contains("sky")) {
				sr.color = new Color (1f,
					(sr.color.g - Mathf.Abs(colorDifference)),
					(sr.color.b - Mathf.Abs(colorDifference)));
			}
		}
	}

}
