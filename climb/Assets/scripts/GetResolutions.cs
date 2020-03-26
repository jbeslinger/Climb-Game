using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetResolutions : MonoBehaviour {

	List<string> resolutions = new List<string> ();

	private void OnEnable () {
		UpdateIndex ();
	}

	private void Awake () {
		foreach (Resolution r in Screen.resolutions) {
			if (resolutions.Contains (r.width + "x" + r.height)) {
				continue;
			}

			resolutions.Add (r.width + "x" + r.height);
		}

		resolutions.Reverse ();
		GetComponent<Dropdown> ().AddOptions (resolutions);
	}

	public void UpdateIndex () {
		Resolution nativeRes = Screen.resolutions [Screen.resolutions.Length - 1];
		string currentRes = PlayerPrefs.GetString ("Resolution", nativeRes.width + "x" + nativeRes.height);

		int optionIndex = 0;
		foreach (string s in resolutions) {
			if (s == currentRes) {
				optionIndex = resolutions.IndexOf (s);
			}
		}
		GetComponent<Dropdown> ().value = optionIndex;
	}


}
