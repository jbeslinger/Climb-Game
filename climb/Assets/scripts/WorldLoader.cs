/* Attach this script to an empty World Load object (tagged WorldLoad)
 * with a BoxCollider2D trigger to load the next scene when
 * the player touches it. */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldLoader : MonoBehaviour {
	public int sceneID;

	public void LoadWorld () {
		InputManager.BlockInput (1f);
		GameObject.FindGameObjectWithTag ("Player").GetComponent<Rigidbody2D> ().simulated = false;
		GameObject.FindGameObjectWithTag ("Player").GetComponent<SpriteRenderer> ().enabled = false;
		GameManager.loadingGame = true;
		GameObject.Find ("Image-Fade").GetComponent<FadeOut> ().FadeToLevel (sceneID);
	}

}
