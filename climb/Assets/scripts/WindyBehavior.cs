/* The point of this script is to affect the player
 * when they enter the area.  It applies force
 * in a specified direction. */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindyBehavior : MonoBehaviour {

	// ENUMS
	public enum Wind { LEFT, UP_LEFT, UP, UP_RIGHT, RIGHT, DOWN_RIGHT, DOWN, DOWN_LEFT };

	// PUBLIC VARIABLES
	public Wind windDirection;
	public uint lateralWindForce = 0;
	public uint verticalWindForce = 0;

	// PRIVATE VARIABLES
	private PlayerController player;
	private Rigidbody2D playerRigidBody2D;
	public float maxWindSpeedGround = 8f;
	public float maxWindSpeedAir = 24f;
	private bool playerWallSliding, playerClimbing;
	private CompassBehavior compass;

	// Try to find the player, then set the two MAX vars based on the player's maxSpeed variable
	// Also, grab the player's RigidBody2D
	private void Start () {
		try {
			player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();
		} catch {
			Debug.Log ("I can't find the player in the scene.");
		}

		try {
			playerRigidBody2D = player.GetComponent<Rigidbody2D> ();
		} catch {
			Debug.Log ("I can't find the player's RigidBody2D.");
		}

		try {
			compass = GameObject.FindObjectOfType<CompassBehavior> ();
		} catch {
			Debug.Log ("I can't find the compass.");
		}
	}

	// Every frame, depending on windDirection, apply lateralWindForce to player.movementSpeed,
	// AddForce() to the playerRB2D, or both.
	// If the player isn't in the level, skip all of it.
	private void Update () {
		if (player == null) {
			player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();
			playerRigidBody2D = player.GetComponent<Rigidbody2D> ();
		}

		// Check to see if the player is sliding down the wall or climbing a rope
		// Makes the Switch-case more readable.
		if (player.wallSlidingL || player.wallSlidingR) {
			playerWallSliding = true;
		} else {
			playerWallSliding = false;
		}
		if (player.climbingLadder || player.climbingRope) {
			playerClimbing = true;
		} else {
			playerClimbing = false;
		}

		switch (windDirection) {
		case Wind.LEFT:
			if (player.grounded && !playerWallSliding && !playerClimbing) {
				if (player.movementSpeed > -maxWindSpeedGround) {
					player.movementSpeed -= lateralWindForce;
				}
			} else if (!playerWallSliding && !playerClimbing) {
				if (player.movementSpeed > -maxWindSpeedAir) {
					player.movementSpeed -= lateralWindForce;
				}
			}
			compass.LerpToDirection (90f);
			break;
		case Wind.UP_LEFT:
			if (player.grounded && !playerWallSliding && !playerClimbing) {
				if (player.movementSpeed > -maxWindSpeedGround) {
					player.movementSpeed -= lateralWindForce;
				}
			} else if (!playerWallSliding && !playerClimbing) {
				if (player.movementSpeed > -maxWindSpeedAir) {
					player.movementSpeed -= lateralWindForce;
				}
			}
			if (!playerWallSliding && !playerClimbing) {
				playerRigidBody2D.AddForce (new Vector2 (0.0f, verticalWindForce));
			}
			compass.LerpToDirection (45f);
			break;
		case Wind.UP:
			if (!playerWallSliding && !playerClimbing) {
				playerRigidBody2D.AddForce (new Vector2 (0.0f, verticalWindForce));
			}
			compass.LerpToDirection (0f);
			break;
		case Wind.UP_RIGHT:
			if (player.grounded && !playerWallSliding && !playerClimbing) {
				if (player.movementSpeed < maxWindSpeedGround) {
					player.movementSpeed += lateralWindForce;
				}
			} else if (!playerWallSliding && !playerClimbing) {
				if (player.movementSpeed < maxWindSpeedAir) {
					player.movementSpeed += lateralWindForce;
				}
			}
			if (!playerWallSliding && !playerClimbing) {
				playerRigidBody2D.AddForce (new Vector2 (0.0f, verticalWindForce));
			}
			compass.LerpToDirection (-45f);
			break;
		case Wind.RIGHT:
			if (player.grounded && !playerWallSliding && !playerClimbing) {
				if (player.movementSpeed < maxWindSpeedGround) {
					player.movementSpeed += lateralWindForce;
				}
			} else if (!playerWallSliding && !playerClimbing) {
				if (player.movementSpeed < maxWindSpeedAir) {
					player.movementSpeed += lateralWindForce;
				}
			}
			compass.LerpToDirection (-90f);
			break;
		case Wind.DOWN_RIGHT:
			if (player.grounded && !playerWallSliding && !playerClimbing) {
				if (player.movementSpeed < maxWindSpeedGround) {
					player.movementSpeed += lateralWindForce;
				}
			} else if (!playerWallSliding && !playerClimbing) {
				if (player.movementSpeed < maxWindSpeedAir) {
					player.movementSpeed += lateralWindForce;
				}
			}
			if (!playerWallSliding && !playerClimbing) {
				playerRigidBody2D.AddForce (new Vector2 (0.0f, -verticalWindForce));
			}
			compass.LerpToDirection (-135f);
			break;
		case Wind.DOWN:
			if (!playerWallSliding && !playerClimbing) {
				playerRigidBody2D.AddForce (new Vector2 (0.0f, -verticalWindForce));
			}
			compass.LerpToDirection (180f);
			break;
		case Wind.DOWN_LEFT:
			if (player.grounded && !playerWallSliding && !playerClimbing) {
				if (player.movementSpeed > -maxWindSpeedGround) {
					player.movementSpeed -= lateralWindForce;
				}
			} else if (!playerWallSliding && !playerClimbing) {
				if (player.movementSpeed > -maxWindSpeedAir) {
					player.movementSpeed -= lateralWindForce;
				}
			}
			if (!playerWallSliding && !playerClimbing) {
				playerRigidBody2D.AddForce (new Vector2 (0.0f, -verticalWindForce));
			}
			compass.LerpToDirection (135f);
			break;
		}

		if (player.movementSpeed > maxWindSpeedAir) {
			player.movementSpeed = maxWindSpeedAir;
		} else if (player.movementSpeed < -maxWindSpeedAir) {
			player.movementSpeed = -maxWindSpeedAir;
		}
	}

}
