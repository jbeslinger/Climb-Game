using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpeedrunMenuBehavior : MonoBehaviour {

	public void TriggerSpeedrunMode () {
		GameManager.speedrunning = true;
	}

	public void Restart () {
		GameObject.FindObjectOfType<FadeOut> ().FadeToLevel (SceneManager.GetActiveScene ().buildIndex);
	}

}
