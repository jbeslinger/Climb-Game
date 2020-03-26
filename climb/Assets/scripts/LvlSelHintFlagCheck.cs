/* This script is used to enable / disable the
 * Level Select menu button hint text depending
 * on whether or not the player has seen it yet. */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LvlSelHintFlagCheck : MonoBehaviour {

	private void OnEnable () {
		gameObject.SetActive (!GameManager.gameData.lvlSelHintTriggered);
		GameManager.gameData.lvlSelHintTriggered = true;
	}

}
