using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateClouds : MonoBehaviour {

	// Public Variables
    public GameObject timeController; // This is used to reference the system time in order to change the cloud sprites
	public bool triggerNoonClouds = false; // If you're not using the timeController, but you want noon clouds, then 'true' this
	public GameObject[] cloudPrefabs; // Make sure to load this with various cloud prefabs
    public uint maxClouds = 6; // The maximum amount of clouds to spawn at once
    public uint numberOfClouds = 0; // The number of clouds on the screen
	public string cloudLayer;
	public int orderInLayer;
	public Color cloudColor = new Color (1f, 1f, 1f, 1f); // Use this if you need to adjust the color of the cloud prefabs
	public float yRange = 15f; // The min and max Y value the clouds can spawn at
    public float xBound = 40f; // The X value that clouds are destroyed at
	public float minSpeed = 0.05f; // The minimum and maximum speed clouds can travel at
	public float maxSpeed = 0.10f;
	public float minScale = 0.50f; // The minimum and maximum size the clouds can be
	public float maxScale = 2.00f;
    public List<GameObject> childClouds;

    private void Start () {
		for (int i = 0; i < maxClouds; i++) {
			GameObject newCloud = Instantiate (cloudPrefabs [Mathf.RoundToInt (Random.Range (0f, cloudPrefabs.Length - 1))],
				new Vector3 ((Random.Range (0f, xBound)) + transform.position.x, transform.position.y + Random.Range (-yRange, yRange), 0f), transform.rotation, gameObject.transform);
			
			// Try to set the sorting layer and order in layer for the new cloud
			try {
				newCloud.GetComponent<SpriteRenderer> ().sortingLayerName = cloudLayer;
			} catch {
				Debug.Log ("Invalid Sorting Layer name");
			}
			newCloud.GetComponent<SpriteRenderer> ().sortingOrder = orderInLayer;

			newCloud.GetComponent<SpriteRenderer> ().color = cloudColor;

			if (newCloud.GetComponent<CloudBehavior> () != null) {
				// Set the vars to the CloudBehavior component of the new cloud
				CloudBehavior newCloudBehav = newCloud.GetComponent<CloudBehavior> ();
				newCloudBehav.cloudGenerator = this.gameObject;
				newCloudBehav.speed = Random.Range (minSpeed, maxSpeed);
				newCloudBehav.scale = Random.Range (minScale, maxScale);
				newCloudBehav.xBound = xBound;
			} else {
				// Set the vars to the StormyCloudBehavior component of the new cloud
				StormyCloudBehavior newCloudBehav = newCloud.GetComponent<StormyCloudBehavior> ();
				newCloudBehav.cloudGenerator = this.gameObject;
				newCloudBehav.speed = Random.Range (minSpeed, maxSpeed);
				newCloudBehav.scale = Random.Range (minScale, maxScale);
				newCloudBehav.xBound = xBound;
			}

			childClouds.Add (newCloud);
			++numberOfClouds; // Increase the number of clouds by 1
		}
    }

    private void Update () {
		// If the max number of clouds isn't onscreen, spawn one and set its properties
		if (numberOfClouds <= maxClouds) {
			GameObject newCloud = Instantiate (cloudPrefabs [Mathf.RoundToInt (Random.Range (0f, cloudPrefabs.Length - 1))],
				new Vector3 (transform.position.x, transform.position.y + Random.Range (-yRange, yRange), 0f), transform.rotation, gameObject.transform);

			// Try to set the sorting layer and order in layer for the new cloud
			try {
				newCloud.GetComponent<SpriteRenderer> ().sortingLayerName = cloudLayer;
			} catch {
				Debug.Log ("Invalid Sorting Layer name");
			}
			newCloud.GetComponent<SpriteRenderer> ().sortingOrder = orderInLayer;

			newCloud.GetComponent<SpriteRenderer> ().color = cloudColor;

			if (newCloud.GetComponent<CloudBehavior> () != null) {
				// Set the vars to the CloudBehavior component of the new cloud
				CloudBehavior newCloudBehav = newCloud.GetComponent<CloudBehavior> ();
				newCloudBehav.cloudGenerator = this.gameObject;
				newCloudBehav.speed = Random.Range (minSpeed, maxSpeed);
				newCloudBehav.scale = Random.Range (minScale, maxScale);
				newCloudBehav.xBound = xBound;
			} else {
				// Set the vars to the StormyCloudBehavior component of the new cloud
				StormyCloudBehavior newCloudBehav = newCloud.GetComponent<StormyCloudBehavior> ();
				newCloudBehav.cloudGenerator = this.gameObject;
				newCloudBehav.speed = Random.Range (minSpeed, maxSpeed);
				newCloudBehav.scale = Random.Range (minScale, maxScale);
				newCloudBehav.xBound = xBound;
			}

			childClouds.Add (newCloud);
			++numberOfClouds; // Increase the number of clouds by 1
		}


		if (timeController != null) { // I put this line in so that you don't have to use the time controller
			if (timeController.GetComponent<TitlescrnTimeHandler> ().currentState == TitlescrnTimeHandler.DayState.NOON) {
				foreach (GameObject cloud in childClouds) {
					cloud.GetComponent<CloudBehavior> ().isNoon = true;
				}
			} else {
				foreach (GameObject cloud in childClouds) {
					cloud.GetComponent<CloudBehavior> ().isNoon = false;
				}
			}
		} else {
			if (triggerNoonClouds) {
				foreach (GameObject cloud in childClouds) {
					if (cloud.GetComponent<CloudBehavior> () != null) {
						cloud.GetComponent<CloudBehavior> ().isNoon = true;
					}
				}
			} else {
				foreach (GameObject cloud in childClouds) {
					if (cloud.GetComponent<CloudBehavior> () != null) {
						cloud.GetComponent<CloudBehavior> ().isNoon = false;
					}
				}
			}

		}

    }
}
