/* This script uses the child
 * GameObject boulder & drops it
 * from the starting point every
 * specified few seconds */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderBehavior : MonoBehaviour {

	// Public Variables
	public float respawnTime = 3.0f; // The amount of time before the spawner reclaims the boulder
	public float offsetTime = 0.0f; // The amount of time to wait before spawning the boulder; Used to create alternating boulder patterns
	public Sprite [] boulders; // The sprites for the script to cycle through
	public float boulderDropSpeed = 0.5f; // This value determines how fast the boulder drops

	// Private Variables
	private bool respawnFlag = false;
	private GameObject childBoulder;

	private void Start () {
		childBoulder = transform.GetChild (0).gameObject;
		childBoulder.GetComponent<Rigidbody2D> ().gravityScale = boulderDropSpeed; // Set the boulder's drop rate to gravityScale
		childBoulder.GetComponent<SpriteRenderer> ().sprite = RandomSprite (boulders); // Change the sprite to something random

		StartCoroutine (OffsetTimer (offsetTime));
	}

	private void Update () {
		// When the timer is on, this will be skipped every frame until time is up
		if (respawnFlag) {
			respawnFlag = false;
			StartCoroutine (RespawnTimer (respawnTime));
		}
	}

	private IEnumerator RespawnTimer (float t) {
		yield return new WaitForSecondsUnpaused (t);
		childBoulder.GetComponent<SpriteRenderer> ().sprite = RandomSprite (boulders);
		childBoulder.GetComponent<Transform> ().position = transform.position;
		childBoulder.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
		respawnFlag = true;

	}

	private IEnumerator OffsetTimer (float t) {
		yield return new WaitForSecondsUnpaused (t);
		childBoulder.GetComponent<Rigidbody2D> ().simulated = true;
		respawnFlag = true;
	}

	private Sprite RandomSprite (Sprite[] s) {
		return s [Random.Range (0, s.Length)];
	}
}
