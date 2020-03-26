#if (UNITY_EDITOR)

/* This script adds an entry to the
 * context menu of any PolygonCollider2D
 * and automatically snaps all the points
 * to the nearest unit (integer). */

using UnityEditor;
using UnityEngine;

public class SnapAllPointsShortcut : MonoBehaviour {

	// Add a menu item called "Snapp All Points" to a PolygonCollider2D's context menu.
    [MenuItem("CONTEXT/PolygonCollider2D/Snap All Points")]
    static void SnapAllPoints (MenuCommand command) {
		PolygonCollider2D myPolygonCollider2D = (PolygonCollider2D)command.context;

		for (int i = 0; i <= (myPolygonCollider2D.pathCount - 1); i++) {
			Vector2[] paths = myPolygonCollider2D.GetPath (i);
			Vector2[] roundedPath = new Vector2[paths.Length];

			for (int e = 0; e <= (paths.Length - 1); e++) {
				roundedPath [e] = new Vector2 (RoundToNearestHalfPoint (paths [e].x), RoundToNearestHalfPoint (paths [e].y));
			}

			myPolygonCollider2D.SetPath (i, roundedPath);
		}
    }

	private static float RoundToNearestHalfPoint (float numberToRound) {
		numberToRound *= 2f;
		return (Mathf.Round (numberToRound)) / 2f;
	}

}

#endif