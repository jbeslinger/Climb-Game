using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompassBehavior : MonoBehaviour {

	private float speed = 2f;
	private Vector3 targetAngle = new Vector3 (0f, 0f, 0f);
	private Vector3 currentAngle;

	public void Start () {
		currentAngle = transform.eulerAngles;
	}

	public void Update () {
		currentAngle = new Vector3(
			Mathf.LerpAngle(currentAngle.x, targetAngle.x, Time.deltaTime * speed),
			Mathf.LerpAngle(currentAngle.y, targetAngle.y, Time.deltaTime * speed),
			Mathf.LerpAngle(currentAngle.z, targetAngle.z, Time.deltaTime * speed));

		transform.eulerAngles = currentAngle;
	}

	public void LerpToDirection (float angle) {
//		transform.rotation = Quaternion.Lerp (transform.rotation, new Quaternion (0f, 0f, angle, 0f), Time.time * speed);
		targetAngle = new Vector3 (0f, 0f, angle);
	}

}
