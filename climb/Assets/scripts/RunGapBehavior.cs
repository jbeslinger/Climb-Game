/* This script details exactly how the tiny gaps the player runs across
 * should work in any given situation */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunGapBehavior : MonoBehaviour {

	// Constants
	private const float PPU = 16f;

	// Public Variables
	public Collider2D myC2D;

	// Private Variables
	private GameObject player;
	private float playerHeight;
	private bool playerIsRunning;
	private bool playerIsAbove;

	// Use this for initialization
	void Start () {
		try {
			player = GameObject.FindGameObjectWithTag ("Player");
			playerHeight = (player.GetComponentInParent<SpriteRenderer> ().sprite.rect.height / PPU);
		} catch {
			Debug.Log ("Script call for an object named 'player' be present & a BoxCollider2D on Parent.");
		}
	}

	void FixedUpdate () {
		playerIsRunning = player.GetComponentInParent<PlayerController>().running;
	}

	// Update is called once per frame
	void Update () {
		if (player == null) {
			player = GameObject.FindGameObjectWithTag ("Player");
		}

		if (Mathf.Round ((player.transform.position.y - ((playerHeight / 2)))) >= transform.position.y) {
			playerIsAbove = true;
		} else {
			playerIsAbove = false;
		}

		if (playerIsRunning && playerIsAbove) {
			Physics2D.IgnoreCollision (player.GetComponentInParent<CapsuleCollider2D> (), myC2D, false);
		} else if (!playerIsRunning) {
			Physics2D.IgnoreCollision (player.GetComponentInParent<CapsuleCollider2D> (), myC2D, true);
		} else if (!playerIsAbove) {
			Physics2D.IgnoreCollision (player.GetComponentInParent<CapsuleCollider2D> (), myC2D, true);
		}
	}

}
