/* Use this script at runtime to automatically
 * round each point of a polygon collider 2D.
 * Then, copy the component, stop the editor, and paste values. */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSnapper : MonoBehaviour {
	
	public PolygonCollider2D myPolygonCollider2D;

	private void Start () {
		for (int i = 0; i <= (myPolygonCollider2D.pathCount - 1); i++) {
			Vector2[] paths = myPolygonCollider2D.GetPath (i);
			Vector2[] roundedPath = new Vector2[paths.Length];

			for (int e = 0; e <= (paths.Length - 1); e++) {
				roundedPath [e] = new Vector2 (Mathf.RoundToInt (paths [e].x), Mathf.RoundToInt (paths [e].y));
			}

			myPolygonCollider2D.SetPath (i, roundedPath);
		}
	}

}
