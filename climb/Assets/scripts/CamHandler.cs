// By the way, at 2x scale, the camera fits 960x496 px or 60x31 units

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CamHandler : MonoBehaviour {
	
    // Public Vars
    public SpriteRenderer currentLevelSprite;
    public bool levelLoadFlag = true;
	public float lerpSpeed = 0.1f;

	// Private Vars
	private Vector3 desiredPos; // The position that Camera should be at that given moment
	private float xOffset, yOffset; // These help guide the camera to the correct position of the level in the world
	private GameObject player;
	private Text uiText;
	private string playerLevelName;
    private float rBound, lBound, tBound, bBound; // Right, left, top, and bottom bounds

    // Once the Camera starts, calculate the ortho size and find Player
    private void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		try {
			uiText = GameObject.Find ("Label-Score").GetComponent<Text> ();
		} catch {
		}
    }

    private void Update () {
		if (player == null) {
			player = GameObject.FindGameObjectWithTag ("Player");
		}

		if (levelLoadFlag) {
			OnLevelLoad ();
		}

		desiredPos = new Vector3 ((player.transform.position.x), (player.transform.position.y), -1); // Find the player
		desiredPos.x = Mathf.Clamp (desiredPos.x, lBound, rBound); // Trap ortho_camera within the horiz bounds
		desiredPos.y = Mathf.Clamp (desiredPos.y, bBound, tBound); // Trap ortho_camera within the vert bounds

		// Move the camera into position
		//Camera.main.transform.position = desiredPos;
		Camera.main.transform.position = Vector3.Lerp (Camera.main.transform.position, desiredPos, lerpSpeed);

		UpdateHUDText ();
    }

	// Change the UI text to reflect the level # and coins
	private void UpdateHUDText() {
		try {
			uiText.text = LocalizationManager.localizedDictionary ["level"] + " " + currentLevelSprite.GetComponentInParent<LevelProperties> ().worldID + "-" + currentLevelSprite.GetComponentInParent<LevelProperties> ().levelID + "\n" + LocalizationManager.localizedDictionary ["coins"] + " " + player.GetComponent<PlayerController> ().numberOfCoins.ToString ();
		} catch {
		}
	}

    // This should run every time the next scene loads
    private void OnLevelLoad () {
        levelLoadFlag = false;
        CalcLevelBounds ();
    }

    // This is the math behind finding the camera's bounds
    // as defined by 'levelXX-background' images
    private void CalcLevelBounds () {
		if (currentLevelSprite == null) {
			return;
		}

		float vertExtent = Camera.main.orthographicSize;  // The vertical
		float horizExtent = (vertExtent * Screen.width / Screen.height); // and horizontal extents of the camera
		xOffset = currentLevelSprite.transform.position.x;
		yOffset = currentLevelSprite.transform.position.y;

		rBound = (float)(currentLevelSprite.sprite.bounds.size.x / 2.0f - horizExtent) + xOffset;
		lBound = (float)(horizExtent - currentLevelSprite.sprite.bounds.size.x / 2.0f) + xOffset;
		tBound = (float)(currentLevelSprite.sprite.bounds.size.y / 2.0f - vertExtent) + yOffset;
		bBound = (float)(vertExtent - currentLevelSprite.sprite.bounds.size.y / 2.0f) + yOffset;

		desiredPos = new Vector3 ((player.transform.position.x), (player.transform.position.y), -1); // Find the player
		desiredPos.x = Mathf.Clamp (desiredPos.x, lBound, rBound); // Trap ortho_camera within the horiz bounds
		desiredPos.y = Mathf.Clamp (desiredPos.y, bBound, tBound); // Trap ortho_camera within the vert bounds

		Camera.main.transform.position = desiredPos;
    }

#if (UNITY_EDITOR)
	private void OnGUI () {
		GUI.Label (new Rect (10, 10, 100, 100), (1.0f / Time.smoothDeltaTime).ToString ("F"));
	}
#endif

}
