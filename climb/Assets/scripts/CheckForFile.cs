using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CheckForFile : MonoBehaviour {

	// Public Variables
	public GameObject deleteButton;

	// Private Variables
	private int fileNumber;

	private void Awake () {
		fileNumber = GetComponent<MainMenuBehaviors> ().fileNumber;
	}

	private void Update () {
		if (File.Exists (Application.persistentDataPath + "/climbSave" + fileNumber + ".sav") && !deleteButton.activeSelf) {
			deleteButton.SetActive (true);
		} else if (!File.Exists (Application.persistentDataPath + "/climbSave" + fileNumber + ".sav") && deleteButton.activeSelf) {
			deleteButton.SetActive (false);
		}
	}
}
