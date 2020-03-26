/* This is the script that's called when the
 * Quit button is clicked. */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGameBehavior : MonoBehaviour
{
	
    // Quit the game
    public void QuitGame () {
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}
