/* Kind of an afterthought fix.
 * Checks to see if there exists any gameData
 * in GameManager before allowing the player to change levels. */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class GameDataExistsCheck : MonoBehaviour {

	void LateUpdate () {
		if (!File.Exists (Application.persistentDataPath + "/climbSave" + GameManager.fileNumber + ".sav")) {
			GetComponent<Button> ().interactable = false;
		} else {
			GetComponent<Button> ().interactable = true;
			this.enabled = false;
		}
	}

}
