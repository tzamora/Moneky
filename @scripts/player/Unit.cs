using UnityEngine;
using System.Collections;
using System;
using System.Linq;

//[RequireComponent( typeof( CharacterController ) )]
public class Unit : MonoBehaviour 
{
	public float SPEED = 3.5f;
	
	public float playerSpeed = 2f;
	
	public float playerJumpSpeed = 8f;
		
	public float playerGravity = 20.0f;

	private bool running = false;
	
	private bool jumping = false;

	private bool falling = false;
	
	public float pushPower = 2.0F;		
	
	public Vector3 move = Vector3.zero;
	
	public float pushVelocity = -0.3f;
	
	protected Vector3 gravity = Vector3.zero;
	
	protected CharacterController characterController;
	
	protected bool jump = false;
	
	protected bool superJump = false;
	
	private tk2dSpriteAnimator playerAnim;
	
	protected float inputX = 0f;
	
	private bool slowDown = false;
	
	private float zPosition = 0f;
	
	private bool crouched = false;
	
	private bool standUp = false;
	
	private tk2dSprite frameTrailSprite;
	
	public string MonkeyColorPrefix = "";
	
	protected float CurrentDeltaTimeMultiplier;

	// Use this for initialization
	public virtual void Start ()
	{
		//
		// get the tk2dAnimatedSprite component 
		//
		
		playerAnim = GetComponent<tk2dSpriteAnimator>();
		
		//
		// set the initial player sprites
		//
		
		playerAnim.Play( GetPlayerAnimationPrefix() + "idle");
		
		//
		// search for the character controller component
		//
		
		characterController = GetComponent<CharacterController>();
		
		if(!characterController)
		{
			Debug.Log( "The character controller component has not been found" );
		}
		
		Physics.gravity = new Vector3( 0f, playerGravity, 0f);
		
		gravity = Physics.gravity;
		
		CurrentDeltaTimeMultiplier = 1;
	}
	
	// Update is called once per frame
	public virtual void Update () 
	{
		//
		// set the current animation
		//
		
		PlayAnimations();
		
		//
		// move the character
		//
		
		MoveCharacter();
	}

	private void PlayAnimations()
	{
		// Debug.Log("velocity " + characterController.velocity);
		
		if( Mathf.Abs( characterController.velocity.x ) > 0f )
		{
			playRunAnimation();
		}
		else
		{
			if(characterController.isGrounded)
			{
				PlayIdleAnimation();
			}
		}
		
		if(!characterController.isGrounded)
		{
			if(characterController.velocity.y > 0)
			{
				playJumpAnimation();
			} 
			else if(characterController.velocity.y < 0)
			{	
				playFallAnimation();
			}
		}
		else
		{
			falling = false;
		}
	}
	
	private void MoveCharacter()
	{
		//Debug.Log("riooir" + move);
		
		//
		// check the direction to see if we need to flip
		// the  sprite
		//
		
		if(characterController.isTouchingWall())
		{
			//playerSpeed = -(playerSpeed*0.2f);
		}
		
		if(slowDown)
		{
			playerSpeed = Mathf.Lerp(playerSpeed, 0, Time.deltaTime*CurrentDeltaTimeMultiplier*3);
		}
		else
		{
			if(playerSpeed < SPEED || playerSpeed > SPEED)
			{
				//
				// return back to the original speed
				//
				
				playerSpeed = Mathf.Lerp(playerSpeed, SPEED, Time.deltaTime*CurrentDeltaTimeMultiplier*3);
			}
		}
		
		move.x = inputX * playerSpeed;
		
		if(inputX < 0)
		{
			playerAnim.Sprite.FlipX = true;
			
			tk2dSpriteAnimationFrame frame = playerAnim.CurrentClip.GetFrame(0);
				
			Messenger.Broadcast(CameraEvent.ChangeDirection, -1);
		}
		
		if(inputX > 0)
		{
			playerAnim.Sprite.FlipX = false;
				
			Messenger.Broadcast(CameraEvent.ChangeDirection, 1);
		}
		
		if(characterController.isGrounded)
		{
			move.y = 0;
		}
		
		if(jump)
			{
				jump = false;
				
				float jumpSpeed = playerJumpSpeed;
				
				if(superJump)
				{
					jumpSpeed *= 1.7f;
					
					superJump = false;
				}
				
				move.y += jumpSpeed;
			}
		
		move.y += playerGravity * Time.deltaTime*CurrentDeltaTimeMultiplier;
		
		characterController.Move( move * Time.deltaTime*CurrentDeltaTimeMultiplier );
		
				
		//
		// keep the position of the player fixed on zero in z axis 
		//
		
		transform.position = new Vector3(transform.position.x, transform.position.y, zPosition);
	}
	
	void SpawnFrameTrail()
	{
		//
		// Todo sell it
		// Spawn a new FrameTrail
		//
		
		
	}
	
	public void PushToFall()
	{
		//
		// when the player is pushed in its z
		// it will fall and loose the game
		//
		
		zPosition = 3f;
	}

	public void RestoreZ()
	{
		//
		// when the player is pushed in its z
		// it will fall and loose the game
		//
		
		zPosition = 0f;
	}
	
	/*public void ChangeDirection(float direction)
	{
		if(direction < 0)
		{
			playerAnim.Sprite.FlipX = true;	
			
			Messenger.Broadcast(CameraEvent.ChangeDirection, -1);
		}
		
		if(direction > 0)
		{
			playerAnim.Sprite.FlipX = false;	
				
			Messenger.Broadcast(CameraEvent.ChangeDirection, 1);
		}
	}*/
	
	private void playRunAnimation()
	{
		if(characterController.isGrounded)
		{
			if(!playerAnim.IsPlaying( GetPlayerAnimationPrefix() + "walk") )
			{
				playerAnim.Play( GetPlayerAnimationPrefix() + "walk" );
				
				playerAnim.AnimationCompleted = animationCompleteDelegate;
				
				running = true;
			}
		}
	}
	
	private void playJumpAnimation()
	{
		if(!playerAnim.IsPlaying( GetPlayerAnimationPrefix() + "jump_start") )
		{
			playerAnim.Play( GetPlayerAnimationPrefix() + "jump_start" );
			
			playerAnim.AnimationCompleted = null;
			
			running = false;
		}
	}
	
	private void PlayIdleAnimation()
	{
		if(!playerAnim.IsPlaying( GetPlayerAnimationPrefix() + "idle") )
		{
			playerAnim.Play( GetPlayerAnimationPrefix() + "idle" );
			
			playerAnim.AnimationCompleted = null;
			
			running = false;
		}
	}

	private void playFallAnimation()
	{		
		if(!falling)
		{
			falling = true;
			
			if(!playerAnim.IsPlaying( GetPlayerAnimationPrefix() + "jump_end") )
			{
				playerAnim.Play( GetPlayerAnimationPrefix() + "jump_end" );
				
				playerAnim.AnimationCompleted = null;
				
				running = false;
			}
		}
	}

	void animationCompleteDelegate(tk2dSpriteAnimator sprite, tk2dSpriteAnimationClip clip)
	{
		if (running)
        {
		playerAnim.Play( GetPlayerAnimationPrefix() + "walk");
        }
		else
		{
			playerAnim.Play( GetPlayerAnimationPrefix() + "idle");
		}
	}

	private string GetPlayerAnimationPrefix()
	{
		return MonkeyColorPrefix;
	}
	
	public void SlowDown(bool slow)
	{
		slowDown = slow;
	}
	
	public void SpeedUp()
	{
		//playerSpeed = Mathf.Lerp(playerSpeed, SPEED, Time.deltaTime*2);
	}
	
	private float crouchHeight = 0.37f;
	
	public const float standUpHeight = 1.1f;
	
	public void Crouch(bool crouch)
	{
		//
		// if we are moving dont crouch
		//
		
		if( Mathf.Abs( characterController.velocity.x ) > 0f )
		{
			crouched = false;
			
			return;
		}
		
		if(crouch)
		{	
			characterController.height = standUpHeight - crouchHeight;
			
			crouched = true;
		}
		else
		{
			if(crouched)
			{
	        	characterController.height = standUpHeight;
				
				Vector3 currentPosition = transform.position;
				
				currentPosition.y += crouchHeight/2f;
					
				transform.position = currentPosition;
				
				crouched = false;
			}
		}
	}
	
	void OnControllerColliderHit(ControllerColliderHit hit) 
	{
        /*Rigidbody body = hit.collider.attachedRigidbody;
		
        if (body == null || body.isKinematic)
		{
			return;
		}
            
		if (hit.moveDirection.y < pushVelocity)
		{
			return;
		}
        
		Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
		
        body.velocity = pushDir * pushPower;*/
    }
	
	public tk2dSprite FrameTrailSprite {
		get {
			return this.frameTrailSprite;
		}
		set {
			frameTrailSprite = value;
		}
	}	
}
