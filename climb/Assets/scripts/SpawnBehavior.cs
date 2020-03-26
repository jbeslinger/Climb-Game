/* The purpose of this script is to change
 * the animation state of the spawn point.
 * The spawn point is affected by the wind and 
 * changes animation based on the direction
 * of the wind. */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBehavior : MonoBehaviour {

	// Private Variables
	private Animator myAnimator;
	private WindyBehavior parentLevelWind;

	private void OnEnable () {
		myAnimator = GetComponent<Animator> ();
	}

	private void Start () {
		try {
			parentLevelWind = GetComponentInParent<WindyBehavior> ();

			switch (parentLevelWind.windDirection) {
			case WindyBehavior.Wind.LEFT:
				myAnimator.SetInteger ("WindDirection", 1);
				break;
			case WindyBehavior.Wind.UP_LEFT:
				myAnimator.SetInteger ("WindDirection", 2);
				break;
			case WindyBehavior.Wind.UP:
				myAnimator.SetInteger ("WindDirection", 2);
				break;
			case WindyBehavior.Wind.DOWN:
				myAnimator.SetInteger ("WindDirection", 3);
				break;
			default:
				myAnimator.SetInteger ("WindDirection", 0);
			break;
			}
		} catch {
		}
	}

	private void OnTriggerEnter2D (Collider2D other) {
		if (GameManager.speedrunning) {
			return;
		}

		if (other.CompareTag ("Player")) {
			GameManager.gameData.playerSpawnPos [0] = transform.position.x;
			GameManager.gameData.playerSpawnPos [1] = transform.position.y;
			GameManager.gameData.playerSpawnPos [2] = transform.position.z;

			GameManager.SaveGame ();
		}	
	}

}
