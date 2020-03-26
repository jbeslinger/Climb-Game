using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemBehavior : MonoBehaviour {
    
	// Components
	private Animator myAnimator;
	private AudioSource myAudioSource;

	// Public Variables
	public int gemID;
	public bool collected = false;

	// Private Variables
	private GameObject gemText;

	private void OnEnable () {
		if (GameManager.speedrunning) { // Turn off every gem if the player is speedrunning the level
			gameObject.SetActive (false);
		}
	}

    // Getting all of the Components of the gem
    private void Start () {
		myAnimator = GetComponent<Animator> ();
		myAudioSource = GetComponent<AudioSource> ();
		gemText = this.gameObject.transform.GetChild (0).gameObject;

    }

    private void Update () {
		if (collected) {
			myAnimator.SetBool ("Collected", true); // Tell the coin's animator that it's been collected
			gemText.GetComponent<Animator>().enabled = true; // Set off the gemText animation
		}
    }

	public void PlayCollectNoise () {
		myAudioSource.Play ();
	}

}
