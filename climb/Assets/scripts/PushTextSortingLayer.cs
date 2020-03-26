using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushTextSortingLayer : MonoBehaviour {

	public void Appear () {
		GetComponent<TextMesh> ().text = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ().numberOfGems + "/16";
		GetComponent<Renderer> ().sortingLayerName = "Overlay Effects";
	}

}
