/* This script is used to make sure that the sprites are scaled
 * to the same size on every resolution. */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamOrthosizeConfig : MonoBehaviour {
	
    const float PPU = 16f; // This is the number of pixels per unit

    private void Start () {
		Camera.main.orthographicSize = PPU;
    }

}
