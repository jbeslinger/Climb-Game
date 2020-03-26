using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckInputField : MonoBehaviour {

	public InputField inputField;
	public Button thisButton;

	private void Update () {
		if (inputField.text == "") {
			thisButton.interactable = false;
		} else {
			thisButton.interactable = true;
		}
	}

}
