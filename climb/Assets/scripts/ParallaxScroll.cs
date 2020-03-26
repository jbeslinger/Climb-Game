/* This script's purpose is to
 * be attached to the background sprite
 * and adjust the position of the sprite
 * to lull around the camera's position horizontally & vertically. */

// This script will work effectively if the background sprite is
// the same size as the foreground

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScroll : MonoBehaviour
{

	public float horizLaxSpeed, vertLaxSpeed; // Edit these variables to adjust the speed which to move the sprite
    public float xOffset = 0f; // Edit these variables to adjust the optimum spot to place the sprite
	public float yOffset = 0f;

	private Vector3 desiredPos;
	private string parentLevelName;
	private Vector3 startPos; // This is used to reset the position of the sprite when the player isn't in the level

    void Start () {
        startPos = transform.position;
        parentLevelName = transform.parent.gameObject.name;
    }

    void Update () {
		// If the two speed vars can't be used to calc the lulled position, then exit
		if (horizLaxSpeed <= 0 || vertLaxSpeed <= 0) {
			return;
		}

		try {
			// If the player is in the level, then follow the camera with a lag
			// If not, the reset the sprite to where it was before
			if (Camera.main.GetComponent<CamHandler> ().currentLevelSprite.name == parentLevelName + "-foreground") {
				desiredPos = new Vector3 ((Camera.main.transform.position.x / horizLaxSpeed), (Camera.main.transform.position.y / vertLaxSpeed), transform.position.z); // Find the camera, translate to position & divide by parallax speed
				desiredPos.x += xOffset;
				desiredPos.y += yOffset;

				transform.position = desiredPos;

				PixelSnapper.SnapToNearestPixel (this.gameObject);
			} else {
				desiredPos = new Vector3 (0, 0, 0);
				transform.position = startPos;
			}
		} catch {
		}
    }

}