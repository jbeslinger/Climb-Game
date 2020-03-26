/* This script handles all of the state changes
 * for the title screen sprites according to the time of day
 * and other random factors */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitlescrnTimeHandler : MonoBehaviour {

	// Enums
    public enum DayState { MORNING, DAY, NOON, RAIN, NIGHT };
    
	// Public Variables
	public DayState currentState; // Lets the script know specfically what state the title is in
	public Image skyImage;
    public Sprite morningSprite, daySprite, noonSprite, rainSprite, nightSprite; // The user-defined sprites for the sky
    public float secsUntilTimecheck = 60f; // Seconds until the script checks the time

	// Private Variables
    private int currentHour; // The current hour the local system sees
    private bool timeCheckFlag = true; // Stops the Coroutine from running every frame
    private bool changeStateFlag = true; // A flag to control DayState changes
    private DayState proposedState; // Used to compare & see if we're transitioning to a new state

    // Adjust the sprites of the menu items according to the hour of day
    private void Start () {
		// Try to find the sprite renderers of the logo & sky sprites
		try {
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
		// First, disable all the sprite renderers for each dynamic object on the screen
		SpriteRenderer[] all_dyn_sprites = GameObject.Find ("dynamic_sprites").GetComponentsInChildren<SpriteRenderer> ();
		for (int i = 0; i < all_dyn_sprites.Length; i++) {
			all_dyn_sprites [i].enabled = false;
		}

		// Then, enable them according to state
		switch (state) {
		case DayState.MORNING:
			skyImage.sprite = morningSprite;
			changeStateFlag = false;
			break;

		case DayState.DAY:
			all_dyn_sprites = GameObject.FindGameObjectWithTag ("DaySprites").GetComponentsInChildren<SpriteRenderer> ();
			for (int i = 0; i < all_dyn_sprites.Length; i++) {
				all_dyn_sprites [i].enabled = true;
			}

			skyImage.sprite = daySprite;
			changeStateFlag = false;
			break;

		case DayState.NOON:
			skyImage.sprite = noonSprite;
			changeStateFlag = false;
			break;

		case DayState.NIGHT:
			all_dyn_sprites = GameObject.FindGameObjectWithTag ("NightSprites").GetComponentsInChildren<SpriteRenderer> ();
			for (int i = 0; i < all_dyn_sprites.Length; i++) {
				all_dyn_sprites [i].enabled = true;
			}

			skyImage.sprite = nightSprite;
			changeStateFlag = false;
			break;
		}
    }

    // Check the time every specified number of seconds
    // then update the current day state
	private IEnumerator CheckTimeForSeconds (float seconds) {
			/*
        // If it's currently raining, skip the time check
        if (raining)
        {
            yield return null;
        }
        else
        {
            // Run a random check to see if the rain event should be triggered
            // 5% chance to rain every minute
            if (Mathf.Round(Random.Range(0f, 200)) == 69f)
            {
                StartCoroutine(RainEvent());
                yield return null;
            }
        }
        */

		timeCheckFlag = false;
		yield return new WaitForSecondsUnpaused (seconds);
		currentHour = System.DateTime.Now.Hour;

		proposedState = ParseHours (currentHour);

		timeCheckFlag = true;
		changeStateFlag = true;
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

    /*
    IEnumerator RainEvent()
    {
        raining = true;
        currentState = DayState.RAIN;
        yield return new WaitForSecondsUnpaused(60 * 60); // Wait for an hour
        raining = false;
    }
    */

}
