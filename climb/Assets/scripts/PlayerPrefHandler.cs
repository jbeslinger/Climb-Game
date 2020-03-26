/* This contains all of the methods and properties used
 * by the Options menu to save play options like SFX & music vol. */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPrefHandler : MonoBehaviour {

	// Public Variables
	public Slider musicSlider, sfxSlider, hudSlider;
	public Dropdown resDropdown;
	public Toggle fullscrnToggle;
	public Image hud;
	public Image windIcon, arrowIcon;
	public Text hudText;
	public bool changesWereMade = false; // This will trigger whether or not to apply the binded controls

	// Private Variables
	private float musicVolume, sfxVolume, hudOpacity;
	private string resolution;
	private bool fullscreen;
	private string lang;

	private void Awake () {
		LoadPrefs ();
		LocalizationManager.Init (lang);
	}

	public void UpdateMusicVolume () {
		musicVolume = musicSlider.value;

		foreach (AudioSource audioSource in GameObject.FindObjectsOfType<AudioSource> ()) { // Look for each AudioSource
			if (audioSource.CompareTag ("Music")) { // If it's tagged 'Music', set the volume
				audioSource.volume = musicSlider.value;
			}
		}

		SavePrefs ();
	}

	public void UpdateSFXVolume () {
		sfxVolume = sfxSlider.value;

		foreach (AudioSource audioSource in GameObject.FindObjectsOfType<AudioSource> ()) { // Look for each AudioSource
			if (!audioSource.CompareTag ("Music")) { // As long as it's not tagged 'Music', set the volume
				audioSource.volume = sfxSlider.value;
			}
		}

		SavePrefs ();
	}

	public void UpdateHUDOpacity ()	{
		hudOpacity = hudSlider.value;

		hud.color = new Color (1f, 1f, 1f, hudOpacity);
		hudText.color = new Color (1f, 1f, 1f, hudOpacity);

		if (windIcon != null & arrowIcon != null) {
			windIcon.color = new Color (1f, 1f, 1f, hudOpacity);
			arrowIcon.color = new Color (1f, 1f, 1f, hudOpacity);
		}

		SavePrefs ();
	}

	public void UpdateResolution () {
		string proposedRes = resDropdown.options [resDropdown.value].text;
		resolution = proposedRes;

		string currentRes = (Screen.currentResolution.width + "x" + Screen.currentResolution.height);

		if (proposedRes != currentRes) {
			int newWidth = int.Parse (proposedRes.Split ('x') [0]);
			int newHeight = int.Parse (proposedRes.Split ('x') [1]);

			Screen.SetResolution (newWidth, newHeight, fullscreen);
		}

		SavePrefs ();
	}

	public void UpdateFullscreen () {
		fullscreen = fullscrnToggle.isOn;

		if (Screen.fullScreen != fullscreen) {
			Screen.fullScreen = fullscreen;
		}

		SavePrefs ();
	}

	// Set all the player preference values from properties in this class
	public void SavePrefs () {
		InputManager.SaveControls ();

		PlayerPrefs.SetFloat ("MusicVol", musicVolume);
		PlayerPrefs.SetFloat ("SFXVol", sfxVolume);
		PlayerPrefs.SetFloat ("HUD Opacity", hudOpacity);
		PlayerPrefs.SetString ("Resolution", resolution);
		PlayerPrefs.SetInt ("Fullscreen", (fullscreen ? 1 : 0));
	}

	// Set all values in a scene from playerprefs
	public void LoadPrefs () {
		InputManager.LoadControls ();

		musicVolume = PlayerPrefs.GetFloat ("MusicVol", 0.5f);
		sfxVolume = PlayerPrefs.GetFloat ("SFXVol", 0.5f);
		hudOpacity = PlayerPrefs.GetFloat ("HUD Opacity", 0.75f);

		Resolution nativeRes = Screen.resolutions [Screen.resolutions.Length - 1];
		resolution = PlayerPrefs.GetString ("Resolution", nativeRes.width + "x" + nativeRes.height);

		fullscreen = (PlayerPrefs.GetInt ("Fullscreen", 1) == 1);

		string currentRes = (Screen.currentResolution.width + "x" + Screen.currentResolution.height);
		if (resolution != currentRes) {
			int newWidth = int.Parse (resolution.Split ('x') [0]);
			int newHeight = int.Parse (resolution.Split ('x') [1]);

			Screen.SetResolution (newWidth, newHeight, fullscreen);
		}
		if (Screen.fullScreen != fullscreen) {
			Screen.fullScreen = fullscreen;
		}

		lang = PlayerPrefs.GetString ("Language", "en");

		foreach (AudioSource audioSource in GameObject.FindObjectsOfType<AudioSource> ()) { // Look for each AudioSource
			if (audioSource.CompareTag ("Music")) { // If it's tagged 'Music', set volume to 'MusicVol'
				audioSource.volume = musicVolume;
			} else { // If it's not tagged, set volume to 'SFXVol'
				audioSource.volume = sfxVolume;
			}
		}

		musicSlider.value = musicVolume;
		sfxSlider.value = sfxVolume;
		if (hud != null) {
			hud.color = new Color (1f, 1f, 1f, hudOpacity);
			hudText.color = new Color (1f, 1f, 1f, hudOpacity);
			hudSlider.value = hudOpacity;
		}

		if (windIcon != null & arrowIcon != null) {
			windIcon.color = new Color (1f, 1f, 1f, hudOpacity);
			arrowIcon.color = new Color (1f, 1f, 1f, hudOpacity);
		}

		fullscrnToggle.isOn = fullscreen;
	}

	public void LoadDefaultPrefs () {
		musicVolume = PlayerPrefs.GetFloat ("MusicVol", 0.5f);
		sfxVolume = PlayerPrefs.GetFloat ("SFXVol", 0.5f);
		hudOpacity = PlayerPrefs.GetFloat ("HUD Opacity", 0.75f);

		Resolution nativeRes = Screen.resolutions [Screen.resolutions.Length - 1];

		resolution = PlayerPrefs.GetString ("Resolution", nativeRes.width + "x" + nativeRes.height);
		fullscreen = (PlayerPrefs.GetInt ("Fullscreen", 1) == 1);

		musicSlider.value = musicVolume;
		sfxSlider.value = sfxVolume;
		if (hud != null) {
			hud.color = new Color (1f, 1f, 1f, hudOpacity);
			hudText.color = new Color (1f, 1f, 1f, hudOpacity);
			hudSlider.value = hudOpacity;
		}

		if (windIcon != null & arrowIcon != null) {
			windIcon.color = new Color (1f, 1f, 1f, hudOpacity);
			arrowIcon.color = new Color (1f, 1f, 1f, hudOpacity);
		}

		fullscrnToggle.isOn = fullscreen;

		Screen.SetResolution (nativeRes.width, nativeRes.height, fullscreen);
	}

}
