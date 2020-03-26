using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayGemsWave : MonoBehaviour {

	// Public Variables
	public float offsetTime;
	public int displayGemID; // This number should correspond with the real gem's ID

	// Components
	private Animator myAnimator;

	void Start () {
		myAnimator = GetComponent<Animator> ();
		myAnimator.Play ("display_gem_float", -1, offsetTime);

		GetComponent<SpriteRenderer> ().enabled = GameManager.gameData.collectedGems [displayGemID];
	}
		
}
