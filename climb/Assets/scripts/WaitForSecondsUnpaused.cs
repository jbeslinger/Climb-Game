/* May I preface with this:  I want an academy award for this bullshit.
 * This script creates a new CustomYieldInstruction that takes
 * into account the GameManager's Pause function.  Basically,
 * it works the exact same way as WaitForSecondsUnpaused (),
 * but it pauses when the game is paused.  Use this instead. */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForSecondsUnpaused : CustomYieldInstruction {

	private float duration;
	private float timer = 0.0f;

	// This method is returning true every frame
	// It stops when it returns false
	public override bool keepWaiting {
		get {
			if (!GameManager.gameIsPaused) { // If the game isn't paused, add to the timer
				timer += Time.deltaTime;
			}

			return timer < duration; // When the timer is up, this will return false
		}
	}

	public WaitForSecondsUnpaused (float seconds) { // How long should I wait?
		duration = seconds; // Gotcha.
		Reset ();
	}

	public new void Reset () {
		timer = 0.0f;
		base.Reset ();
	}

}
