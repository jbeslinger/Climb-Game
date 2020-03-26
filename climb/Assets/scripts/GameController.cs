using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	// Reference var to hold a single instance of object
	public static GameController instance = null;

	// Public Variables
	public GameObject prefabCoin, prefabLiteCoin;

	// Sort of Singleton design pattern for this object
	// Each Awake (), check to make sure the object wasn't already created
	// If it was, destroy it
	private void Awake () {
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad (gameObject);
		} else {
			Destroy (gameObject);
		}
	}

	// Every time a scene is loaded, make sure to set off all the triggers that the player already has
	// Also, turn off all of the coins and gems they've collected
	public void CheckTriggers () {
		if (GameManager.newGame) {
			return;
		}

		GameObject[] coinsInScene = GameObject.FindGameObjectsWithTag ("Coin");
		GameObject replacementCoinContainer = new GameObject ("collected_coins");
		// For each coin in the scene
		foreach (GameObject coin in coinsInScene) {
			if (coin != null) {
				// If the spot in the array of collected coins corresponding to the coin number is true
				if (GameManager.gameData.collectedCoins [int.Parse (coin.name.Substring (5))]) {
					// Then the coin is collected, so deactivate it & replace it with a collected coin
					coin.GetComponent<Animator> ().enabled = false;
					coin.GetComponent<CoinBehavior> ().enabled = false;
					coin.GetComponent<CoinBehavior> ().collected = true;
					coin.GetComponent<BoxCollider2D> ().enabled = false;

					SpriteRenderer coinSpriteRenderer = coin.GetComponent<SpriteRenderer> ();
					// Replace each collected coin with a greyed out one

					GameObject replacementCoin;
					if (SceneManager.GetActiveScene ().buildIndex == 4) {
						replacementCoin = Instantiate (prefabLiteCoin, replacementCoinContainer.transform);
					} else {
						replacementCoin = Instantiate (prefabCoin, replacementCoinContainer.transform);
					}
					replacementCoin.transform.position = coin.transform.position;
					replacementCoin.GetComponent<AudioSource> ().volume = coin.GetComponent<AudioSource> ().volume;
					replacementCoin.GetComponent<SpriteRenderer> ().color = new Color (coinSpriteRenderer.color.r,
						coinSpriteRenderer.color.g,
						coinSpriteRenderer.color.b,
						.5f);

					coinSpriteRenderer.enabled = false;
				}
			}
		}

		GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ().numberOfCoins = GameManager.gameData.coinCount;
		GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ().numberOfGems = GameManager.gameData.gemCount;

		GameObject[] gemsInScene = GameObject.FindGameObjectsWithTag ("Gem");
		// For each gem in the scene
		foreach (GameObject gem in gemsInScene) {
			if (gem != null) {
				// If the spot in the array of collected gems corresponding to the gem ID number is true
				if (GameManager.gameData.collectedGems [gem.GetComponent<GemBehavior> ().gemID]) {
					gem.GetComponent<Animator> ().enabled = false;
					gem.GetComponent<GemBehavior> ().enabled = false;
					gem.GetComponent<GemBehavior> ().collected = true;
					gem.GetComponent<BoxCollider2D> ().enabled = false;
					gem.GetComponent<SpriteRenderer> ().enabled = false;
				}
			}
		}
	}

}
