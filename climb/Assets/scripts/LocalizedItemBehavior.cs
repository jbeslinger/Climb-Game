using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalizedItemBehavior : MonoBehaviour {

	public string localizationKey;

	private void Start () {
		try {
			GetComponent<Text> ().text = LocalizationManager.localizedDictionary [localizationKey];
		} catch {
		}
	}

}
