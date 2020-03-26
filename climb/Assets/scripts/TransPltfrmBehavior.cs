using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransPltfrmBehavior : MonoBehaviour {

	// Constants
	private float PPU = 16f;

	// Private Variables
	private GameObject player;
//	private float playerSpriteHeight;
	private float mySpriteHeight;
	private BoxCollider2D myBoxCollider2D;
	private float playerFootPosition;
	private float platformTopPosition;

    private void Start () {
        try {
			player = GameObject.Find ("player");
//			playerSpriteHeight = (GetComponent<SpriteRenderer> ().sprite.rect.height / PPU);
			mySpriteHeight = (GetComponent<SpriteRenderer> ().sprite.rect.height / PPU);
			myBoxCollider2D = GetComponent<BoxCollider2D> ();
        } catch {
            Debug.Log ("Script calls for an object named 'player' be present & a BoxCollider2D on Parent.");
        }
    }

	private void Update () {
		if (player == null) {
			player = GameObject.FindGameObjectWithTag ("Player");
		}
	}

	private void FixedUpdate () {
		playerFootPosition = RoundToNearestHalfPoint(player.transform.position.y - 2f);
		platformTopPosition = RoundToNearestHalfPoint(transform.position.y + (mySpriteHeight / 2f));

		if (playerFootPosition > platformTopPosition) { // If the player's feet are below the platform, no collision
			myBoxCollider2D.enabled = true;
		} else if (playerFootPosition < platformTopPosition) { // Otherwise, the platform is solid
			myBoxCollider2D.enabled = false;
		} else {
			myBoxCollider2D.enabled = true;
		}
	}

    // Takes a specific collider and ignores Player's
    // collision with it for a specifiied number of seconds
    // This is called when the player press Down + Z
    public IEnumerator HaltCollision (float seconds) {
		Physics2D.IgnoreCollision (player.GetComponent<CapsuleCollider2D> (), myBoxCollider2D, true);
		yield return new WaitForSecondsUnpaused (seconds);
		Physics2D.IgnoreCollision (player.GetComponent<CapsuleCollider2D> (), myBoxCollider2D, false);
    }

	private float RoundToNearestHalfPoint (float numberToRound) {
		numberToRound *= 2f;
		return (Mathf.Round (numberToRound)) / 2f;
	}

}
