/* This static class is used as a translator between any kind
 * of input and the main programming.  It defines button-press,
 * button-lift, and button-hold functions for each button and axis.
 * It also contains several properties that hold the names of each
 * virtual button & has the ability to stop input entirely. */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;

public class InputManager {
	// These are defined at runtime based on PlayerPrefs
//	private static GameInput select, run, pause, up, down, left, right;

	// This is the public access for those GameInputs
	public static Dictionary<string, GameInput> inputs = new Dictionary<string, GameInput> () {
		{"Select / Jump", null},
		{"Run", null},
		{"Pause", null},
		{"Up", null},
		{"Down", null},
		{"Left", null},
		{"Right", null}
	};

	// These are static wrappers for accessing GetInput() functions of predefined GameInputs
	// SELECT
	public static bool SelectButton () {
		return inputs ["Select / Jump"].GetInput ();
	}
	public static bool SelectButtonDown () {
		return inputs ["Select / Jump"].GetInputDown ();
	}
	public static bool SelectButtonUp () {
		return inputs ["Select / Jump"].GetInputUp ();
	}

	// CANCEL
	public static bool CancelButton () {
		return inputs ["Run"].GetInput ();
	}
	public static bool CancelButtonDown () {
		return inputs ["Run"].GetInputDown ();
	}
	public static bool CancelButtonUp () {
		return inputs ["Run"].GetInputUp ();
	}

	// PAUSE
	public static bool PauseButton () {
		return inputs ["Pause"].GetInput ();
	}
	public static bool PauseButtonDown () {
		return inputs ["Pause"].GetInputDown ();
	}
	public static bool PauseButtonUp () {
		return inputs ["Pause"].GetInputUp ();
	}

	// HORIZONTAL
	public static bool LeftButton () {
		return inputs ["Left"].GetInput ();
	}
	public static bool LeftButtonDown () {
		return inputs ["Left"].GetInputDown ();
	}
	public static bool LeftButtonUp () {
		return inputs ["Left"].GetInputUp ();
	}
	public static bool RightButton () {
		return inputs ["Right"].GetInput ();
	}
	public static bool RightButtonDown () {
		return inputs ["Right"].GetInputDown ();
	}
	public static bool RightButtonUp () {
		return inputs ["Right"].GetInputUp ();
	}

	// VERTICAL
	public static bool DownButton () {
		return inputs ["Down"].GetInput ();
	}
	public static bool DownButtonDown () {
		return inputs ["Down"].GetInputDown ();
	}
	public static bool DownButtonUp () {
		return inputs ["Down"].GetInputUp ();
	}
	public static bool UpButton () {
		return inputs ["Up"].GetInput ();
	}
	public static bool UpButtonDown () {
		return inputs ["Up"].GetInputDown ();
	}
	public static bool UpButtonUp () {
		return inputs ["Up"].GetInputUp ();
	}

	// Start this Coroutine to halt all player inputs momentarily
	public static IEnumerator BlockInput (float seconds) {
		GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController>().pauseInput = true;
		yield return new WaitForSecondsUnpaused (seconds);
		GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController>().pauseInput = false;
	}

	// Set all player prefs via custom controls to a CSV format string
	public static void SaveControls () {
		GameInput gi = InputManager.inputs ["Select / Jump"];
		PlayerPrefs.SetString ("Select / Jump", gi.inputType + "," + gi.inputName + "," + gi.isNegativeAxis.ToString ());
		gi = InputManager.inputs ["Run"];
		PlayerPrefs.SetString ("Run", gi.inputType + "," + gi.inputName + "," + gi.isNegativeAxis.ToString ());
		gi = InputManager.inputs ["Up"];
		PlayerPrefs.SetString ("Up", gi.inputType + "," + gi.inputName + "," + gi.isNegativeAxis.ToString ());
		gi = InputManager.inputs ["Down"];
		PlayerPrefs.SetString ("Down", gi.inputType + "," + gi.inputName + "," + gi.isNegativeAxis.ToString ());
		gi = InputManager.inputs ["Left"];
		PlayerPrefs.SetString ("Left", gi.inputType + "," + gi.inputName + "," + gi.isNegativeAxis.ToString ());
		gi = InputManager.inputs ["Right"];
		PlayerPrefs.SetString ("Right", gi.inputType + "," + gi.inputName + "," + gi.isNegativeAxis.ToString ());
		gi = InputManager.inputs ["Pause"];
		PlayerPrefs.SetString ("Pause", gi.inputType + "," + gi.inputName + "," + gi.isNegativeAxis.ToString ());
	}

	// Read said CSV format string to string array and extrapolate data from there
	public static void LoadControls () {
		string[] loadedControl = PlayerPrefs.GetString ("Select / Jump", "key,Z,false").Split (',');
		InputManager.inputs ["Select / Jump"] = new GameInput (loadedControl [0], loadedControl [1], bool.Parse (loadedControl [2]));
//		Debug.Log (InputManager.inputs ["Select / Jump"].ToString ());

		loadedControl = PlayerPrefs.GetString ("Run", "key,X,false").Split (',');
		InputManager.inputs ["Run"] = new GameInput (loadedControl [0], loadedControl [1], bool.Parse (loadedControl [2]));
//		Debug.Log (InputManager.inputs ["Run"].ToString ());

		loadedControl = PlayerPrefs.GetString ("Up", "key,UpArrow,false").Split (',');
		InputManager.inputs ["Up"] = new GameInput (loadedControl [0], loadedControl [1], bool.Parse (loadedControl [2]));
//		Debug.Log (InputManager.inputs ["Up"].ToString ());

		loadedControl = PlayerPrefs.GetString ("Down", "key,DownArrow,false").Split (',');
		InputManager.inputs ["Down"] = new GameInput (loadedControl [0], loadedControl [1], bool.Parse (loadedControl [2]));
//		Debug.Log (InputManager.inputs ["Down"].ToString ());

		loadedControl = PlayerPrefs.GetString ("Left", "key,LeftArrow,false").Split (',');
		InputManager.inputs ["Left"] = new GameInput (loadedControl [0], loadedControl [1], bool.Parse (loadedControl [2]));
//		Debug.Log (InputManager.inputs ["Left"].ToString ());

		loadedControl = PlayerPrefs.GetString ("Right", "key,RightArrow,false").Split (',');
		InputManager.inputs ["Right"] = new GameInput (loadedControl [0], loadedControl [1], bool.Parse (loadedControl [2]));
//		Debug.Log (InputManager.inputs ["Right"].ToString ());

		loadedControl = PlayerPrefs.GetString ("Pause", "key,Escape,false").Split (',');
		InputManager.inputs ["Pause"] = new GameInput (loadedControl [0], loadedControl [1], bool.Parse (loadedControl [2]));
//		Debug.Log (InputManager.inputs ["Pause"].ToString ());
	}

}

/// <summary>
/// This class is used as a catch-all for KeyCodes, Joystick Buttons, and Joystick Axes. It also contains several fields used to describe the input.
/// </summary>
public class GameInput {
	// ENUM
	public enum type { KEY, BUTTON, AXIS }; // A type identifier to make life easier

	// PUBLIC VARS
	public readonly type inputType;
	public readonly string buttonName, axisName;
	public readonly KeyCode keyCode;
	public readonly bool isNegativeAxis; // Whether or not this is the negative half of said axis
	public bool wasPressedLastFrame = false; // These are used by GetInputDown() & GetInputUp() respectively to track the state of the axis
	public bool wasReleasedLastFrame = true;

	// PROPERTY
	public string inputName {
		get {
			if (this.inputType == type.KEY) {
				return this.keyCode.ToString ();
			} else if (this.inputType == type.BUTTON) {
				return this.buttonName;
			} else if (this.inputType == type.AXIS) {
				return this.axisName;
			} else {
				throw new UnityException ("There doesn't exist a GameInput of this type");
			}
		}
	}

	// CONSTRUCTOR
	public GameInput (string inputType, string inputName, bool isNegativeAxis) {
		switch (inputType.ToLower ()) {
		case "key":
			this.inputType = type.KEY;
			try {
				this.keyCode = (KeyCode)System.Enum.Parse (typeof(KeyCode), inputName);
			} catch {
				throw new UnityException ("Invalid input.");
			}
			break;
		case "button":
			this.inputType = type.BUTTON;
			this.buttonName = inputName;
			break;
		case "axis":
			this.inputType = type.AXIS;
			this.axisName = inputName;
			break;
		default: // Set the default control
			throw new UnityException ("Invalid input.");
		}
			
		this.isNegativeAxis = isNegativeAxis;
	}

	// PUBLIC METHODS
	/// <summary>
	/// Returns true each frame the user holds the specified GameInput.
	/// </summary>
	public bool GetInput () {
		switch (this.inputType) {
		case GameInput.type.KEY:
			return Input.GetKey (this.keyCode);
		case GameInput.type.BUTTON:
			return Input.GetButton (this.buttonName);
		case GameInput.type.AXIS:
			if (!this.isNegativeAxis)
				return (Input.GetAxis(this.axisName) > .25f) ? true : false;
			else
				return (Input.GetAxis(this.axisName) < -.25f) ? true : false;
		}

		return false;
	}

	/// <summary>
	/// Returns true the first frame the user presses the specified GameInput.
	/// </summary>
	public bool GetInputDown () {
		switch (this.inputType) {
		case GameInput.type.KEY:
			return Input.GetKeyDown (this.keyCode);
		case GameInput.type.BUTTON:
			return Input.GetButtonDown (this.buttonName);
		case GameInput.type.AXIS:
			if (!this.isNegativeAxis) {
				if (Input.GetAxis (this.axisName) > .25f) {
					if (!this.wasPressedLastFrame) {
						this.wasPressedLastFrame = true;
						return true;
					} else {
						return false;
					}
				} else {
					this.wasPressedLastFrame = false;
				}
			} else if (Input.GetAxis (this.axisName) < -.25f) {
				if (!this.wasPressedLastFrame) {
					this.wasPressedLastFrame = true;
					return true;
				} else {
					return false;
				}
			} else {
				this.wasPressedLastFrame = false;
			}
			break;
		}

		return false;
	}

	/// <summary>
	/// Returns true the first frame the user releases the specified GameInput.
	/// </summary>
	public bool GetInputUp () {
		switch (this.inputType) {
		case GameInput.type.KEY:
			return Input.GetKeyUp (this.keyCode);
		case GameInput.type.BUTTON:
			return Input.GetButtonUp (this.buttonName);
		case GameInput.type.AXIS:
			if (!this.isNegativeAxis) {
				if (Input.GetAxis (this.axisName) < .25f) {
					if (!this.wasReleasedLastFrame) {
						this.wasReleasedLastFrame = true;
						return true;
					} else {
						return false;
					}
				} else {
					this.wasReleasedLastFrame = false;
				}
			} else {
				if (Input.GetAxis (this.axisName) > -.25f) {
					if (!this.wasReleasedLastFrame) {
						this.wasReleasedLastFrame = true;
						return true;
					} else {
						return false;
					}
				} else {
					this.wasReleasedLastFrame = false;
				}
			}
			break;
		}

		return false;
	}

	/// <summary>
	/// Returns a human-readable string from the GameInput.
	/// </summary>
	public override string ToString () {
		TextInfo ti = new CultureInfo ("en-US", false).TextInfo;

		switch (this.inputType) {
		case type.KEY:
			return this.keyCode.ToString ();
		case type.BUTTON:
			return ti.ToTitleCase (this.buttonName);
		case type.AXIS:
			string name = ti.ToTitleCase (this.axisName);

			if (this.isNegativeAxis)
				name += "(-)";
			else
				name += "(+)";

			return name;
		}

		return "";
	} 
}