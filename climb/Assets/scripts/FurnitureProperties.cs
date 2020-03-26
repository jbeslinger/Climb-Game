using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureProperties : MonoBehaviour {

	// Public Variables
	[HideInInspector] public string displayName;
	public string localizationKey;
	public uint price;
	public Sprite purchaseIcon;

	private void Start () {
		displayName = LocalizationManager.localizedDictionary [localizationKey];
	}

	private void OnDisable () {
		this.Start ();
	}

}
