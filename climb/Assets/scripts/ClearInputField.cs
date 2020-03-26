using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearInputField : MonoBehaviour {

	public void ClearSpecifiedInputField (InputField input) {
		input.text = "";
	}

}
