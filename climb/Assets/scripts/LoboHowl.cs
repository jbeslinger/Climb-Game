using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoboHowl : MonoBehaviour {

	private Animator myAnimator;

	private void Start () {
		try {
			myAnimator = GetComponent<Animator> ();
		} catch {
			Debug.Log ("This object needs to have an animator.");
		}
	}

	private void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.CompareTag ("Player")) {
			myAnimator.SetBool ("Looking", true);
		}
	}

	private void OnTriggerExit2D (Collider2D other) {
		if (other.gameObject.CompareTag ("Player")) {
			myAnimator.SetBool ("Looking", false);
		}
	}

}
