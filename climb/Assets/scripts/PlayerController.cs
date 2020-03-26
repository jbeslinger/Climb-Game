using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

	// Constants
	private const float PPU = 16f;
    
	// Enums
	public enum Face { LEFT = -1, RIGHT = 1 };
	public enum Costume { NORMAL, SPEEDY, LIGHT, WOOL, NIGHT, AGILE, MULTICOLOR };

	// Public Variables
	public LayerMask groundLayer;
	public bool pauseInput = false;
	public Face faceDir = Face.RIGHT; // The direction the player faces
	public Costume playerCostume;
	public Material standardMaterial, darkMaterial;
	public bool grounded, groundedOnClimbable; // A state to determine if the player is on the ground
	public bool wallSlidingL, wallSlidingR; // Says if the player is sliding down a wall on either side of them or not
	public bool climbingRope, climbingLadder; // Sees if the player is climbing a rope or a ladder
	public bool running, moving, rising, falling; // Says if the player is in any of these states
	public bool dying = false; // Says whether or not the player is dying
	public float movementSpeed; // the player's current speed during runtime
	public float maxSpeed;
	public int numberOfCoins = 0; // The amount of coins the player's collected
	public int numberOfGems = 0; // The amount of gems the player's collected
	public Vector3 spawnPoint;
	public GameObject spawnPointFlag = null; // The spawn point the player has touched
	public CamHandler mainCamHandler;

	// Private Variables
	private Rigidbody2D myRigidBody2D;
	private SpriteRenderer mySpriteRenderer;
	private Animator myAnimator;
	private ParticleSystem myParticleSystem;
	private PlayerSFXHandler playerSFXHandler;
//	private Vector3 startPos;
	private GameObject currentLevel; // Current level the player is in
	private bool touchingWallL, touchingWallR; // Used to check if the player is touching a wall on the (l)eft or (r)ight
	private bool ducking, lookingUp; // Says whether or not the player is ducking
	private bool sitting;
	private bool jumpPress, jumpCancel, canJump, canJumpAgain; // Keeps track of the player holding the jump button down & letting go, respectively; also, if the player is able to jummp
	private bool wallJumping; // Says if the player is walljumping
	private bool touchingClimbable = false; // Says if the player is touching something that can be climbed
	private GameObject climbableObj = null; // The object that can be climbed
	private bool touchingDoor = false; // Says whether or not the player is in a Door's trigger collider
	private GameObject doorObj = null; // The door that the player is touching
	private bool touchingChair = false;
	private GameObject chairObj = null;
	private GameObject switchObj = null;
	private bool touchingMoving = false; // Says if the player is on top of a moving platform
	private GameObject movingObj = null; // The moving object the player has touched; used to calculate where to put the player
	private bool climbingUpward, climbingDownward;
	private float acceleration = 2f; // the player's rate of acceleration
	public float maxSpeedWalk = 8f; // Top speed the player can reach walking
	public float maxSpeedRun = 16f; // Top speed the player can reach running
	private float climbSpeed = .2f; // Speed of the player's climb
	private float minJump = 2f; // The minimum height on the player's jump
	private float maxJump = 35f; // The max height on the player's jump
	private float walljumpForce = 20f; // Controls how far the player walljumps
	private float groundDrag = .75f; // The amount of drag the player undergoes on the ground
	private float dryDrag = 1f; // Amount of drag on dry ground
	private float wetDrag = .25f; // Amount of drag on wet ground
	private float airDrag = .1f; // The amount of drag the player undergoes in the air
	private float regularGravityScale; // The player's rigidbody2d gravity scale; it's init is in the Start()
	private float risingJumpMultiplier = 0.75f; // The number that affects the gravity on the player's rise to the peak of their jump
	private float slidingGravityMultiplier = 0.1f; // The number that affects gravity on the player's wall descent
	private float walkAnimSpeed = 0.75f; // The speed of the walk animation
	private float runAnimSpeed = 1.00f; // The speed of the running animation

	private void Awake () {
		// Gathering the components from the player
		myRigidBody2D = GetComponent<Rigidbody2D> ();
		regularGravityScale = myRigidBody2D.gravityScale;
		mySpriteRenderer = GetComponent<SpriteRenderer> ();
		myAnimator = GetComponent<Animator> ();
		myParticleSystem = GetComponent<ParticleSystem> ();
		playerSFXHandler = GetComponent<PlayerSFXHandler> ();

		if (mainCamHandler == null) {
			mainCamHandler = Camera.main.GetComponent<CamHandler> ();
		}

//		startPos = transform.position;
		spawnPoint = transform.position;

		// Set the player's material to darkMaterial if it's the player house or the cave levels and it's not the Multicolor costume
		if ((SceneManager.GetActiveScene ().buildIndex == 4 || SceneManager.GetActiveScene ().buildIndex == 6)) {
			mySpriteRenderer.material = darkMaterial;
		}

		// Set the SFX volume when the player is instantiated
		foreach (AudioSource audio in GetComponents<AudioSource> ()) {
			audio.volume = PlayerPrefs.GetFloat ("SFXVol", 0.5f);
		}
	}

	private void Start () {
		switch (playerCostume) {
		case Costume.SPEEDY:
			acceleration *= 1.25f;
			maxSpeedWalk *= 1.25f;
			maxSpeedRun = maxSpeedWalk * 2f;
			break;
		case Costume.LIGHT:
			maxJump *= 1.25f;
			break;
		case Costume.WOOL:
			wetDrag = groundDrag;
			break;
		case Costume.NIGHT:
			if (SceneManager.GetActiveScene ().buildIndex != 4 && SceneManager.GetActiveScene ().buildIndex != 6) {
				break;
			}
			foreach (SpriteRenderer sr in GameObject.FindObjectsOfType<SpriteRenderer>()) {
				sr.material = standardMaterial;
			}
			break;
		case Costume.MULTICOLOR:
			InitPlayerTexture ();
			break;
		}
	}

    // Used mostly for input response and for checking on the status of several vars
    private void Update () {
		UpdateParticleDirection ();

		// 'pauseInput' is turned true when the 'BlockInput()' function is called
		// The rest of the Update() is skipped with 'return;'
		if (pauseInput || GameManager.gameIsPaused) {
			return;
		}

		// ** INPUT**
		if ((InputManager.LeftButton () || InputManager.RightButton () || InputManager.SelectButton ()) && sitting) {
			chairObj = null;
			sitting = false;
		}

		// Pressing Left
		if ((InputManager.LeftButton () && !InputManager.RightButton () && (!climbingRope && !climbingLadder) && !ducking)) {
			if (!wallSlidingL) {
				faceDir = Face.LEFT;
			}

			lookingUp = false;
			if (!touchingWallL) {
				if (movementSpeed > -maxSpeed) {
					movementSpeed += -acceleration;
				}
			}
		}
		// Pressing Right
		if ((InputManager.RightButton () && !InputManager.LeftButton () && (!climbingRope && !climbingLadder) && !ducking)) {
			if (!wallSlidingR) {
				faceDir = Face.RIGHT;
			}

			lookingUp = false;
			if (!touchingWallR) {
				if (movementSpeed < maxSpeed) {
					movementSpeed += acceleration;
				}
			}
		}
		// Pressing Left + Right
		if (InputManager.LeftButton () && InputManager.RightButton ()) {
			lookingUp = false;
			ducking = false;
		}


		// Flicking a switch
		if (InputManager.UpButton () && switchObj != null) {
			switchObj.GetComponent<LightSwitchBehavior> ().ToggleLights ();
		}
		// Looking Up
		if (InputManager.UpButton () && grounded && !ducking && !moving && !touchingClimbable && !touchingDoor && !touchingChair) {
			lookingUp = true;
		}
		// Hopping on a climable object
		else if (InputManager.UpButton () && touchingClimbable && !(climbingRope || climbingLadder)) {
			if (climbableObj.name.Contains ("ladder")) {
				climbingLadder = true;
			} else if (climbableObj.name.Contains ("rope")) {
				climbingRope = true;
			}

			transform.position = new Vector2 (transform.position.x, transform.position.y + .1f);

			try {
				climbableObj.transform.GetChild (0).GetComponent<BoxCollider2D> ().enabled = false;
			} catch {
			}
		}
		// Entering a door
		else if (InputManager.UpButton () && touchingDoor) {
			InputManager.BlockInput (1f);
			myRigidBody2D.simulated = false;
			mySpriteRenderer.enabled = false;

			try {
				GameObject.Find ("player_light").SetActive (false);
				GameObject.Find ("player_light(Clone)").SetActive (false);
			} catch {
			}

			GameManager.loadingGame = true;
			GameObject.Find ("Image-Fade").GetComponent<FadeOut> ().FadeToLevel (doorObj.GetComponent<DoorBehavior> ().sceneID);
		}
		// Sitting on a chair
		else if (InputManager.UpButton () && touchingChair && !moving && grounded && !sitting) {
			try {
				chairObj.GetComponent<ChairBehavior> ().active = false;
				transform.position = new Vector3 (chairObj.transform.position.x, transform.position.y, transform.position.z);
			} catch {
				return;
			}
			movementSpeed = 0f;
			sitting = true;
		}
		// Not looking up
		else if (!InputManager.UpButton ()) {
			lookingUp = false;
		}
		// Climbing up
		if (InputManager.UpButton () && (climbingLadder || climbingRope)) {
			Vector2 ray = transform.position;
			float dist = 0.5f;
			//Debug.DrawRay (ray, Vector2.up, Color.green);

			RaycastHit2D hit = Physics2D.Raycast (ray, Vector2.up, dist, groundLayer);

			if (hit.collider == null) {
				climbingUpward = true;
			} else {
				climbingUpward = false;
			}
		} else if (!InputManager.UpButton () && (climbingLadder || climbingRope)) {
			climbingUpward = false;
		}

		// Pressing Down
		if (InputManager.DownButton () && grounded && !groundedOnClimbable) {
			if (lookingUp) {
				lookingUp = false;
			}
			ducking = true;
		} else if (InputManager.DownButton () && touchingClimbable && !(climbingRope || climbingLadder)) {
			if (grounded) {
				if (!groundedOnClimbable) {
					return;
				} else {
				}
			}

			if (climbableObj.name.Contains ("ladder")) {
				climbingLadder = true;
			} else if (climbableObj.name.Contains ("rope")) {
				climbingRope = true;
			}

			transform.position = new Vector2 (transform.position.x, transform.position.y + .1f);

			try {
				climbableObj.transform.GetChild (0).GetComponent<BoxCollider2D> ().enabled = false;
			} catch {
			}
		} else if (!InputManager.DownButton ()) {
			ducking = false;
		}

		// Climbing Down
		if (InputManager.DownButton () && (climbingLadder || climbingRope)) {
			climbingDownward = true;
		} else if (!InputManager.DownButton () && (climbingLadder || climbingRope)) {
			climbingDownward = false;
		}

		if ((InputManager.SelectButtonDown () && canJump && !ducking)) {
			if (climbingLadder || climbingRope) {
				climbingLadder = false;
				climbingRope = false;
			}

			playerSFXHandler.jumpFlag = true;
			jumpPress = true;
		} else if (InputManager.SelectButtonDown () && canJumpAgain && !ducking) {
			canJumpAgain = false;
			playerSFXHandler.jumpFlag = true;
			jumpPress = true;
		} else if ((InputManager.SelectButtonDown () && wallSlidingL) || (InputManager.SelectButtonDown () && wallSlidingR) && canJump) {
			playerSFXHandler.jumpFlag = true;
			jumpPress = true;
		} else if (InputManager.SelectButtonDown () && grounded && ducking) {
			FallThroughTransPlatforms ();
		}
		// Releasing Jump
		if (InputManager.SelectButtonUp () && !grounded) {
			jumpCancel = true;
		}

		// Pressing Run while moving
		if (InputManager.CancelButton () && moving) {
			if (grounded) {
				running = true;
			} else {
				running = false;
			}
		}
		if ((InputManager.CancelButton () && !moving) || (InputManager.CancelButtonUp ())) {
			running = false;
		}

		IsRisingOrFalling ();

		// Checking if the player is supposed to be sliding down a wall
		if (falling && touchingWallL) {
			wallSlidingL = true;
		} else if (falling && touchingWallR) {
			wallSlidingR = true;
		} else {
			wallSlidingL = false;
			wallSlidingR = false;
		}

		if (!running) {
			if (moving && grounded) {
				playerSFXHandler.walkFlag = true;
			} else if (!moving || !grounded) {
				playerSFXHandler.walkFlag = false;
			}
			playerSFXHandler.runFlag = false;
			maxSpeed = maxSpeedWalk;
		} else if (running) {
			playerSFXHandler.walkFlag = false;
			playerSFXHandler.runFlag = true;
			maxSpeed = maxSpeedRun;
		}

		if ((climbingLadder || climbingRope)) {
			if (grounded) {
				if (climbableObj.transform.GetChild (0).GetComponent<BoxCollider2D> () != null) {
					climbableObj.transform.GetChild (0).GetComponent<BoxCollider2D> ().enabled = true;
				}

				climbingRope = false;
				climbingLadder = false;
				climbingUpward = false;
				climbingDownward = false;
			}
		} else {
			climbingUpward = false;
			climbingDownward = false;
		}

		if (touchingMoving) {
			transform.parent = movingObj.transform;
			if (myRigidBody2D.interpolation != RigidbodyInterpolation2D.None) {
				myRigidBody2D.interpolation = RigidbodyInterpolation2D.None;
			}
		} else {
			transform.parent = null;
			if (myRigidBody2D.interpolation != RigidbodyInterpolation2D.Interpolate) {
				myRigidBody2D.interpolation = RigidbodyInterpolation2D.Interpolate;
			}
		}

		if (sitting) {
			mySpriteRenderer.flipX = false;
		}
    }

    // FixedUpdate is called once per frame
    // This function plays nicer with RigidBody2D
    private void FixedUpdate () {
		if (GameManager.gameIsPaused) {
			return;
		}

		// See if the player is touching the ground
		CheckGrounded ();
		// See if the player is touching a wall & if so, which side?
		CheckTouchingWall ();

		if (grounded || wallSlidingL || wallSlidingR || climbingLadder || climbingRope) {
			canJump = true;

			if (playerCostume == Costume.AGILE) {
				canJumpAgain = true;
			}
		} else {
			canJump = false;
		}

		// ** PHYSICS **
		// Ground Movement

		// Applying the velocity + drag to the RigidBody2D
		if (movementSpeed > 0 && grounded) {
			movementSpeed += -groundDrag;
			myRigidBody2D.velocity = PixelSnapper.SnapToNearestPixel (new Vector2 (movementSpeed, myRigidBody2D.velocity.y));
		} else if (movementSpeed > 0 && !grounded) {
			movementSpeed += -airDrag;
			myRigidBody2D.velocity = PixelSnapper.SnapToNearestPixel (new Vector2 (movementSpeed, myRigidBody2D.velocity.y));
		} else if (movementSpeed < 0 && grounded) {
			movementSpeed += groundDrag;
			myRigidBody2D.velocity = PixelSnapper.SnapToNearestPixel (new Vector2 (movementSpeed, myRigidBody2D.velocity.y));
		} else if (movementSpeed < 0 && !grounded) {
			movementSpeed += airDrag;
			myRigidBody2D.velocity = PixelSnapper.SnapToNearestPixel (new Vector2 (movementSpeed, myRigidBody2D.velocity.y));
		} else if (movementSpeed == 0 && grounded) {
			myRigidBody2D.velocity = PixelSnapper.SnapToNearestPixel (new Vector2 (0, myRigidBody2D.velocity.y));
		}

		// Equalizing the player movement to 0 if they are standing still
		// Fixes the 'sub-increment' problem with the calculation of movementSpeed
		if ((Mathf.Abs (movementSpeed) > 0 && Mathf.Abs (movementSpeed) < 0.5)) {
			movementSpeed = 0;
		}

		// Jumping
		// Raise the player while they hold the jump button
		if (jumpPress) {
			if (!wallSlidingL && !wallSlidingR) { // Normal jump
				myRigidBody2D.velocity = PixelSnapper.SnapToNearestPixel (new Vector2 (myRigidBody2D.velocity.x, maxJump));
				jumpPress = false;
			} else if (wallSlidingL) { // Wall jumps
				//StartCoroutine(BlockInput(.05f));
				faceDir = Face.RIGHT;
				touchingWallL = false;
				movementSpeed += walljumpForce;
				myRigidBody2D.velocity = PixelSnapper.SnapToNearestPixel (new Vector2 (movementSpeed, maxJump));
				//myRigidBody2D.AddForce(new Vector2(walljumpForce * 1.25f, walljumpForce * 2.5f));
				jumpPress = false;
			} else if (wallSlidingR) {
				//StartCoroutine(BlockInput(.05f));
				faceDir = Face.LEFT;
				touchingWallR = false;
				movementSpeed += -walljumpForce;
				myRigidBody2D.velocity = PixelSnapper.SnapToNearestPixel (new Vector2 (movementSpeed, maxJump));
				//myRigidBody2D.AddForce(new Vector2(-walljumpForce * 1.25f, walljumpForce * 2.5f));
				jumpPress = false;
			}
		}
		// Drop the player if they let go of the jump button
		if (jumpCancel) {
			if (myRigidBody2D.velocity.y > minJump) {
				myRigidBody2D.velocity = PixelSnapper.SnapToNearestPixel (new Vector2 (myRigidBody2D.velocity.x, minJump));
			}
			jumpCancel = false;
		}

		// Climbing
		if (climbingUpward) {
			transform.position = PixelSnapper.SnapToNearestPixel (new Vector3 (transform.position.x, transform.position.y + climbSpeed, transform.position.z));
		}
		if (climbingDownward) {
			transform.position = PixelSnapper.SnapToNearestPixel (new Vector3 (transform.position.x, transform.position.y - climbSpeed, transform.position.z));
		}

		// Flip the sprite to change the direction the player is facing
		FlipPlayer (faceDir);

		// Check if the player's horizontal velocity is other than 0 & they're pressing the movement buttons
		// to see if they are deliberately moving
		if ((Mathf.Abs (myRigidBody2D.velocity.x) > 0) && (InputManager.LeftButton () || InputManager.RightButton ())) {
			moving = true;
		} else {
			moving = false;
		}

		if ((climbingRope || climbingLadder) && climbableObj != null) {
			transform.position = PixelSnapper.SnapToNearestPixel (new Vector3 (climbableObj.transform.position.x, transform.position.y, transform.position.z));
			myRigidBody2D.gravityScale = 0f;
			myRigidBody2D.velocity = new Vector2 (0, 0);
			movementSpeed = 0;
			return;
		}

		// Change the player's RigidBody2D gravity scale while sliding down wall
		if ((wallSlidingL || wallSlidingR) && !grounded) {
			myRigidBody2D.gravityScale = regularGravityScale * slidingGravityMultiplier;
			if (touchingWallL) {
				faceDir = Face.RIGHT;
			} else if (touchingWallR) {
				faceDir = Face.LEFT;
			}
		} else {
			myRigidBody2D.gravityScale = regularGravityScale;
		}

		// Lower gravity while they are rising in the air
		if (rising && !(climbingRope || climbingLadder)) {
			myRigidBody2D.gravityScale = regularGravityScale * risingJumpMultiplier;
		}

		if (touchingWallL || touchingWallR) {
			movementSpeed = 0f;
		}
    }

    // This update is called after everything is calculated, said and done
    // I think it'll work best for animation param updates
    private void LateUpdate () {
		if (GameManager.gameIsPaused) {
			return;
		}

		myAnimator.SetBool ("Grounded", grounded);
		myAnimator.SetBool ("Moving", moving);
		myAnimator.SetBool ("Rising", rising);
		myAnimator.SetBool ("Falling", falling);
		myAnimator.SetBool ("Looking Up", lookingUp);
		myAnimator.SetBool ("Ducking", ducking);
		myAnimator.SetBool ("Climbing Ladder", climbingLadder);
		myAnimator.SetBool ("Climbing Rope", climbingRope);
		myAnimator.SetBool ("Sitting", sitting);

		if (climbingRope || climbingLadder) {
			if (climbingUpward || climbingDownward) {
				myAnimator.enabled = true;
			} else {
				myAnimator.enabled = false;
			}
		} else {
			myAnimator.enabled = true;
		}

		// Separates the two wall-sliding states
		if (wallSlidingL || wallSlidingR) {
			myAnimator.SetBool ("Sliding", true);
		} else {
			myAnimator.SetBool ("Sliding", false);
		}

		// If the player is running, double the animation speed
		if (!running) {
			myAnimator.SetFloat ("Running Speed", walkAnimSpeed);
		} else {
			myAnimator.SetFloat ("Running Speed", runAnimSpeed);
		}
    }

    // This will handle collectibles the player touches
    private void OnTriggerEnter2D (Collider2D other) {
		// If the object was a coin, play the animation, up the coin count, and destroy the coin
		if (other.gameObject.CompareTag ("Coin")) {
			numberOfCoins += 1;
		}
		if (other.gameObject.CompareTag ("Gem")) {
			numberOfGems = 0;
			GameManager.gameData.collectedGems [other.GetComponent<GemBehavior> ().gemID] = true;
			foreach (bool gemIsCollected in GameManager.gameData.collectedGems) {
				if (gemIsCollected) {
					numberOfGems += 1;
				}
			}
			GameManager.SaveGame ();
			other.gameObject.GetComponent<GemBehavior> ().collected = true;
		}

		if (other.gameObject.CompareTag ("Hidden")) {
			other.gameObject.GetComponent<Animator> ().SetBool ("Player Touching", true);
		}

		if (other.gameObject.CompareTag ("Climbable")) {
			touchingClimbable = true;
			climbableObj = other.gameObject;
			climbableObj.GetComponent<ClimbableObjProperties> ().playerTouching = true;
		}
		if (other.gameObject.CompareTag ("Door")) {
			touchingDoor = true;
			doorObj = other.gameObject;
		}
		if (other.gameObject.CompareTag ("Chair")) {
			touchingChair = true;
			chairObj = other.gameObject;
		}
		if (other.gameObject.CompareTag ("LightSwitch")) {
			switchObj = other.gameObject;
		}

		// If the object being collided with is tagged w/ 'LevelBound', then transition the player to the level that they're touching
		if (other.gameObject.CompareTag ("LevelBound")) {
			ChangeLevel (other.gameObject);
		}
		// If the object is tagged "WorldLoad"", change the scene to the one specified on the object
		if (other.gameObject.CompareTag ("WorldLoad")) {
			other.GetComponent<WorldLoader> ().LoadWorld ();
		}

		if (other.gameObject.CompareTag ("Respawn")) {
			if (spawnPointFlag != null) {
				spawnPointFlag.GetComponent<Animator> ().SetBool ("Active", false);
			}

			spawnPoint = other.transform.position;
			spawnPointFlag = other.gameObject;
			spawnPointFlag.GetComponent<Animator> ().SetBool ("Active", true);

			if (GameManager.newGame) {
				GameManager.newGame = false;
			}
		}

		if (other.gameObject.CompareTag ("Death")) {
			Die ();
		}
    }

	private void OnTriggerExit2D (Collider2D other) {
		if (other.gameObject.CompareTag ("Hidden")) {
			other.gameObject.GetComponent<Animator> ().SetBool ("Player Touching", false);
		}

		if (other.gameObject.CompareTag ("Climbable")) {
			if (climbingLadder || climbingRope) {
				climbingRope = false;
				climbingLadder = false;
				climbingUpward = false;
				climbingDownward = false;
			}
			touchingClimbable = false;
			climbableObj.GetComponent<ClimbableObjProperties> ().playerTouching = false;

			try {
				climbableObj.transform.GetChild (0).GetComponent<BoxCollider2D> ().enabled = true;
			} catch {
			}

			climbableObj = null;
		}

		if (other.gameObject.CompareTag ("Door")) {
			touchingDoor = false;
			doorObj = null;
		}

		if (other.gameObject.CompareTag ("Chair")) {
			touchingChair = false;
			chairObj = null;
		}

		if (other.gameObject.CompareTag ("LightSwitch")) {
			switchObj = null;
		}
	}

    // Changes the direction the player faces by flipping the sprite based on an enum
    private void FlipPlayer (Face faceDir) {
		if (faceDir == Face.RIGHT && !dying) {
			mySpriteRenderer.flipX = false;
		} else if (faceDir == Face.LEFT && !dying) {
			mySpriteRenderer.flipX = true;
		}
    }

    // See of the player is touching the ground using raycasts
    // Also checks if the collider is tagged with "Transparent" &
    // handles transparent platforms
    private void CheckGrounded () {
		float xOffset = 0.65f;
		float distance = .25f;

		Vector2 position1 = (transform.position);
		position1.y -= ((mySpriteRenderer.sprite.rect.height / 16) / 2);

		Vector2 position2 = position1;
		Vector2 position3 = position1;
		position2.x -= xOffset;
		position3.x += xOffset;

		RaycastHit2D hit1 = Physics2D.Raycast (position1, Vector2.down, distance, groundLayer);
		RaycastHit2D hit2 = Physics2D.Raycast (position2, Vector2.down, distance, groundLayer);
		RaycastHit2D hit3 = Physics2D.Raycast (position3, Vector2.down, distance, groundLayer);

		if ((hit1.collider != null) || (hit2.collider != null) || (hit3.collider != null)) {
			try {
				if (hit1.collider.CompareTag ("Transparent")) {
					if (!rising) {
						grounded = true;
					}
				} else if (hit1.collider.CompareTag ("Climbable")) {
					grounded = true;
					groundedOnClimbable = true;
				} else if (hit1.collider.CompareTag ("Wet")) {
					grounded = true;
					groundDrag = wetDrag;
				} else if (hit1.collider.CompareTag ("Moving")) {
					grounded = true;
					touchingMoving = true;
					movingObj = hit1.collider.gameObject;
					hit1.collider.gameObject.GetComponent<FollowPath> ().active = true;
				} else if (!hit1.collider.CompareTag ("Transparent") && !hit1.collider.CompareTag ("Climbable") && !hit1.collider.CompareTag ("Wet")) {
					grounded = true;
					groundedOnClimbable = false;
					groundDrag = dryDrag;
					if (hit1.collider.CompareTag ("Crumbling")) {
						hit1.collider.GetComponentInParent<CrumblePltfrmBehavior> ().destroyed = true;
					}
				}
			} catch {
				try {
					if (hit2.collider.CompareTag ("Transparent")) {
						if (!rising) {
							grounded = true;
						}
					} else if (hit2.collider.CompareTag ("Climbable")) {
						grounded = true;
						groundedOnClimbable = true;
					} else if (hit2.collider.CompareTag ("Wet")) {
						grounded = true;
						groundDrag = wetDrag;
					} else if (hit2.collider.CompareTag ("Moving")) {
						grounded = true;
						touchingMoving = true;
						movingObj = hit2.collider.gameObject;
						hit2.collider.gameObject.GetComponent<FollowPath> ().active = true;
					} else if (!hit2.collider.CompareTag ("Transparent") && !hit2.collider.CompareTag ("Climbable") && !hit2.collider.CompareTag ("Wet")) {
						grounded = true;
						groundedOnClimbable = false;
						groundDrag = dryDrag;
						if (hit2.collider.CompareTag ("Crumbling")) {
							hit2.collider.GetComponentInParent<CrumblePltfrmBehavior> ().destroyed = true;
						}
					}
				} catch {
					try {
						if (hit3.collider.CompareTag ("Transparent")) {
							if (!rising) {
								grounded = true;
							}
						} else if (hit3.collider.CompareTag ("Climbable")) {
							grounded = true;
							groundedOnClimbable = true;
						} else if (hit3.collider.CompareTag ("Wet")) {
							grounded = true;
							groundDrag = wetDrag;
						} else if (hit3.collider.CompareTag ("Moving")) {
							grounded = true;
							touchingMoving = true;
							movingObj = hit3.collider.gameObject;
							hit3.collider.gameObject.GetComponent<FollowPath> ().active = true;
						} else if (!hit3.collider.CompareTag ("Transparent") && !hit3.collider.CompareTag ("Climbable") && !hit3.collider.CompareTag ("Wet")) {
							grounded = true;
							groundedOnClimbable = false;
							groundDrag = dryDrag;
							if (hit3.collider.CompareTag ("Crumbling")) {
								hit3.collider.GetComponentInParent<CrumblePltfrmBehavior> ().destroyed = true;
							}
						}
					} catch {
					}
				}
			}
		} else {
			grounded = false;
			touchingMoving = false;
			groundedOnClimbable = false;
		}

		return;
    }

    // Function that raycasts x3 across the player and returns 'true'
    // if one of the three raycasts hits an object on groundLayer
    // beside them
    private bool CheckTouchingWall () {
		float yOffset = 0.75f;
		float distance = 0.7f;

		// Setting the first raycast position
		Vector2 position1 = transform.position;
		position1.y -= yOffset;

		Vector2 position2 = position1;
		Vector2 position3 = position1;
		position2.y -= yOffset;
		position3.y += yOffset;

		// Left-facing casts
		RaycastHit2D lhit1 = Physics2D.Raycast (position1, Vector2.left, distance, groundLayer);
		RaycastHit2D lhit2 = Physics2D.Raycast (position2, Vector2.left, distance, groundLayer);
		RaycastHit2D lhit3 = Physics2D.Raycast (position3, Vector2.left, distance, groundLayer);
		if ((lhit1.collider != null) || (lhit2.collider != null) || (lhit3.collider != null)) {
			try {
				if (!lhit1.collider.CompareTag ("Transparent") && !lhit1.collider.CompareTag ("Crumbling") && !lhit1.collider.CompareTag ("Gap") && !lhit1.collider.CompareTag ("One-Way")) {
					touchingWallL = true;
					if (lhit1.collider.CompareTag ("CrumblingSideways")) {
						lhit1.collider.GetComponentInParent<CrumblePltfrmBehavior> ().destroyed = true;
					}
				}
			} catch {
				try {
					if (!lhit2.collider.CompareTag ("Transparent") && !lhit2.collider.CompareTag ("Crumbling") && !lhit2.collider.CompareTag ("Gap") && !lhit2.collider.CompareTag ("One-Way")) {
						touchingWallL = true;
						if (lhit2.collider.CompareTag ("CrumblingSideways")) {
							lhit2.collider.GetComponentInParent<CrumblePltfrmBehavior> ().destroyed = true;
						}
					}
				} catch {
					try {
						if (!lhit3.collider.CompareTag ("Transparent") && !lhit3.collider.CompareTag ("Crumbling") && !lhit3.collider.CompareTag ("Gap") && !lhit3.collider.CompareTag ("One-Way")) {
							touchingWallL = true;
							if (lhit3.collider.CompareTag ("CrumblingSideways")) {
								lhit3.collider.GetComponentInParent<CrumblePltfrmBehavior> ().destroyed = true;
							}
						}
					} catch {
					}
				}
			}
		} else {
			touchingWallL = false;
		}

		// Right-facing casts
		RaycastHit2D rhit1 = Physics2D.Raycast (position1, Vector2.right, distance, groundLayer);
		RaycastHit2D rhit2 = Physics2D.Raycast (position2, Vector2.right, distance, groundLayer);
		RaycastHit2D rhit3 = Physics2D.Raycast (position3, Vector2.right, distance, groundLayer);
		if ((rhit1.collider != null) || (rhit2.collider != null) || (rhit3.collider != null)) {
			try {
				if (!rhit1.collider.CompareTag ("Transparent") && !rhit1.collider.CompareTag ("Crumbling") && !rhit1.collider.CompareTag ("Gap") && !rhit1.collider.CompareTag ("One-Way")) {
					touchingWallR = true;
					if (rhit1.collider.CompareTag ("CrumblingSideways")) {
						rhit1.collider.GetComponentInParent<CrumblePltfrmBehavior> ().destroyed = true;
					}
				}
			} catch {
				try {
					if (!rhit2.collider.CompareTag ("Transparent") && !rhit2.collider.CompareTag ("Crumbling") && !rhit2.collider.CompareTag ("Gap") && !rhit2.collider.CompareTag ("One-Way")) {
						touchingWallR = true;
						if (rhit2.collider.CompareTag ("CrumblingSideways")) {
							rhit2.collider.GetComponentInParent<CrumblePltfrmBehavior> ().destroyed = true;
						}
					}
				} catch {
					try {
						if (!rhit3.collider.CompareTag ("Transparent") && !rhit3.collider.CompareTag ("Crumbling") && !rhit3.collider.CompareTag ("Gap") && !rhit3.collider.CompareTag ("One-Way")) {
							touchingWallR = true;
							if (rhit3.collider.CompareTag ("CrumblingSideways")) {
								rhit3.collider.GetComponentInParent<CrumblePltfrmBehavior> ().destroyed = true;
							}
						}
					} catch {
					}
				}
			}
		} else {
			touchingWallR = false;
		}

		return false;
    }

    // Update the player states to see if the player is rising from a jump or falling
    private void IsRisingOrFalling () {
		if (myRigidBody2D.velocity.y > 0 && !grounded && (!climbingLadder || !climbingRope)) {
			rising = true;
			falling = false;
		} else if (myRigidBody2D.velocity.y < 0 && (!climbingLadder || !climbingRope)) {
			rising = false;
			if (!grounded) {
				falling = true;
			} else {
				falling = false;
			}
		} else if (myRigidBody2D.velocity.y == 0 || grounded) {
			rising = false;
			falling = false;
		}
    }

    // This method is called when the player's collider enters
    // a collider tagged 'Death'; it triggers the death animation,
    // shuts off gravity, and respawns the player
    private void Die () {
		dying = true;
		transform.SetParent (null);
		myAnimator.enabled = true;
		myAnimator.SetTrigger ("Die");
		playerSFXHandler.dieFlag = true;

		float death_animation_time = 0f;
		RuntimeAnimatorController myAnimator_ac = myAnimator.runtimeAnimatorController;
		foreach (AnimationClip ac in myAnimator_ac.animationClips) {
			if (ac.name == "player_die") {
				death_animation_time = ac.length;
			}
		}

		InputManager.BlockInput (death_animation_time);
    }

    private void Respawn () {
		myAnimator.SetTrigger ("Respawn");
		dying = false;
		movementSpeed = 0;
		faceDir = Face.RIGHT;
		transform.position = spawnPoint;
    }

    private void FallThroughTransPlatforms () {
		GameObject nearestPlatform = null;
		GameObject[] transparentObjects = GameObject.FindGameObjectsWithTag ("Transparent");

		// Find nearest GameObject w/ tag "Transparent"
		float minDist = Mathf.Infinity;
		Vector3 currentPos = transform.position;
		currentPos.y -= 1f; // Drop the position to the player's feet

		foreach (GameObject t in transparentObjects) {
			float dist = Vector3.Distance (t.transform.position, currentPos);
			if (dist < minDist) {
				nearestPlatform = t;
				minDist = dist;
			}
		}

		try {
			StartCoroutine (nearestPlatform.GetComponent<TransPltfrmBehavior> ().HaltCollision (.25f));
		} catch {
			return;
		}
    }

    private void UpdateParticleDirection () {
		ParticleSystem.ShapeModule myParticleSystem_shape = myParticleSystem.shape;

		switch (faceDir) {
		case Face.LEFT:
			myParticleSystem_shape.rotation = new Vector3 (0f, 0f, 15f);
			break;
		case Face.RIGHT:
			myParticleSystem_shape.rotation = new Vector3 (0f, 180f, 15f);
			break;
		}

		if (running && grounded) {
			// This is the current way to turn on and off a ParticleSystem
			// No fucking clue why
			ParticleSystem.EmissionModule em = myParticleSystem.emission;
			em.enabled = true;
		} else {
			ParticleSystem.EmissionModule em = myParticleSystem.emission;
			em.enabled = false;
		}
    }

    private void ChangeLevel (GameObject newLevel) {
		// Snap the player to the next unit & a half to stop them from sitting in between levels
		if (movementSpeed < 0) {
			transform.position = new Vector3 (transform.position.x - 1.5f, transform.position.y, transform.position.z);
		} else if (movementSpeed > 0) {
			transform.position = new Vector3 (transform.position.x + 1.5f, transform.position.y, transform.position.z);
		}

		// Deactivate the old level wind physics, if possible
		if (currentLevel != null) {
			try {
				currentLevel.GetComponent<WindyBehavior>().enabled = false;
			} catch {
			}
		}

		// Swap old level for new level
		currentLevel = newLevel;

		// Activate the new level wind physics, if possible
		try {
			currentLevel.GetComponent<WindyBehavior>().enabled = true;
		} catch {
		}

		mainCamHandler.levelLoadFlag = true;
		mainCamHandler.currentLevelSprite = GameObject.Find (newLevel.name + "-foreground").GetComponent<SpriteRenderer> ();
	}

	// This is only called for the Multicolor player obj
	public void UpdateCharacterTexture () {
		Color bodyColor = new Color (GameManager.gameData.customPlayerColorBody [0], GameManager.gameData.customPlayerColorBody [1], GameManager.gameData.customPlayerColorBody [2]);
		Color trimColor = new Color (GameManager.gameData.customPlayerColorTrim [0], GameManager.gameData.customPlayerColorTrim [1], GameManager.gameData.customPlayerColorTrim [2]);

		float h, s, v; // The Hue, Sat, Value of the bodyColor
		Color.RGBToHSV (bodyColor, out h, out s, out v);
		v /= 2f; // Divide the value in half
		Color bodyColorDark = Color.HSVToRGB (h, s, v); // Create a dark color from the body color

		if (bodyColorDark.r < .1f) {
			bodyColorDark.r = .1f;
		}

		Texture2D copiedTexture = mySpriteRenderer.sprite.texture;

		//Create a new Texture2D, which will be the copy.
		Texture2D texture = new Texture2D(copiedTexture.width, copiedTexture.height);
		//Choose your filtermode and wrapmode here.
		texture.filterMode = FilterMode.Point;
		texture.wrapMode = TextureWrapMode.Clamp;

		// Going through each pixel
		int y = 0;
		Color currentPixelColor;
		while (y < texture.height) {
			int x = 0;
			while (x < texture.width) {
				currentPixelColor = copiedTexture.GetPixel (x, y);

				if (currentPixelColor == Color.cyan) { // If the pixel is cyan
					texture.SetPixel (x, y, bodyColor); // Set this pixel to the body color
				} else if (currentPixelColor == Color.red) {
					texture.SetPixel (x, y, bodyColorDark); // Set this pixel to the dark body color
				} else if (currentPixelColor == Color.magenta) { // If the pixel is magenta
					texture.SetPixel (x, y, trimColor); // Set this pixel to the trim color
				} else {
					texture.SetPixel(x, y, copiedTexture.GetPixel(x,y));
				}
				++x;
			}
			++y;
		}

		// Name the texture, if you want.
		texture.name = ("multicolor_poncho");

		// This finalizes it. If you want to edit it still, do it before you finish with .Apply(). Do NOT expect to edit the image after you have applied. It did NOT work for me to edit it after this function.
		texture.Apply();

		// Return the variable, so you have it to assign to a permanent variable and so you can use it.
		Texture2D myTexture2D = texture;

		//Get your SpriteRenderer, get the name of the old sprite, create a new sprite, name the sprite the old name, and then update the material. If you have multiple sprites, you will want to do this in a loop- which I will post later in another post.
		string tempName = mySpriteRenderer.sprite.name;
		mySpriteRenderer.sprite = Sprite.Create (myTexture2D, mySpriteRenderer.sprite.rect, new Vector2(0,1));
		mySpriteRenderer.sprite.name = tempName;

		mySpriteRenderer.material.mainTexture = myTexture2D;
	}

	public void InitPlayerTexture () {
		PaletteSwap.InitColorSwapTex (mySpriteRenderer);

		// Pull the colors from the GameManager
		Color bodyColor = new Color (GameManager.gameData.customPlayerColorBody [0],
			GameManager.gameData.customPlayerColorBody [1],
			GameManager.gameData.customPlayerColorBody [2], 1f);

		float h, s, v; // The Hue, Sat, Value of the bodyColor
		Color.RGBToHSV (bodyColor, out h, out s, out v);
		v /= 2f; // Divide the value in half
		Color bodyColorDark = Color.HSVToRGB (h, s, v); // Create a dark color from the body color

		if (bodyColorDark.r < .1f) {
			bodyColorDark.r = .1f;
		}

		Color trimColor = new Color (GameManager.gameData.customPlayerColorTrim [0],
			GameManager.gameData.customPlayerColorTrim [1],
			GameManager.gameData.customPlayerColorTrim [2], 1f);

		// Swap the colors
		PaletteSwap.SwapColor (PaletteSwap.SwapIndex.Body, bodyColor);
		PaletteSwap.SwapColor (PaletteSwap.SwapIndex.BodyDark, bodyColorDark);
		PaletteSwap.SwapColor (PaletteSwap.SwapIndex.Trim, trimColor);
		PaletteSwap.mColorSwapTex.Apply (); // Apply the texture
	}

	// Custom instructions for when the nighttime costumed player is destroyed
	private void OnDestroy () {
		if (playerCostume == Costume.NIGHT && SceneManager.GetActiveScene ().buildIndex == 6) {
			foreach (SpriteRenderer sr in GameObject.FindObjectsOfType<SpriteRenderer>()) {
				sr.material = darkMaterial;
			}
		}
	}

}
