using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PonchoMenuBehavior : MonoBehaviour {

	// Public Variables
	public GameObject [] ponchoPrefabs = new GameObject [7]; // This array holds all of the poncho prefabs in order (According to the unlockedCostumes array in gameData
	public Sprite [] ponchoImages = new Sprite [8];
	public Image imgPonchoIcon;
	public Text txtPonchoName, txtPonchoDesc; // The UI Text objects
	public Button btnChange, btnBack;
	public GameObject rbgPanel; // The rgb adjustment panel
	public EventSystem myEventSystem;

	// Private Variables
	private int currentPonchoIndex = 0; // The current index of the poncho we're looking at on the panel
	private string [,] ponchoDictionary = new string [7,2]; // [x,0] = name, [x,1] = description

	private void Start () {
		// Set all of the ponchoDictionary 2D array information from the Texts file
		ponchoDictionary [0, 0] = LocalizationManager.localizedDictionary ["norm_poncho"];
		ponchoDictionary [0, 1] = LocalizationManager.localizedDictionary ["norm_poncho_desc"];

		ponchoDictionary [1, 0] = LocalizationManager.localizedDictionary ["spdy_poncho"];
		ponchoDictionary [1, 1] = LocalizationManager.localizedDictionary ["spdy_poncho_desc"];

		ponchoDictionary [2, 0] = LocalizationManager.localizedDictionary ["lght_poncho"];
		ponchoDictionary [2, 1] = LocalizationManager.localizedDictionary ["lght_poncho_desc"];

		ponchoDictionary [3, 0] = LocalizationManager.localizedDictionary ["wool_poncho"];
		ponchoDictionary [3, 1] = LocalizationManager.localizedDictionary ["wool_poncho_desc"];

		ponchoDictionary [4, 0] = LocalizationManager.localizedDictionary ["nght_poncho"];
		ponchoDictionary [4, 1] = LocalizationManager.localizedDictionary ["nght_poncho_desc"];

		ponchoDictionary [5, 0] = LocalizationManager.localizedDictionary ["agle_poncho"];
		ponchoDictionary [5, 1] = LocalizationManager.localizedDictionary ["agle_poncho_desc"];

		ponchoDictionary [6, 0] = LocalizationManager.localizedDictionary ["mlti_poncho"];
		ponchoDictionary [6, 1] = LocalizationManager.localizedDictionary ["mlti_poncho_desc"];

		txtPonchoName.text = ponchoDictionary [currentPonchoIndex, 0]; // Set the UI text Name
		txtPonchoDesc.text = ponchoDictionary [currentPonchoIndex, 1]; // Set the UI text Description
		imgPonchoIcon.sprite = ponchoImages [currentPonchoIndex]; // Set the UI image Icon

		if (btnChange.IsInteractable ()) {
			myEventSystem.SetSelectedGameObject (btnChange.gameObject);
		} else {
			myEventSystem.SetSelectedGameObject (btnBack.gameObject);
		}
	}

	// This method is called by the 'Change' button
	public void Change () {
		// Store the relevant player information into several temporary variables
		// (Player position, number of coins, etc.)
		// Then destroy the current player & replace him with the new costume prefab

		GameManager.gameData.playerCostume = currentPonchoIndex;
		GameObject currentPlayer = GameObject.FindGameObjectWithTag ("Player");
		Vector3 currentPlayerPosition = currentPlayer.transform.position;
		Color currentPlayerColor = currentPlayer.GetComponent<SpriteRenderer> ().color;
		Destroy (currentPlayer);
		GameObject replacementPlayer = Instantiate (ponchoPrefabs [GameManager.gameData.playerCostume]);
		replacementPlayer.transform.position = currentPlayerPosition;
		replacementPlayer.GetComponent<SpriteRenderer> ().color = currentPlayerColor;
		replacementPlayer.GetComponent<PlayerController>().numberOfCoins = GameManager.gameData.coinCount;
		GameManager.SaveGame (); // Then save!
		GameManager.ResumeGame ();
		ShiftSelection ("left");
		ShiftSelection ("right");
	}

	// This method is called by the Left and Right arrow buttons
	public void ShiftSelection (string direction) {
		// Move the selection according to the string 'left' & 'right'
		// If the poncho is unlocked, then update the image & descriptions
		// If it's not unlocked, then change the texts to '???'
		// If it's the array index for the multicolor poncho, then enable the RGB panel
		switch (direction) {
		case "left":
			if (currentPonchoIndex - 1 < 0) { // If 1 less the index makes an Out of Bounds exception, loop to the top of the array
				currentPonchoIndex = 6;
			} else {
				currentPonchoIndex -= 1;
			}
			break;
		case "right":
			if (currentPonchoIndex + 1 > 6) { // If 1 plus the index makes an Out of Bounds exception, loop to the bottom of the array
				currentPonchoIndex = 0;
			} else {
				currentPonchoIndex += 1;
			}
			break;
		default:
			break;
		}

		if (GameManager.gameData.unlockedCostumes [currentPonchoIndex]) { // If the costume is unlocked, then set the ui elements to the correct info
			txtPonchoName.text = ponchoDictionary [currentPonchoIndex, 0]; // Set the UI text Name
			txtPonchoDesc.text = ponchoDictionary [currentPonchoIndex, 1]; // Set the UI text Description
			imgPonchoIcon.sprite = ponchoImages [currentPonchoIndex]; // Set the UI image Icon
			btnChange.interactable = true;
		} else { // If the costume isn't, then set the ui elements to '???' & disable the Change button
			txtPonchoName.text = "???"; // Set the UI text Name
			txtPonchoDesc.text = "???"; // Set the UI text Description
			imgPonchoIcon.sprite = ponchoImages [7]; // Set the UI image Icon
			btnChange.interactable = false;
		}

		if (btnChange.IsInteractable ()) {
			myEventSystem.SetSelectedGameObject (btnChange.gameObject);
		} else {
			myEventSystem.SetSelectedGameObject (btnBack.gameObject);
		}

		// If we're looking at the Multicolor poncho, open up the rgb settings panel
		if (currentPonchoIndex == 6) {
			rbgPanel.SetActive (true);
		} else {
			rbgPanel.SetActive (false);
		}
	}

}
