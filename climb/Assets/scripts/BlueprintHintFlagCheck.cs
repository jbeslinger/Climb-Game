/* This script sets the Active state of this gameobject
 * equal to the state of the blueprintHintTriggered var
 * in GameManager.gameData before anything happens. */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueprintHintFlagCheck : MonoBehaviour {

	private void Start () {
		gameObject.SetActive (!GameManager.gameData.blueprintHintTriggered);
	}

}
