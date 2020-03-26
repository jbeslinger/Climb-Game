using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DefaultButtonBehavior : MonoBehaviour {

	public void SetDefaultPrefs () {
		PlayerPrefs.DeleteKey ("MusicVol");
		PlayerPrefs.DeleteKey ("SFXVol");
		PlayerPrefs.DeleteKey ("HUD Opacity");
		PlayerPrefs.DeleteKey ("Resolution");
		PlayerPrefs.DeleteKey ("Fullscreen");

		GameObject.FindObjectOfType<PlayerPrefHandler> ().LoadDefaultPrefs ();
		GameObject.FindObjectOfType<GetResolutions> ().UpdateIndex ();
	}

	public void SetDefaultControls () {
		PlayerPrefs.DeleteKey ("Select / Jump");
		PlayerPrefs.DeleteKey ("Run");
		PlayerPrefs.DeleteKey ("Up");
		PlayerPrefs.DeleteKey ("Down");
		PlayerPrefs.DeleteKey ("Left");
		PlayerPrefs.DeleteKey ("Right");
		PlayerPrefs.DeleteKey ("Pause");

		InputManager.LoadControls ();

		transform.parent.transform.parent.gameObject.SetActive (false);
		transform.parent.transform.parent.gameObject.SetActive (true);
	}

}
