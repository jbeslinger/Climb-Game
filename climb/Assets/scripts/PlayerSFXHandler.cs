using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSFXHandler : MonoBehaviour {
	
    public AudioSource myAudioSource1, myAudioSource2; // The first one is used for non-looping clips, the second one for looping clips (i.e. walking)

    public bool jumpFlag, landFlag, runFlag, walkFlag, dieFlag = false;
    public AudioClip jump1, jump2, move, die;
    public float walkPitchModifier = 1.25f;
    public float runPitchModifier = 1.50f;

    // Update is called once per frame
    private void Update () {
		if (jumpFlag) {
			switch (Random.Range (0, 2)) {
			case 0:
				myAudioSource1.clip = jump1;
				myAudioSource1.Play ();
				break;
			case 1:
				myAudioSource1.clip = jump2;
				myAudioSource1.Play ();
				break;
			}
			jumpFlag = false;
		}

		if (walkFlag || runFlag) {
			myAudioSource2.clip = move;
			if (walkFlag) {
				myAudioSource2.pitch = walkPitchModifier;
			} else if (runFlag) {
				myAudioSource2.pitch = runPitchModifier;
			}

			if (!myAudioSource2.isPlaying && !myAudioSource1.isPlaying) {
				myAudioSource2.Play ();
			}
		} else if (myAudioSource1.isPlaying || !(walkFlag || runFlag)) {
			myAudioSource2.Pause ();
		}

		if (dieFlag) {
			myAudioSource1.clip = die;
			myAudioSource1.Play ();
			dieFlag = false;
		}
    }

}
