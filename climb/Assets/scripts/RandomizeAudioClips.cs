/* All this does is sort through a
 *  list of sounds and picks one */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeAudioClips : MonoBehaviour {
	
    public AudioClip[] clips; // Use this array to hold each of the clips you'd like to randomly sort through
    private AudioSource myAudioSource; // This is the GameObject's AudioSource component

    private void Start () {
		// Don't forget to instantiate the array size & element data in the editor
		// or you'll get an error

		try {
			myAudioSource = GetComponent<AudioSource> ();
		} catch {
			Debug.Log ("No AudioSource component is attached to this GameObject.");
		}
    }

    public void PlayRandomClip () {
		// Randomly pick a new clip from the array of clips
		myAudioSource.clip = clips [Mathf.RoundToInt (Random.Range (0f, clips.Length - 1))];
		myAudioSource.Play ();
    }

}
