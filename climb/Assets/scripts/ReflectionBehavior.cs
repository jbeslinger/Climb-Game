using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectionBehavior : MonoBehaviour {

	// Public Variables
	public Material darkMaterial, colorChangingMaterial;

	// Private Variables
	private float reflectionOffset = 1f;
	private GameObject player;
	private SpriteRenderer mySpriteRenderer;
	private SpriteRenderer playerSpriteRenderer;

	// Grab the player sprite renderer component and this gameobject's
	void Start () {
		try {
			mySpriteRenderer = GetComponent<SpriteRenderer> ();
			player = GameObject.FindGameObjectWithTag ("Player");
			playerSpriteRenderer = player.GetComponent<SpriteRenderer> ();

			if (player.GetComponent<PlayerController>().playerCostume == PlayerController.Costume.MULTICOLOR) {
				mySpriteRenderer.material = colorChangingMaterial;
				ChangeToPlayerColor ();
			} else {
				mySpriteRenderer.material = darkMaterial;
			}
		} catch {
			Debug.Log ("There is either no SpriteRenderer on this object or the script can't find the player object");
		}
	}

	// If this sprite isn't the same as the player's, update this one's sprite to the player's sprite
	// Also, update the reflection's position to the player's plus the offset
	void Update () {
		if (player == null) {
			player = GameObject.FindGameObjectWithTag ("Player");
			playerSpriteRenderer = player.GetComponent<SpriteRenderer> ();

			if (player.GetComponent<PlayerController>().playerCostume == PlayerController.Costume.MULTICOLOR) {
				mySpriteRenderer.material = colorChangingMaterial;
				ChangeToPlayerColor ();
			} else {
				mySpriteRenderer.material = darkMaterial;
			}
		}

		transform.position = new Vector3 (player.transform.position.x + reflectionOffset, player.transform.position.y, transform.position.z);

		if (mySpriteRenderer.sprite != playerSpriteRenderer.sprite) {
			mySpriteRenderer.sprite = playerSpriteRenderer.sprite;
		}
		if (mySpriteRenderer.flipX != playerSpriteRenderer.flipX) {
			mySpriteRenderer.flipX = playerSpriteRenderer.flipX;
		}
	}

	private void ChangeToPlayerColor () {
		PaletteSwap.InitColorSwapTex (mySpriteRenderer);

		// Pull the colors from the GameManager
		Color bodyColor = new Color (GameManager.gameData.customPlayerColorBody [0],
			GameManager.gameData.customPlayerColorBody [1],
			GameManager.gameData.customPlayerColorBody [2], 1f);

		float h, s, v; // The Hue, Sat, Value of the bodyColor
		Color.RGBToHSV (bodyColor, out h, out s, out v);
		v /= 2f; // Divide the value in half
		Color bodyColorDark = Color.HSVToRGB (h, s, v); // Create a dark color from the body color

		if (bodyColorDark.r < .1f) {
			bodyColorDark.r = .1f;
		}

		Color trimColor = new Color (GameManager.gameData.customPlayerColorTrim [0],
			GameManager.gameData.customPlayerColorTrim [1],
			GameManager.gameData.customPlayerColorTrim [2], 1f);

		// Swap the colors
		PaletteSwap.SwapColor (PaletteSwap.SwapIndex.Body, bodyColor);
		PaletteSwap.SwapColor (PaletteSwap.SwapIndex.BodyDark, bodyColorDark);
		PaletteSwap.SwapColor (PaletteSwap.SwapIndex.Trim, trimColor);
		PaletteSwap.mColorSwapTex.Apply (); // Apply the texture
	}
}
