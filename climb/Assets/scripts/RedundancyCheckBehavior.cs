using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RedundancyCheckBehavior : MonoBehaviour {

	// Public Vars
	public Text[] buttonsToCheck;

	// Private Vars
	private Color standardColor = new Color (1f, 1f, 1f); // This is the standard color for text
	private Color redundancyNotificationColor = new Color (0f, 0f, 1f); // Set the text to this color if they are already present in the list of buttons

	private void OnEnable () {
		foreach (Text t in buttonsToCheck) {
			t.color = standardColor;
		}
	}

	private void Update () {
		for (int i = 0; i < buttonsToCheck.Length; i++) {
			for (int j = i + 1; j < buttonsToCheck.Length; j++) {
				if (buttonsToCheck [i].text == buttonsToCheck [j].text) {
					buttonsToCheck [i].color = redundancyNotificationColor;
					buttonsToCheck [j].color = redundancyNotificationColor;
				}
			}
		}
	}
}
