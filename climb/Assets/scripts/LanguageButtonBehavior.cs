using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LanguageButtonBehavior : MonoBehaviour {

	public void ChangeLanguage (string lang) {
		PlayerPrefs.SetString ("Language", lang);
		LocalizationManager.localizedDictionary.Clear ();
		SceneManager.LoadScene (0);
	}

}
