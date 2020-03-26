using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomInputModule : MonoBehaviour {

	// Private Vars
	EventSystem myEventSystem;
	private bool delayActive = true;
	private Selectable currentObject= null;

	private void OnEnable () {
		myEventSystem = GetComponent<EventSystem> ();
	}

	private void Update () {
		if (myEventSystem.currentSelectedGameObject == null) {
			return;
		}

		currentObject = myEventSystem.currentSelectedGameObject.GetComponent<Selectable> ();

		if (InputManager.SelectButtonDown ()) {
			if (currentObject.GetComponent<Button> () == null)
				return;
			Button button = currentObject.GetComponent<Button> ();
			ExecuteEvents.Execute(button.gameObject, new BaseEventData(myEventSystem), ExecuteEvents.submitHandler);
		}

		if (InputManager.UpButtonDown ()) {
			if (currentObject.FindSelectableOnUp () == null)
				return;
			GameObject nextObj = currentObject.FindSelectableOnUp ().gameObject;
			myEventSystem.SetSelectedGameObject (nextObj);
			Debug.Log("Navigate up");
		}
		if (InputManager.DownButtonDown ()) {
			if (currentObject.FindSelectableOnDown () == null)
				return;
			GameObject nextObj = currentObject.GetComponent<Selectable> ().FindSelectableOnDown ().gameObject;
			myEventSystem.SetSelectedGameObject (nextObj);
				Debug.Log("Navigate down");
		}
		if (InputManager.LeftButtonDown ()) {
			if (currentObject.FindSelectableOnLeft () == null)
				return;
			GameObject nextObj = currentObject.FindSelectableOnLeft ().gameObject;
			myEventSystem.SetSelectedGameObject (nextObj);
			Debug.Log("Navigate left");
		}
		if (InputManager.RightButtonDown ()) {
			if (currentObject.FindSelectableOnRight () == null)
				return;
			GameObject nextObj = currentObject.FindSelectableOnRight ().gameObject;
			myEventSystem.SetSelectedGameObject (nextObj);
			Debug.Log("Navigate right");
		}
	}

}
