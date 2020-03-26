using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HiResScreenShots : MonoBehaviour {
	public int resWidth = 1920; 
	public int resHeight = 1080;

	private bool takeHiResShot = false;
	private Camera camera;
	private float timerLength = 15f;
	private float timer = 0.0f;

	private void OnEnable () {
		DontDestroyOnLoad (this.gameObject);
		camera = GetComponent<Camera> ();
		timer = timerLength;
	}

	public static string ScreenShotName(int width, int height) {
		return string.Format("{0}\\screen_{1}.png", 
			"C:\\Users\\jbeslinger\\Downloads\\snaps",
			System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
	}

	public void TakeHiResShot() {
		takeHiResShot = true;
	}

	private void Update () {
		transform.position = Camera.main.transform.position;

		if (timer >= 0.0) {
			timer -= Time.deltaTime;
		} else {
			timer = timerLength;
			takeHiResShot = true;
		}
	}

	private void LateUpdate() {
		if (takeHiResShot) {
			RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
			camera.targetTexture = rt;
			Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
			camera.Render();
			RenderTexture.active = rt;
			screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
			camera.targetTexture = null;
			RenderTexture.active = null; // JC: added to avoid errors
			Destroy(rt);
			byte[] bytes = screenShot.EncodeToPNG();
			string filename = ScreenShotName(resWidth, resHeight);
			System.IO.File.WriteAllBytes(filename, bytes);
			takeHiResShot = false;
		}
	}
}