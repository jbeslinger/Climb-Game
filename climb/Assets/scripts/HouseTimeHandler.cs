using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseTimeHandler : MonoBehaviour {

	// Enums
	public enum DayState { MORNING, DAY, NOON, RAIN, NIGHT };

	// Public Variables
	public DayState currentState; // Lets the script know specfically what state the title is in
	public Sprite morningSprite, daySprite, noonSprite, nightSprite; // The user-defined sprites for the sky
	public float secsUntilTimecheck = 1f; // Seconds until the script checks the time
	public Light houseAmbiantLight; // The Light component of the house to adjust

	// Private Variables
	private int currentHour; // The current hour the local system sees
	private bool timeCheckFlag = true; // Stops the Coroutine from running every frame
	private bool changeStateFlag = true; // A flag to control DayState changes
	private DayState proposedState; // Used to compare & see if we're transitioning to a new state
	private SpriteRenderer mySpriteRenderer;

	// Adjust the house according to the hour of day
	private void Start () {
		try {
			mySpriteRenderer = GetComponent<SpriteRenderer> ();
			currentHour = System.DateTime.Now.Hour;
			currentState = ParseHours (currentHour);
			ChangeDayState (currentState);
		} catch {
		}
	}

	private void Update () {	
		// See if it's time to update the time again
		if (timeCheckFlag) {
			StartCoroutine (CheckTimeForSeconds (secsUntilTimecheck));
		}

		// If the new, upcoming state is different, then transition to the new one
		if (changeStateFlag) {
			if (proposedState != currentState) {
				currentState = proposedState;
				ChangeDayState (currentState);
			}
		}
	}

	private void ChangeDayState (DayState state) {
		switch (state) {
		case DayState.MORNING:
			houseAmbiantLight.intensity = 15f;
			mySpriteRenderer.sprite = morningSprite;
			changeStateFlag = false;
			break;

		case DayState.DAY:
			houseAmbiantLight.intensity = 25f;
			mySpriteRenderer.sprite = daySprite;
			changeStateFlag = false;
			break;

		case DayState.NOON:
			houseAmbiantLight.intensity = 15f;
			mySpriteRenderer.sprite = noonSprite;
			changeStateFlag = false;
			break;

		case DayState.NIGHT:
			houseAmbiantLight.intensity = 5f;
			mySpriteRenderer.sprite = nightSprite;
			changeStateFlag = false;
			break;
		}
	}

	private DayState ParseHours (int hours) {
		// Between 6am and 9am : Morning
		if (hours >= 6 && hours < 9) {
			return DayState.MORNING;
		}
		// Between 9am & 6pm : Day
		else if (hours >= 9 && hours < 18) {
			return DayState.DAY;
		}
		// Between 6pm & 9pm : Noon
		else if (hours >= 18 && hours < 21) {
			return DayState.NOON;
		}
		// Between 9pm & 6am : Night
		else if ((hours >= 21 && hours < 24) || (hours >= 0 && hours < 6)) {
			return DayState.NIGHT;
		} else {
			return DayState.DAY;
		}
	}

	// Check the time every specified number of seconds
	// then update the current day state
	private IEnumerator CheckTimeForSeconds (float seconds) {
		timeCheckFlag = false;
		yield return new WaitForSecondsUnpaused (seconds);
		currentHour = System.DateTime.Now.Hour;

		proposedState = ParseHours (currentHour);

		timeCheckFlag = true;
		changeStateFlag = true;
	}

}
