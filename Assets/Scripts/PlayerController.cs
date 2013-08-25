using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;

    public float gravity = 1f;
    public float horizontalSpeed = 1;
    public float jumpSpeed = 1;
    public float speedMultiplier = 2;
    public float jumpAccelerationSpeed = 3;

    public int money = 0;

    public AudioClip jumpSound;

    private float jumpTime = .2f;
    private bool isJumping = false;

    private float speedBoostTimer = 0;
    private bool hasSeepdBoost = false;

    private bool noGravity = false;

    private Vector3 movementDirection = Vector3.zero;

	private void Start ()
	{
	    WorldManager.PlayerController = this;
	    characterController = GetComponent<CharacterController>();
	}
	
	private void Update ()
	{
        if (Input.GetKeyDown(KeyCode.F) && Debug.isDebugBuild)
        {
            noGravity = !noGravity;
        }

        if (isJumping && !Input.GetKey(KeyCode.Space))
        {
            isJumping = false;
            jumpTime = .2f;
        }

	    if (characterController.isGrounded)
	    {
	        movementDirection = Vector3.zero;
            
	        if (Input.GetKey(KeyCode.Space))
	        {
	            isJumping = true;
                audio.PlayOneShot(jumpSound, 2);
	        }

	    }

        movementDirection.x = Input.GetAxis("Horizontal") * horizontalSpeed;
        
        if (isJumping)
        {
            jumpTime += Time.deltaTime * jumpAccelerationSpeed * speedMultiplier;
            movementDirection.y = Mathf.Lerp(0, jumpSpeed, jumpTime);

            if (jumpTime >= 1)
            {
                isJumping = false;
                jumpTime = .2f;
            }
        }

	    if (noGravity)
	    {
            movementDirection.y = Input.GetAxis("Vertical") * horizontalSpeed;
	    }
	    else
	    {
	        movementDirection.y -= gravity*Time.deltaTime;
	    }

	    if (characterController.Move(movementDirection*Time.deltaTime*speedMultiplier).Equals(CollisionFlags.CollidedAbove))
	    {
            Debug.Log("Aborting jump");
	        isJumping = false;
	        jumpTime = .2f;
	    }

        // SPEED BOOST CODE
        if (hasSeepdBoost)
        {
            speedBoostTimer -= Time.deltaTime;
            if (speedBoostTimer <= 0)
            {
                hasSeepdBoost = false;
                speedMultiplier = 1;
            }
        }
	}

    public void kill()
    {
        Destroy(gameObject);

        WorldManager.GameManager.switchGameState(GameManager.GameState.DeathScreen);
    }

    public void boostSpeed(float multiplier, float time)
    {
        speedMultiplier = multiplier;
        hasSeepdBoost = true;
        speedBoostTimer = time;
    }
}
