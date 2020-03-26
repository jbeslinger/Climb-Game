#if (UNITY_EDITOR)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugLoader : MonoBehaviour {

	private void OnEnable () {
		GameManager.LoadGame ();
		GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ().numberOfCoins = GameManager.gameData.coinCount;
	}

}
#endif
