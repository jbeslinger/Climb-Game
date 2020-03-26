/* This script merely changes the mushroom's light color
 * by modifying the Red value of the light source upon start */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeColor : MonoBehaviour {

	// Private Variables
	Light myLight;

	void Start () {
		try {
			myLight = GetComponent<Light> ();
		} catch {
			Debug.Log ("There is no light attached to this object.");
			return;
		}

		int redVal = Random.Range (110, 255);
		float redValConverted = ((float)redVal / 255f);

		myLight.color = new Color (redValConverted, 1f, 1f);
	}

}
