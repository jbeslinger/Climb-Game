using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PaletteSwap {

	public static Texture2D mColorSwapTex;
	public static Color[] mSpriteColors;

	// This enum is used to reference the R values in the custom player sprite
	public enum SwapIndex
	{
		Body = 50,
		BodyDark = 125,
		Trim = 200,
	}

	public static void InitColorSwapTex (SpriteRenderer targetSpriteRenderer) {
		Texture2D colorSwapTex = new Texture2D(256, 1, TextureFormat.RGBA32, false, false);
		colorSwapTex.filterMode = FilterMode.Point;

		for (int i = 0; i < colorSwapTex.width; ++i)
			colorSwapTex.SetPixel(i, 0, new Color(0.0f, 0.0f, 0.0f, 0.0f));

		colorSwapTex.Apply();

		targetSpriteRenderer.material.SetTexture ("_SwapTex", colorSwapTex);

		mSpriteColors = new Color[colorSwapTex.width];
		mColorSwapTex = colorSwapTex;
	}

	public static void SwapColor (SwapIndex index, Color color) {
		mSpriteColors [(int)index] = color;
		mColorSwapTex.SetPixel ((int)index, 0, color);
	}

}
