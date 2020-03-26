using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitchBehavior : MonoBehaviour {

	// Public Variables
	public GameObject lightGroup; // The parent GameObject that contains every light source to be toggled
	public Sprite activeSprite; // The sprite that displays when the player touches the door

	// Private Variables
	private bool active;
	private bool readyToActivate = true;
	private Sprite inactiveSprite; // The sprite that's displayed when the player isn't touching the door

	// Components
	private SpriteRenderer mySpriteRenderer;
	private AudioSource myAudioSource; 

	private void Start () {
		// Grab the component
		try {
			mySpriteRenderer = GetComponent<SpriteRenderer> ();
			myAudioSource = GetComponent<AudioSource> ();
			inactiveSprite = mySpriteRenderer.sprite;
		} catch {
			Debug.Log ("ERROR: There is no SpriteRenderer component attached to this object.");
		}
	}

	private void Update () {
		if (!InputManager.UpButton()) {
			readyToActivate = true;
		}

		if (active) {
			mySpriteRenderer.sprite = activeSprite;
		} else {
			mySpriteRenderer.sprite = inactiveSprite;
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

	public void ToggleLights () {
		if (!readyToActivate) {
			return;
		}

		myAudioSource.Play ();

		try {
			foreach (Light light in lightGroup.GetComponentsInChildren<Light> ()) {
				if (light.enabled) {
					light.enabled = false;
				} else {
					light.enabled = true;
				}
			}
		} catch {
			Debug.Log ("No Light Group was assigned");
		}

		readyToActivate = false;
	}

}
