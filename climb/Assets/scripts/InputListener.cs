using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputListener : MonoBehaviour {

	public FurnitureMenuBehavior furnitureMenu;
	public PonchoMenuBehavior ponchoMenu;
	public AudioSource sndPress;
	public EventSystem myEventSystem;

	private bool buttonPress = false;

	private void Update () {
		if (InputManager.LeftButton () && !buttonPress) {
			if (myEventSystem.currentSelectedGameObject != null &&
				myEventSystem.currentSelectedGameObject.name.Contains("Slider-")) {
				return;
			}

			buttonPress = true;
			sndPress.Play ();

			if (furnitureMenu != null) {
				furnitureMenu.ShiftSelection ("left");
			} 
			if (ponchoMenu != null) {
				ponchoMenu.ShiftSelection ("left");
			}
		}
		if (InputManager.RightButton () && !buttonPress) {
			if (myEventSystem.currentSelectedGameObject != null &&
				myEventSystem.currentSelectedGameObject.name.Contains("Slider-")) {
				return;
			}

			buttonPress = true;
			sndPress.Play ();

			if (furnitureMenu != null) {
				furnitureMenu.ShiftSelection ("right");
			} 
			if (ponchoMenu != null) {
				ponchoMenu.ShiftSelection ("right");
			}
		}

		if (!InputManager.LeftButton () && !InputManager.RightButton ()) {
			buttonPress = false;
		}
	}

}
