using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBehavior : MonoBehaviour {

	// Components
	private Animator myAnimator;
	private AudioSource myAudioSource;
	private BoxCollider2D myBoxCollider2D;

	// Public Variables
	public bool collected;

	private void OnEnable () {
		if (GameManager.speedrunning) { // Turn off every coin if the player is speedrunning the level
			gameObject.SetActive (false);
		}
	}

    // Getting all of the Components of the coin
    private void Start () {
		try {
			myAnimator = GetComponent<Animator> ();
			myAudioSource = GetComponent<AudioSource> ();
			myBoxCollider2D = GetComponent<BoxCollider2D> ();
		} catch {
			Debug.Log ("This script demands that the GameObject has an Animator, a coin-collect animation, and an Audio Source.");
		}
    }

    private IEnumerator PlayCollectAnim () {
		float collectAnimDuration = 0f;
		RuntimeAnimatorController myAnimatorController = myAnimator.runtimeAnimatorController;
		foreach (AnimationClip clip in myAnimatorController.animationClips) {
			if (clip.name == "coin_collect") {
				collectAnimDuration = clip.length;
			}
		}

		yield return new WaitForSecondsRealtime (collectAnimDuration); // Wait for as long as the animation plays
	}

	private void OnTriggerEnter2D (Collider2D other) {
		if (other.CompareTag ("Player")) {
			collected = true;
			myAnimator.SetTrigger ("Collected"); // Tell the coin's animator that it's been collected
			myBoxCollider2D.enabled = false;
			StartCoroutine (PlayCollectAnim ());
			myAudioSource.Play ();
		}
	}
}
