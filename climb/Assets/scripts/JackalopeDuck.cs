/* This script sets the state of the Animator
 * component of the Sitting Jackalope GameObject.
 * It's intended to make the animal duck when it's
 * about to bonk his head on a rock while moving along. */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JackalopeDuck : MonoBehaviour {

	// Components
	private Animator myAnimator;

	// Start by grabbing the Animator component
	private void Start () {
		try {
			myAnimator = GetComponent<Animator> ();
		} catch { 
			Debug.Log ("This GameObject needs to have an Animator for this script to work.");
		}
	}

	// If the Jackalope enters the trigger, tell the Animator he's ducking
	private void OnTriggerEnter2D (Collider2D other) {
		if (other.CompareTag ("JackalopeDuckingTrigger")) {
			myAnimator.SetBool ("Ducking", true);
		}
		if (other.CompareTag ("Death")) {
			myAnimator.SetTrigger ("Jumping");
		}
	}

	// If the Jackalope exits the trigger, tell the Animator he's not ducking
	private void OnTriggerExit2D (Collider2D other) {
		if (other.CompareTag ("JackalopeDuckingTrigger")) {
			myAnimator.SetBool ("Ducking", false);
		}
	}

}
