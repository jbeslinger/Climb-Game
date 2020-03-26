using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FurnitureMenuBehavior : MonoBehaviour {

	// Public Variables
	public GameObject[] furnitures = new GameObject[16];
	public Text txtPlayerCoins;
	public Image imgPurchaseIcon;
	public Text txtFurnitureName, txtPrice;
	public Button btnPurchase, btnBack;
	public GameObject pnlRGBControls;
	public FurnitureProperties currentProduct;
	public EventSystem myEventSystem;

	// Private Variables
	private PlayerController player; // Get a reference to the player's script
	private int currentFurnitureIndex = 0; // The current index of the furniture we're looking at on the panel

	private void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();

		for (int i = 0; i < furnitures.Length; i++) {
			furnitures [i].SetActive (GameManager.gameData.unlockedFurniture [i]);
		}

		// Temp reference to avoid calling GetComponent a dozen times
		FurnitureProperties currentProduct = furnitures [currentFurnitureIndex].GetComponent<FurnitureProperties> ();
		imgPurchaseIcon.sprite = currentProduct.purchaseIcon;
		txtFurnitureName.text = currentProduct.displayName;
		txtPrice.text = "x " + currentProduct.price;

		// If the player doesn't have enough money or if the product is unlocked already, disable the purchase button
		if (GameManager.gameData.unlockedFurniture [currentFurnitureIndex]) {
			btnPurchase.GetComponentInChildren<Text> ().text = LocalizationManager.localizedDictionary ["purchased"]; // The text is changed to 'Purchased'
			btnPurchase.interactable = false;
		} else if (GameManager.gameData.coinCount < currentProduct.price) {
			btnPurchase.GetComponentInChildren<Text> ().text = LocalizationManager.localizedDictionary ["purchase"]; // The text is changed to 'Purchased'
			btnPurchase.interactable = false;
		} else {
			btnPurchase.GetComponentInChildren<Text> ().text = LocalizationManager.localizedDictionary ["purchase"]; // The text is changed to 'Purchased'
			btnPurchase.interactable = true;
		}

		if (btnPurchase.IsInteractable ()) {
			myEventSystem.SetSelectedGameObject (btnPurchase.gameObject);
		} else {
			myEventSystem.SetSelectedGameObject (btnBack.gameObject);
		}
	}

	private void Update () {
		if (player == null) {
			player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();
		}

		txtPlayerCoins.text = LocalizationManager.localizedDictionary ["coins"] + " " + player.numberOfCoins; // Update the player coin total
	}

	// This function is called when the player presses the Purchase button on the furniture menu
	public void PurchaseFurniture () {
		btnPurchase.interactable = false; // The button is now non-interactible
		myEventSystem.SetSelectedGameObject (btnBack.gameObject);
		btnPurchase.GetComponentInChildren<Text> ().text = LocalizationManager.localizedDictionary ["purchased"]; // The text is changed to 'Purchased'
		GameManager.gameData.unlockedFurniture [currentFurnitureIndex] = true; // The gameData instance is updated with the unlocked furniture
		player.numberOfCoins -= (int)furnitures[currentFurnitureIndex].GetComponent<FurnitureProperties>().price; // Subtract the number of coins from the player's total
		furnitures [currentFurnitureIndex].SetActive (true); // Set the recently purchased furniture to active
		GameManager.SaveGame(); // Then save!
	}

	// This is called when the player presses the left or right buttons on the furniture menu
	public void ShiftSelection (string direction) {
		switch (direction) {
		case "left":
			if (currentFurnitureIndex - 1 < 0) { // If 1 less the index makes an Out of Bounds exception, loop to the top of the array
				currentFurnitureIndex = 15;
			} else {
				currentFurnitureIndex -= 1;
			}
			break;
		case "right":
			if (currentFurnitureIndex + 1 > 15) { // If 1 plus the index makes an Out of Bounds exception, loop to the bottom of the array
				currentFurnitureIndex = 0;
			} else {
				currentFurnitureIndex += 1;
			}
			break;
		default:
			break;
		}

		// Temp reference to avoid calling GetComponent a dozen times
		currentProduct = furnitures [currentFurnitureIndex].GetComponent<FurnitureProperties> ();
		imgPurchaseIcon.sprite = currentProduct.purchaseIcon;
		txtFurnitureName.text = currentProduct.displayName;
		txtPrice.text = "x " + currentProduct.price;

		// If the player doesn't have enough money or if the product is unlocked already, disable the purchase button
		if (GameManager.gameData.unlockedFurniture [currentFurnitureIndex]) {
			btnPurchase.GetComponentInChildren<Text> ().text = LocalizationManager.localizedDictionary ["purchased"]; // The text is changed to 'Purchased'
			btnPurchase.interactable = false;
		} else if (player.numberOfCoins < currentProduct.price) {
			btnPurchase.GetComponentInChildren<Text> ().text = LocalizationManager.localizedDictionary ["purchase"];
			btnPurchase.interactable = false;
		} else {
			btnPurchase.GetComponentInChildren<Text> ().text = LocalizationManager.localizedDictionary ["purchase"];
			btnPurchase.interactable = true;
		}

		if (btnPurchase.IsInteractable ()) {
			myEventSystem.SetSelectedGameObject (btnPurchase.gameObject);
		} else {
			myEventSystem.SetSelectedGameObject (btnBack.gameObject);
		}
	}

}
