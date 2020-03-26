/* This script checks for input from
 * keyboard or controller and moves the cursor. */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectOnInput : MonoBehaviour {
	// Public Variables
    public EventSystem myEventSystem;
    public GameObject selectedObj;

	// Private Variables
	private string vertAxis;
	private bool buttonSelected;

	private void OnEnable () {
		myEventSystem.SetSelectedGameObject (null);
		vertAxis = myEventSystem.GetComponent<StandaloneInputModule> ().verticalAxis;
	}

    private void Update () {
		if (myEventSystem.currentSelectedGameObject == null) {
			buttonSelected = false;
		}

		try {
			// If there's any vertical input from the player, then buttonSelected is true
			if (Input.GetButtonDown (vertAxis) && buttonSelected == false) {
				if (myEventSystem.currentSelectedGameObject == null) {
					myEventSystem.SetSelectedGameObject (selectedObj);
				}

				buttonSelected = true;
			}
		} catch {
		}
    }

    private void OnDisable () {
        buttonSelected = false;
    }
}
