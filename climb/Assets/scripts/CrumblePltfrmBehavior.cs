using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrumblePltfrmBehavior : MonoBehaviour {

	// Constants
	private float PPU = 16f;

	// Public Variables
	public bool destroyed = false;
	public bool transparent = false; // Used to determine whether or not to act like a one-way platform

	// Private Variables
	private Animator myAnimator;
	private ParticleSystem myParticleSystem;
    private AudioSource myAudioSource;
    private SpriteRenderer mySpriteRenderer;
    private BoxCollider2D myBoxCollider2D;
	private GameObject player;
//	private float playerSpriteHeight;
	private float mySpriteHeight;
	private float playerFootPosition;
	private float platformTopPosition;
	private float platformHeight;

    // Use this for initialization
    private void Start () {
		try {
			player = GameObject.FindGameObjectWithTag ("Player");
			mySpriteRenderer = GetComponentInParent<SpriteRenderer> ();
			myBoxCollider2D = GetComponentInParent<BoxCollider2D> ();
			myAnimator = GetComponentInParent<Animator> ();
			myParticleSystem = GetComponentInParent<ParticleSystem> ();
			myAudioSource = GetComponent<AudioSource> ();

//			playerSpriteHeight = (GetComponent<SpriteRenderer> ().sprite.rect.height / PPU);
			mySpriteHeight = (GetComponent<SpriteRenderer> ().sprite.rect.height / PPU);

			ParticleSystem.EmissionModule myEmissionModule = myParticleSystem.emission;
			myEmissionModule.enabled = false;
		} catch {
			Debug.Log ("Script calls for a several components on this object that are now missing.");
		}
    }

    // Update is called once per frame
    private void Update () {
		if (player == null) {
			player = GameObject.FindGameObjectWithTag ("Player");
		}

		myAnimator.SetBool ("Destroyed", destroyed); // Tell the animator that it's time to be destroyed

		if (player.GetComponent<PlayerController> ().dying) {
			myAnimator.SetTrigger ("Player Died");
			Respawn ();
		}
    }

	private void FixedUpdate () {
		if (destroyed || !transparent) {
			return;
		}

		playerFootPosition = RoundToNearestHalfPoint(player.transform.position.y - 2f);
		platformTopPosition = RoundToNearestHalfPoint(transform.position.y + (mySpriteHeight / 2f));

		if (playerFootPosition > platformTopPosition) { // If the player's feet are below the platform, no collision
			myBoxCollider2D.enabled = true;
		} else if (playerFootPosition < platformTopPosition) { // Otherwise, the platform is solid
			myBoxCollider2D.enabled = false;
		} else {
			myBoxCollider2D.enabled = true;
		}
	}

	public void Respawn () {
		destroyed = false;
		mySpriteRenderer.enabled = true;
		myBoxCollider2D.enabled = true;

		ParticleSystem.EmissionModule myEmissionModule = myParticleSystem.emission;
		if (myEmissionModule.enabled) {
			myEmissionModule.enabled = false;
		}
	}

	public void PlaySound () {
		myAudioSource.Play ();
	}

	public void ToggleParticles () {
		ParticleSystem.EmissionModule myEmissionModule = myParticleSystem.emission;

		if (myEmissionModule.enabled) {
			myEmissionModule.enabled = false;
		} else {
			myEmissionModule.enabled = true;
		}
	}

	private float RoundToNearestHalfPoint (float numberToRound) {
		numberToRound *= 2f;
		return (Mathf.Round (numberToRound)) / 2f;
	}

}
