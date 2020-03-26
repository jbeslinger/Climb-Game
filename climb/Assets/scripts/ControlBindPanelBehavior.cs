using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ControlBindPanelBehavior : MonoBehaviour {

	// PUBLIC VARS
	[HideInInspector] public string refInputName; // The string reference to access the InputManager Dictionary of GameInputs
	public GameObject setControlsPanel; // The panel that came before this

	// PRIVATE VARS
	private float timerLength = 15f;
	private Text lblTimerNumber;
	private float timer;

	private void OnEnable () {
		refInputName = transform.Find ("Label-InputName").GetComponent<Text> ().text;
		lblTimerNumber = transform.Find ("Label-TimerNumber").GetComponent<Text> (); // Hold onto a reference for the Text component of the timer
		timer = timerLength;
	}

	private void Update () {
		// Run timer until time runs out
		if (timer >= 1) {
			timer -= Time.deltaTime;
		} else { // If time runs out prematurely, then disable the panel
			gameObject.SetActive (false);
			return;
		}

		lblTimerNumber.text = timer.ToString ("##"); // Write the float to the label and turn it into a whole number

		// Else if the player presses Backspace, then SetActive (False) the menu
		if (Input.GetKeyDown (KeyCode.Backspace)) {
			gameObject.SetActive (false);
			return;
		}

		// If player presses any key/button/axis, then set it in player prefs and SetActive (False) the menu
		// Check for any key
		foreach (KeyCode key in Enum.GetValues (typeof (KeyCode))) {
			if (Input.GetKeyDown (key)) {
				if (key.ToString ().Contains ("Mouse")) {
					continue;
				}
				InputManager.inputs [refInputName] = new GameInput ("key", key.ToString (), false); // Create and assign a new GameInput with the key that was pressed
				InputManager.SaveControls ();
				gameObject.SetActive (false);
				return;
			}
		}
		// Check for any of the 20 joystick buttons
		for (int i = 0; i < 20; i++) {
			if (Input.GetButton ("joystick button " + i)) {
				InputManager.inputs [refInputName] = new GameInput ("button", "joystick button " + i, false);
				InputManager.SaveControls ();
				gameObject.SetActive (false);
				return;
			}
		}
		// Check for any of the 28 joystick axes
		for (int i = 0; i < 28; i++) {
			if (Input.GetAxis ("joystick axis " + i) != 0f) {
				InputManager.inputs [refInputName] = new GameInput ("axis", "joystick axis " + i,
					(Input.GetAxis ("joystick axis " + i) > 0 ? false : true));
				InputManager.SaveControls ();
				gameObject.SetActive (false);
				return;
			}
		}
	}

	private void OnDisable () {
		// Reset the timer
		timer = timerLength;
		setControlsPanel.SetActive (true);
	}
		
}
