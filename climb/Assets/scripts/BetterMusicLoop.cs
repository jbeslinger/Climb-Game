/* The main purpose of this script is to find the timeSample
 * in a piece of music where it should loop, play the intro, &
 * loop the rest of the piece. */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterMusicLoop : MonoBehaviour {

	// Public Variables
	public int loopStart = 0; // In PCM samples, if greater than 0, the track is presumed to have an intro
	public int loopEnd = 0; // In PCM samples, used to cut silence off the end of a track

	// Private Variables
	private AudioSource myAudioSource;

	// Components
	private bool hasIntro = false; // Determines whether or not to loop audioClip from loopStart after playing from 0 first
	private bool pastIntro = false;

    // Use this for initialization
    private void Start () {
		try {
			myAudioSource = GetComponent<AudioSource> ();
			if (loopStart > 0) {
				hasIntro = true;
			}
			if (loopEnd == 0) {
				loopEnd = myAudioSource.clip.samples;
			}
		} catch {
			Debug.Log ("This script won't work without an AudioSource and an AudioClip");
		}
    }

	private void Update () {
		if (hasIntro) { // Go ahead and play the intro once, then skip it the next time
			if (pastIntro) {
			} else {
				if (myAudioSource.timeSamples >= loopStart) {
					pastIntro = true;
				}
			}
		} else {
			// If the clip doesn't have an Intro to skip, then play the clip like normal
		}

		if (myAudioSource.timeSamples >= loopEnd) {
			myAudioSource.timeSamples = loopStart;
		}
	}

}
