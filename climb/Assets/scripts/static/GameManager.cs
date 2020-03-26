using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameManager {

	// Public Variables
	public static bool gameIsPaused = false; // Used to determine whether or not to pause or resume the game
	public static bool speedrunning = false; // This is used to determine if the player is speedrunning the level
	public static int fileNumber = 1; // Used to determine while slot to save the file to / load from
	public static GameData gameData; // A static reference to the save file data
	public static bool newGame = false;
	public static bool loadingGame = false;
	public static bool navigatingFurnitureMenu = false;
	public static bool endOfGameFlag = false;

	// Private Variables
	private static List<AudioSource> pausedAudioSources = new List<AudioSource> (); // Used to temporarily mute AudioSources
	private static List<Animator> pausedAnimators = new List<Animator> (); // Used to temporarily pause the Animators
	private static List<Rigidbody2D> pausedRigidbodies2D = new List<Rigidbody2D> (); // Used to temporarily pause Rigidbodies
	private static List<FollowPath> pausedMovingPlatforms = new List<FollowPath> (); // Used to temporarily pause all moving platforms
	private static List<ParticleSystem> pausedParticleSystems = new List<ParticleSystem> (); // Used to temporarily pause ParticleSystems
	private static List<CloudBehavior> pausedClouds = new List<CloudBehavior> () ; // Used to temporarily stop the clouds
	private static List<StormyCloudBehavior> pausedStormClouds = new List<StormyCloudBehavior> () ; // Used to temporarily stop the storm clouds
	private static PlayerController pausedPlayer; // Used to temporarily disable the script

	public static void PauseGame () {
		if (!gameIsPaused) {
			gameIsPaused = true;
		} else {
			ResumeGame ();
			return;
		}

		// Find each AudioSource in scene, disable them if they're enabled,
		// skip the music_controller, and add them to the list of pausedAudioSources
		foreach (AudioSource audioSource in GameObject.FindObjectsOfType<AudioSource> ()) {
			if (audioSource.CompareTag ("Music") || audioSource.CompareTag ("SFX") || audioSource.mute == true) {
				continue;
			}
			audioSource.mute = true;
			pausedAudioSources.Add (audioSource);
		}

		// Find each Animator in the scene, disable them if they're enabled, and add them to the list of pausedAnimators
		foreach (Animator animator in GameObject.FindObjectsOfType<Animator> ()) {
			if (animator.enabled && !animator.gameObject.CompareTag ("AutosaveIcon")) {
				animator.enabled = false;
				pausedAnimators.Add (animator);
			}
		}

		// Find each Rigidbody2D in the scene, disable them if they're enabled, and add them to the list of pausedRigidbodies2D
		foreach (Rigidbody2D rigidbody2d in GameObject.FindObjectsOfType<Rigidbody2D> ()) {
			if (rigidbody2d.simulated && !rigidbody2d.gameObject.CompareTag("Player")) {
				rigidbody2d.simulated = false;
				pausedRigidbodies2D.Add (rigidbody2d);
			}
		}

		// Find each FollowPath in scene (utilized by moving platforms), set 'active' to false,
		// and add them to the list of pausedMovingPlatforms
		foreach (FollowPath movingplatform in GameObject.FindObjectsOfType<FollowPath> ()) {
			if (movingplatform.active) {
				movingplatform.active = false;
				pausedMovingPlatforms.Add (movingplatform);
			}
		}

		foreach (ParticleSystem particleSystem in GameObject.FindObjectsOfType<ParticleSystem> ()) {
			if (particleSystem.isPlaying) {
				particleSystem.Pause ();
				pausedParticleSystems.Add (particleSystem);
			}
		}

		// Find each CloudBehavior & StormyCloudBehavior script in scene and set the 'pause' value of each to true
		// then, add the cloud or storm cloud to the cloud List
		foreach (CloudBehavior cloud in GameObject.FindObjectsOfType<CloudBehavior> ()) {
			if (!cloud.pause) {
				cloud.pause = true;
				pausedClouds.Add (cloud);
			}
		}
		foreach (StormyCloudBehavior sCloud in GameObject.FindObjectsOfType<StormyCloudBehavior> ()) {
			if (!sCloud.pause) {
				sCloud.pause = true;
				pausedStormClouds.Add (sCloud);
			}
		}

		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		pausedPlayer = player.GetComponent<PlayerController> ();
		pausedPlayer.running = false;
		pausedPlayer.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezeAll;
		pausedPlayer.enabled = false;
//		player.GetComponent<CapsuleCollider2D> ().enabled = false;

		GameObject.FindObjectOfType<PauseMenuBehavior> ().pausePanel.SetActive (true);
	}

	public static void ResumeGame () {
		gameIsPaused = false;

		foreach (AudioSource audioSource in pausedAudioSources) {
			if (audioSource != null) {
				audioSource.mute = false;
			}
		}
		foreach (Animator animator in pausedAnimators) {
			if (animator != null) {
				animator.enabled = true;
			}
		}
		foreach (Rigidbody2D rigidbody2d in pausedRigidbodies2D) {
			if (rigidbody2d != null) {
				rigidbody2d.simulated = true;
			}
		}
		foreach (FollowPath movingplatform in pausedMovingPlatforms) {
			if (movingplatform != null) {
				movingplatform.active = true;
			}
		}
		foreach (ParticleSystem particleSystem in pausedParticleSystems) {
			if (particleSystem != null) {
				particleSystem.Play ();
			}
		}
		foreach (CloudBehavior cloud in GameObject.FindObjectsOfType<CloudBehavior> ()) {
			if (cloud != null) {
				cloud.pause = false;
			}
		}
		foreach (StormyCloudBehavior sCloud in GameObject.FindObjectsOfType<StormyCloudBehavior> ()) {
			if (sCloud != null) {
				sCloud.pause = false;
			}
		}

		ClearAllTemporaryLists ();

		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		pausedPlayer.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezeRotation;
		if (pausedPlayer != null) {
			pausedPlayer.enabled = true;
		}
//		player.GetComponent<CapsuleCollider2D> ().enabled = true;

		if (gameData.playerCostume == 6) {
			player.GetComponent<PlayerController> ().InitPlayerTexture ();
		}


		GameObject.FindObjectOfType<PauseMenuBehavior> ().pausePanel.SetActive (false);

		navigatingFurnitureMenu = false;
	}

	private static void ClearAllTemporaryLists () {
		pausedAudioSources.Clear ();
		pausedAnimators.Clear ();
		pausedRigidbodies2D.Clear ();
		pausedMovingPlatforms.Clear ();
		pausedClouds.Clear ();
		pausedStormClouds.Clear ();
	}

	public static void SaveGame () {
		GameObject.FindGameObjectWithTag ("AutosaveIcon").GetComponent<Animator> ().SetTrigger ("Saving");

		BinaryFormatter binaryFormatter = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/climbSave" + fileNumber + ".sav");

		if (gameData == null) {
			gameData = new GameData ();
		}

		// Special save instructions for speedrunning mode
		// Basically, skip over the rest of this code and go straight to serializing
		if (speedrunning) {
			binaryFormatter.Serialize (file, gameData);
			file.Close ();
			return;
		}

		// Special save instructions for the player house scene
		if (SceneManager.GetActiveScene ().buildIndex == 6) {
			gameData.blueprintHintTriggered = true;
			gameData.coinCount = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ().numberOfCoins;
			binaryFormatter.Serialize (file, gameData);
			file.Close ();
			return;
		}

		gameData.playtime = GameObject.Find ("PlaytimeController").GetComponent<PlaytimeController> ().playtimeThisSession;

		if (endOfGameFlag) {
		} else {
			gameData.playerSceneID = SceneManager.GetActiveScene ().buildIndex;
		}

		// Store the player spawn position if the game has passed loading
//		Vector3 playerPos = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ().spawnPoint;
//		gameData.playerSpawnPos [0] = playerPos.x;
//		gameData.playerSpawnPos [1] = playerPos.y;
//		gameData.playerSpawnPos [2] = playerPos.z;

		gameData.coinCount = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ().numberOfCoins;
		gameData.gemCount = 0;
		// Update the gem count according to how many gems are in the collectedGems array
		foreach (bool gemIsCollected in gameData.collectedGems) {
			if (gemIsCollected) {
				gameData.gemCount += 1;
			}
		}

		// Unlock the costumes according to a couple factors
		if (gameData.gemCount >= 4) {
			if (gameData.unlockedCostumes [1] == false) {
				gameData.unlockedCostumes [1] = true;
				GameObject.Find ("Text-CostumeUnlock").GetComponent<Animator> ().Play ("costume_unlock_hint_flash");
			}
		}
		if (gameData.gemCount >= 8) {
			if (gameData.unlockedCostumes [2] == false) {
				gameData.unlockedCostumes [2] = true;
				GameObject.Find ("Text-CostumeUnlock").GetComponent<Animator> ().Play ("costume_unlock_hint_flash");
			}
		}
		if (gameData.gemCount >= 12) {
			if (gameData.unlockedCostumes [3] == false) {
				gameData.unlockedCostumes [3] = true;
				GameObject.Find ("Text-CostumeUnlock").GetComponent<Animator> ().Play ("costume_unlock_hint_flash");
			}
		} 
		if (gameData.gemCount == 16) {
			if (gameData.unlockedCostumes [4] == false) {
				gameData.unlockedCostumes [4] = true;
				GameObject.Find ("Text-CostumeUnlock").GetComponent<Animator> ().Play ("costume_unlock_hint_flash");
			}
		}
		if (gameData.playerHasBeatenGame) {
			if (gameData.unlockedCostumes [5] == false) {
				gameData.unlockedCostumes [5] = true;
				GameObject.Find ("Text-CostumeUnlock").GetComponent<Animator> ().Play ("costume_unlock_hint_flash");
			}
		}

		// Put every collected coin into the list
		foreach (CoinBehavior cb in GameObject.FindObjectsOfType<CoinBehavior> ()) {
			if (cb.gameObject.CompareTag ("CollectedCoin")) {
				continue;
			}

			if (cb.collected) {
				int coinNumber = int.Parse (cb.gameObject.name.Substring (5));
				gameData.collectedCoins[coinNumber] = true;
			}
		}

		// Triggers
		if (GameObject.FindWithTag ("StartCutsceneTrigger") != null) {
			gameData.startGameCutsceneTriggered = GameObject.FindWithTag ("StartCutsceneTrigger").GetComponent<GameStartCutscene> ().triggered;
		}

		binaryFormatter.Serialize (file, gameData);
		file.Close ();
	}

	public static void LoadGame () {
		if (File.Exists (Application.persistentDataPath + "/climbSave" + fileNumber + ".sav")) {
			BinaryFormatter binaryFormatter = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/climbSave" + fileNumber + ".sav", FileMode.Open);
			gameData = (GameData)binaryFormatter.Deserialize (file);
			file.Close ();
		}
	}

	public static void DeleteGame (int fileNumberToDelete) {
		if (File.Exists (Application.persistentDataPath + "/climbSave" + fileNumberToDelete + ".sav")) {
			File.Delete (Application.persistentDataPath + "/climbSave" + fileNumberToDelete + ".sav");
		}
	}

}

// This serializable class is responsible for data persisting throughout this game
// It holds very important data according to the game
[System.Serializable]
public class GameData {

	// Player Data
	public string fileName = ""; // The name of the file
	public int playerSceneID = 1; // The last scene the game was saved in
	public float [] playerSpawnPos = new float [3]; // The last position the player was at; stored as [0] - x, [1] - y, & [2] - z
	public int playerCostume = 0; // The id number associated with the costume the player is wearing
	public int coinCount = 0; // The number of coins the player currently possesses
	public int gemCount = 0; // The number of gems the player has collected
	public bool [] collectedCoins = new bool [321]; // A list of bools for the coins, to see which ones were collected; 'true' means collected
	public bool [] collectedGems = new bool [16]; // A list of bools for the gems, to see which ones were collected
	public float playtime = 0.0f; // How long the player has played this file

	public float [] bestTimes = new float [5]; // The best times the player has achieved while speedrunning each level

	// Triggers
	public bool startGameCutsceneTriggered = false; // Used to decide whether or not to play the opening cutscene
	public bool playerHasBeatenGame = false; // Used to keep track of whether or not the game has been beaten
	public bool blueprintHintTriggered = false; // Used to enable / disable the hint text in the house
	public bool lvlSelHintTriggered = false; // Used to enable / disable the hint text on the pause menu

	// Unlockable Levels
	/* 0 - Level 1-1
	 * 1 - Level 2-1
	 * 2 - Level 3-1
	 * 3 - Level 3-5
	 * 4 - Level 4-1 */
	public bool [] unlockedLevels = new bool[5];

	// Unlockable Costumes
	/* 0 - Poncho - Tan
	 * 1 - Speedy Poncho - Yellow
	 * 2 - Lightweight Poncho - Blue
	 * 3 - Woolen Poncho - Green
	 * 4 - Nighttime Poncho - Black
	 * 5 - Agile Poncho - White
	 * 6 - Multicolor Poncho */
	public bool [] unlockedCostumes = new bool [7];
	public float [] customPlayerColorBody = new float [3]; // This is the color the player set for the body of Multicolor poncho (r, g, b)
	public float [] customPlayerColorTrim = new float [3]; // " for the trim

	// Unlockable Furniture
	public bool [] unlockedFurniture = new bool [16];

	// Constructor is called when a new GameData is created; i.e. new game
	public GameData () {
		unlockedLevels [0] = true;

		unlockedCostumes [0] = true;
		unlockedCostumes [6] = true;

		customPlayerColorBody [0] = 0.1f;
		customPlayerColorBody [1] = 1.0f;
		customPlayerColorBody [2] = 1.0f;
		customPlayerColorTrim [0] = 1.0f;
		customPlayerColorTrim [1] = 0.0f;
		customPlayerColorTrim [2] = 1.0f;

//		#if (UNITY_EDITOR)
//			unlockedCostumes [1] = true;
//			unlockedCostumes [2] = true;
//			unlockedCostumes [3] = true;
//			unlockedCostumes [4] = true;
//			unlockedCostumes [5] = true;
//			playerHasBeatenGame = true;
//		#endif
	}

}