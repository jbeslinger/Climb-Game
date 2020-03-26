using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormyCloudBehavior : MonoBehaviour {

	public GameObject cloudGenerator;
	public float speed; // How fast and how big the cloud is
	public float scale;
	public Vector3 currentPosition;
	public float xBound; // The spot in the world where clouds go to die
	public bool pause = false; // Enable this when the game is paused, it will pause the clouds

	private void Start () {
		transform.localScale = new Vector3 (scale, scale, 1f);
	}

	private void Update () {

		if (transform.localPosition.x > xBound || transform.localPosition.x < -xBound) {
			cloudGenerator.GetComponent<GenerateClouds> ().childClouds.Remove (this.gameObject);
			cloudGenerator.GetComponent<GenerateClouds> ().numberOfClouds--;
			Destroy (this.gameObject);
		}

		if (pause) {
			return;
		}
		transform.localPosition = new Vector3 (transform.localPosition.x + speed, transform.localPosition.y, transform.localPosition.z);
	}

}
