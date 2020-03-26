using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelSnapper {

	private const float PPU = 16f; // The project's PPU
	private const float PIXEL_INCREMENT = (100 / PPU) / 100; // Translate PPU to a single pixel of a unit

	// This method can snap a GameObject to the nearest pixel
	public static void SnapToNearestPixel (GameObject obj) {
		Vector3 desiredPos = obj.transform.position;

		// Subtract the remainder of sub-pixels
		desiredPos.x -= (desiredPos.x % PIXEL_INCREMENT);
		desiredPos.y -= (desiredPos.y % PIXEL_INCREMENT);

		// Set the object's position to the new snapped position
		obj.transform.position = desiredPos;
	}

	// This method can snap a Vector3 to the nearest pixel
	public static Vector3 SnapToNearestPixel (Vector3 desiredPos) {
		// Subtract the remainder of sub-pixels
		desiredPos.x -= (desiredPos.x % PIXEL_INCREMENT);
		desiredPos.y -= (desiredPos.y % PIXEL_INCREMENT);

		// Set the object's position to the new snapped position
		return desiredPos;
	}

	// This method can snap a Vector2 to the nearest pixel
	public static Vector2 SnapToNearestPixel (Vector2 desiredPos) {
		// Subtract the remainder of sub-pixels
		desiredPos.x -= (desiredPos.x % PIXEL_INCREMENT);
		desiredPos.y -= (desiredPos.y % PIXEL_INCREMENT);

		// Set the object's position to the new snapped position
		return desiredPos;
	}

}
