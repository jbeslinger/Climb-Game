#if (UNITY_EDITOR)

/* This script takes a selection of GameObjects and
 * adds numbers to the end of the name.  To help count
 * GameObjects. */

using UnityEditor;
using UnityEngine;

public class AutonumberGameObjectsShortcut : MonoBehaviour {

	// Add a menu item called "Snapp All Points" to a PolygonCollider2D's context menu.
    [MenuItem("GameObject/Auto-Number")]
    static void AutoNumber () {
		if (Selection.gameObjects == null) {
			Debug.Log ("No objects selected.");
			return;
		}

		int counter = 1;

		foreach (GameObject go in Selection.gameObjects) {
			go.name = go.name + " (" + counter + ")";
			++counter;
		}
    }

}

#endif