using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float moveSpeed; // How fast you go
    private float moveSpeedStore;
  
    public float speedMultiplier; // Speeds the character up after running a certain distance.
    public float speedIncreaseMilestone; // The distance when the character speeds up.
    private float speedIncreaseMilestoneStore;

    private float speedMilestoneCount;
    private float speedMilestoneCountStore;


    public float jumpForce; // How high you jump

    /*
     * Ssay jumpTime is given a value of 5, hold the jump button and then jumpTime will start to count down.
     * The character will continue to rise up until the point jumpTime reaches 0.
     * We must then reset jumpTime or else it will be stuck at 0 and the character will no longer rise on the next jump.
     */
    public float jumpTime; // Hold down jump button, how long character will continue jumping/rising.
    private float jumpTimeCounter;

    private bool stoppedJumping;
    private bool canDoubleJump;

    private Rigidbody2D myRigidbody;

    public bool grounded;
    public LayerMask whatIsGround;
    public Transform groundCheck;
    public float groundCheckRadius;

    //private Collider2D myCollider;

    private Animator myAnimator;

    public GameManager theGameManager;

    public AudioSource jumpSound;
    public AudioSource doubleJumpSound;
    public AudioSource deathSound;

	// Use this for initialization
	void Start () {
        // Finds Rigidbody2D attached to character in Unity.
        myRigidbody = GetComponent<Rigidbody2D>();

        // Finds BoxCollider2D attached to character in Unity.
        //myCollider = GetComponent<Collider2D>();

        // Finds Animator attached to character in Unity.
        myAnimator = GetComponent<Animator>();

        jumpTimeCounter = jumpTime; // Initialise jumpTimeCounter at the start.

        speedMilestoneCount = speedIncreaseMilestone;

        moveSpeedStore = moveSpeed;
        speedMilestoneCountStore = speedMilestoneCount;
        speedIncreaseMilestoneStore = speedIncreaseMilestone;

        stoppedJumping = true;
	}
	
	// Update is called once per frame
	void Update () {
        // We want the character to only be allowed jump when he is touching the ground.
        // We make the character and ground two separate layers and if the layers touch (grounded = true) then the player can jump.
        // This returns either true or false whether the character is touching the ground.
        //grounded = Physics2D.IsTouchingLayers(myCollider, whatIsGround);

        // Character was being classed as 'grounded' if his arm or head touched the ground, this fixes/validates that he is ONNLY grounded when his FEET touch the platform.
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);


        if(transform.position.x > speedMilestoneCount)
        {
            speedMilestoneCount += speedIncreaseMilestone;

            speedIncreaseMilestone = speedIncreaseMilestone * speedMultiplier; // Makes the next milestone further away so you dont speed through the whole game super fast.

            moveSpeed = moveSpeed * speedMultiplier; // Speed increases after hitting a milestone distance.
        }

        // Endless runner so character always running on X-axis. 
        // No running on Y-axis only jumping, so no Y-axis input here.
        myRigidbody.velocity = new Vector2(moveSpeed, myRigidbody.velocity.y);

        // Any time the Spacebar or Mouse button is pressed our character will jump.
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            // If character is grounded is true then the character can jump.
            if (grounded == true)
            {
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
                stoppedJumping = false;
                jumpSound.Play(); // Plays jump sound.
            }

            if (!grounded && canDoubleJump)
            {
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
                jumpTimeCounter = jumpTime;
                stoppedJumping = false;
                canDoubleJump = false;
                doubleJumpSound.Play(); // Plays jump sound.
            }
        }

        if((Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0)) && !stoppedJumping)
        {
            if(jumpTimeCounter > 0)
            {
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
                jumpTimeCounter -= Time.deltaTime; // This counts the counter down so the character eventually stops rising when the jump button is held down.
            }
        }
        
        // Validation: jump validation.
        if(Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0))
        {
            jumpTimeCounter = 0; // When someone tries the hold the jump button again after already letting go, the character cannot jump any more extra.
            stoppedJumping = true;
        }

        // Validation: jump reset after character is grounded.
        if (grounded)
        {
            jumpTimeCounter = jumpTime;
            canDoubleJump = true;
        }

        myAnimator.SetFloat("Speed", myRigidbody.velocity.x);
        myAnimator.SetBool("Grounded", grounded);
	}

    // OnCollision built into Unity. If two colliders touch: 
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "KillBox") // If character enters area of death. (So we kow when to restart the game!)
        {
            theGameManager.RestartGame();
            moveSpeed = moveSpeedStore;
            speedMilestoneCount = speedMilestoneCountStore;
            speedIncreaseMilestone = speedIncreaseMilestoneStore;
            deathSound.Play();
        }
    }
}
