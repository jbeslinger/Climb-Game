using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampFlipBehavior : MonoBehaviour {

	public SpriteRenderer playerSpriteRenderer;
	public bool flipped = false;
	public float xOrigin;

	private void Start () {
		playerSpriteRenderer = GameObject.FindGameObjectWithTag ("Player").GetComponent<SpriteRenderer> ();
		xOrigin = transform.parent.gameObject.transform.localPosition.x;
	}

	private void Update () {
		if (playerSpriteRenderer == null) {
			playerSpriteRenderer = GameObject.FindGameObjectWithTag ("Player").GetComponent<SpriteRenderer> ();
		}

		if (playerSpriteRenderer.flipX) {
			flipped = true;
		} else {
			flipped = false;
		}

		if (flipped) {
			transform.localPosition = new Vector3 (-xOrigin * 2f, transform.localPosition.y, transform.localPosition.z);
		} else {
			transform.localPosition = new Vector3 (0f, transform.localPosition.y, transform.localPosition.z);
		}
	}

}
