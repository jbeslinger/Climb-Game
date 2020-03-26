using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizedSpriteBehavior : MonoBehaviour {

	public SpriteRenderer mySpriteRenderer;
	public Sprite enSprite, ptSprite;

	private void OnEnable () {
		switch (PlayerPrefs.GetString ("Language", "en")) {
		case "en":
			mySpriteRenderer.sprite = enSprite;
			break;
		case "pt":
			mySpriteRenderer.sprite = ptSprite;
			break;
		}
	}

}
