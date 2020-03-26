using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ControlBindButtonBehavior : MonoBehaviour {

	// Public Vars
	public string inputName; // The name of the GameInput this button represents
	public Text lblInputName;
	public GameObject listenerMenu;

	private void OnEnable () {
		GetComponentInChildren<Text> ().text = InputManager.inputs [inputName].ToString ();
	}

	public void OpenMenu () {
		lblInputName.text = inputName;
		transform.parent.transform.parent.gameObject.SetActive (false);
		listenerMenu.SetActive (true);
	}

}
