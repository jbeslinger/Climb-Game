using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCreditsMusic : MonoBehaviour {

	// Components
	private AudioSource myAudioSource;

	private void Start () {
		try {
			myAudioSource = GetComponent<AudioSource> ();
		} catch {
			Debug.Log ("This GameObject needs an AudioSource to work with this script.");
		}
	}

	private void PlayAudio () {
		myAudioSource.Play ();
	}

}
