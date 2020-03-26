using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RGBMenuBehavior : MonoBehaviour {

	// Public Variables
	public Image imgPonchoIcon;
	public Slider [] rgbSliders = new Slider [6];

	// Private Variables
	private Texture2D myTexture2D;
	private Sprite originalPonchoSprite;

	private void OnEnable () {
		originalPonchoSprite = imgPonchoIcon.sprite;
		GetCutsomColorPref ();
		UpdateCharacterTexture ();
	}

	// This is called when any of the sliders are touched
	public void UpdateCharacterTexture () {
		if (originalPonchoSprite != null) {
			imgPonchoIcon.sprite = originalPonchoSprite;
		}

		//This calls the copy texture function, and copies it. The variable characterTextures2D is a Texture2D which is now the returned newly copied Texture2D.
		myTexture2D = CopyTexture2D(imgPonchoIcon.sprite.texture);

		//Get your SpriteRenderer, get the name of the old sprite, create a new sprite, name the sprite the old name, and then update the material. If you have multiple sprites, you will want to do this in a loop- which I will post later in another post.
		string tempName = imgPonchoIcon.sprite.name;
		imgPonchoIcon.sprite = Sprite.Create (myTexture2D, imgPonchoIcon.sprite.rect, new Vector2(0,1));
		imgPonchoIcon.sprite.name = tempName;

		imgPonchoIcon.material.mainTexture = myTexture2D;

	}

	//CopiedTexture is the original Texture  which you want to copy.
	private Texture2D CopyTexture2D (Texture2D copiedTexture) {
		Color bodyColor = new Color (rgbSliders [0].value, rgbSliders [1].value, rgbSliders [2].value);
		Color trimColor = new Color (rgbSliders [3].value, rgbSliders [4].value, rgbSliders [5].value);

		float h, s, v; // The Hue, Sat, Value of the bodyColor
		Color.RGBToHSV (bodyColor, out h, out s, out v);
		v /= 2f; // Divide the value in half
		Color bodyColorDark = Color.HSVToRGB (h, s, v); // Create a dark color from the body color

		if (bodyColorDark.r < .1f) {
			bodyColorDark.r = .1f;
		}

		//Create a new Texture2D, which will be the copy.
		Texture2D texture = new Texture2D(copiedTexture.width, copiedTexture.height);
		//Choose your filtermode and wrapmode here.
		texture.filterMode = FilterMode.Point;
		texture.wrapMode = TextureWrapMode.Clamp;

		// Going through each pixel
		int y = 0;
		Color currentPixelColor;
		while (y < texture.height) {
			int x = 0;
			while (x < texture.width) {
				currentPixelColor = copiedTexture.GetPixel (x, y);

				if (currentPixelColor == Color.cyan) { // If the pixel is cyan
					texture.SetPixel (x, y, bodyColor); // Set this pixel to the body color
				} else if (currentPixelColor == Color.red) {
					texture.SetPixel (x, y, bodyColorDark); // Set this pixel to the dark body color
				} else if (currentPixelColor == Color.magenta) { // If the pixel is magenta
					texture.SetPixel (x, y, trimColor); // Set this pixel to the trim color
				} else {
					texture.SetPixel(x, y, copiedTexture.GetPixel(x,y));
				}
				++x;
			}
			++y;
		}

		// Name the texture, if you want.
		texture.name = ("multicolor_poncho_icon");

		// This finalizes it. If you want to edit it still, do it before you finish with .Apply(). Do NOT expect to edit the image after you have applied. It did NOT work for me to edit it after this function.
		texture.Apply();

		// Return the variable, so you have it to assign to a permanent variable and so you can use it.
		return texture;
	}

	// Gets the cutsom color from gameData and sets it to the sliders.
	public void GetCutsomColorPref () {
		rgbSliders [0].value = GameManager.gameData.customPlayerColorBody [0];
		rgbSliders [1].value = GameManager.gameData.customPlayerColorBody [1];
		rgbSliders [2].value = GameManager.gameData.customPlayerColorBody [2];
		rgbSliders [3].value = GameManager.gameData.customPlayerColorTrim [0];
		rgbSliders [4].value = GameManager.gameData.customPlayerColorTrim [1];
		rgbSliders [5].value = GameManager.gameData.customPlayerColorTrim [2];
	}

	// Sets the cutsom color sliders to the gameData variables.
	public void SetCutsomColorPref () {
		GameManager.gameData.customPlayerColorBody [0] = rgbSliders [0].value;
		GameManager.gameData.customPlayerColorBody [1] = rgbSliders [1].value;
		GameManager.gameData.customPlayerColorBody [2] = rgbSliders [2].value;
		GameManager.gameData.customPlayerColorTrim [0] = rgbSliders [3].value;
		GameManager.gameData.customPlayerColorTrim [1] = rgbSliders [4].value;
		GameManager.gameData.customPlayerColorTrim [2] = rgbSliders [5].value;

		GameManager.SaveGame ();
	}

}
