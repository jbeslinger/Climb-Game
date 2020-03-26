using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FollowPath : MonoBehaviour {
	// type of Movement
	public enum MovementType {
        MoveTowards,
        LerpTowards
    }
		
	// Public Variables
    public MovementType type = MovementType.MoveTowards; // Movement type used
    public MovementPath myPath; // Reference to Movement Path Used
    public float speed = 1; // speed object is moving
    public float maxDistanceToGoal = .1f; // How close does it have to be to the point to be considered at point
	public bool active; // Lets the script know if the object has been touched (activated)

	// Private Variables
	private PlayerController player;
	private IEnumerator<Transform> pointInPath; // Used to reference points returned from myPath.GetNextPathPoint
	private Vector3 startPosition; // Used to reset the position of the platform when the player dies
	private bool startActiveState; // Used to reset an inactive object upon player death
	//private bool snapping; // This tells the program whether or not the platform is snapping to the nearest pixel while moving

    private void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();

		startActiveState = active;

		// Make sure there is a path assigned
		if (myPath == null) {
			Debug.LogError ("Movement Path cannot be null, I must have a path to follow.", gameObject);
			return;
		}

		// Set the start position to the current position
		startPosition = transform.position;

		// Sets up a reference to an instance of the coroutine GetNextPathPoint
		pointInPath = myPath.GetNextPathPoint ();

		// Get the next point in the path to move to (Gets the Default 1st value)
		pointInPath.MoveNext ();

		// Make sure there is a point to move to
		if (pointInPath.Current == null) {
			Debug.LogError ("A path must have points in it to follow", gameObject);
			return; // Exit Start() if there is no point to move to
		}

		// Set the position of this object to the position of our starting point
		transform.position = pointInPath.Current.position;
    }

    private void Update () {
		if (player == null) {
			player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();
		}

		if (player.dying) {
			active = startActiveState;
			transform.position = startPosition;
			myPath.movingTo = 0;
			pointInPath.MoveNext ();
		}

		if (!active) {
			return; // Exit if the object isn't active
		}

		// Validate there is a path with a point in it
		if (pointInPath == null || pointInPath.Current == null) {
			return; // Exit if no path is found
		}

		if (type == MovementType.MoveTowards) { // If you are using MoveTowards movement type
			// Move to the next point in path using MoveTowards
			transform.position =
            Vector3.MoveTowards (transform.position,
				pointInPath.Current.position,
				Time.deltaTime * speed);
		} else if (type == MovementType.LerpTowards) { // If you are using LerpTowards movement type
			// Move towards the next point in path using Lerp
			transform.position = Vector3.Lerp (transform.position,
				pointInPath.Current.position,
				Time.deltaTime * speed);
		}

		// Check to see if you are close enough to the next point to start moving to the following one
		// Using Pythagorean Theorem
		// per unity suaring a number is faster than the square root of a number
		// Using .sqrMagnitude 
		var distanceSquared = (transform.position - pointInPath.Current.position).sqrMagnitude;

		// If you are close enough
		if (distanceSquared < maxDistanceToGoal * maxDistanceToGoal) {
			pointInPath.MoveNext (); // Get next point in MovementPath
		}
    
		// The version below uses Vector3.Distance same as Vector3.Magnitude which includes (square root)
		/*
        var distanceSquared = Vector3.Distance(transform.position, pointInPath.Current.position);
        if (distanceSquared < maxDistanceToGoal) // If you are close enough
        {
            pointInPath.MoveNext(); // Get next point in MovementPath
        }
        */

		/*
		// Basically, if the platform is moving to the next point in a straight line, then snap to the nearest pixel
		// If not, don't snap.  This is to allow the platform to move in sub-pixels when traversing along a diagonal line to keep it on track.
		if (((myPath.pathSequence [myPath.movingTo].position.x) == transform.position.x) ||
			((myPath.pathSequence [myPath.movingTo].position.y) == transform.position.y)) {
			snapping = true;
			PixelSnapper.SnapToNearestPixel (this.gameObject);
		} else {
			snapping = false;
		}
		*/
    }
}
