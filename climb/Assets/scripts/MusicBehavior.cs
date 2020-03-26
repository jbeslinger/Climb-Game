using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBehavior : MonoBehaviour
{
	
    private AudioSource myAudioSource;
    public AudioClip intro, loop;

    // Use this for initialization
    private void Start () {
		try {
			myAudioSource = GetComponent<AudioSource> ();
			myAudioSource.clip = intro;
		} catch {
			Debug.Log ("The script demands that an audio source is attached to this object & Intro + Loop audio clips are specified in the editor.");
		}

		StartCoroutine (playMusic ());
    }

    private IEnumerator playMusic () {
		if (intro != null) {
			myAudioSource.Play ();
			yield return new WaitForSeconds (intro.length);
		}

		myAudioSource.clip = loop;
		myAudioSource.Play ();
		myAudioSource.loop = true;
    }

}
