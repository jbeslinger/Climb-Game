/* This script is called when a certain hitbox of
 * the 4th world is triggered.  It makes the rain
 * stop by disabling a couple of grids. */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainStop : MonoBehaviour {

	// Public variables
	public GameObject rainPatterGrid, rainGrid;
	public bool trigger;

	private void Update () {
		if (!trigger) {
			return;
		} else {
			rainPatterGrid.GetComponent<Animator> ().enabled = true;
			rainGrid.GetComponent<Animator> ().enabled = true;

			try {
				Destroy (GameObject.Find ("cloud-spawn-stormy"));
			} catch {
			}

			StartCoroutine (DestroyAfterAnimation ());
		}
	}

	private IEnumerator DestroyAfterAnimation () {
		yield return new WaitForSecondsUnpaused (1f);
		rainGrid.GetComponent<Animator> ().enabled = false;
		rainPatterGrid.GetComponent<Animator> ().enabled = false;
		rainPatterGrid.GetComponent<Renderer> ().enabled = false;
		rainGrid.GetComponent<Renderer> ().enabled = false;
		Destroy (this.gameObject);
	}

	private void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.CompareTag ("Player")) {
			trigger = true;
		}
	}
}
