/* This is used to disable the overlay level tracer
 * I used while decorating the levels when I test-play the game. */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlayDisabler : MonoBehaviour {

	private void Start () {
		this.GetComponent<SpriteRenderer> ().enabled = false;
	}

}
