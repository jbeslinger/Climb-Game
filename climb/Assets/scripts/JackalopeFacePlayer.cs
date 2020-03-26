using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JackalopeFacePlayer : MonoBehaviour {

	// Enums
	private enum FaceDirection { LEFT, RIGHT};

	// Private Variables
	private FaceDirection dir = FaceDirection.LEFT;
	private SpriteRenderer mySpriteRenderer;
	private GameObject player;

	private void Start () {
		try {
			mySpriteRenderer = GetComponent<SpriteRenderer> ();
			player = GameObject.FindGameObjectWithTag ("Player");
		} catch {
			Debug.Log ("This object needs to have a SpriteRenderer attached in order to work.\nOr, it can't find \"player\"");
		}
	}

	private void Update () {
		if (player == null) {
			player = GameObject.FindGameObjectWithTag ("Player");
		}

		if (!mySpriteRenderer.isVisible) {
			return;
		}

		if (dir == FaceDirection.LEFT) {
			mySpriteRenderer.flipX = false;
		} else if (dir == FaceDirection.RIGHT) {
			mySpriteRenderer.flipX = true;
		}

		if (player.transform.position.x < transform.position.x) {
			dir = FaceDirection.LEFT;
		} else {
			dir = FaceDirection.RIGHT;
		}
	}

}
