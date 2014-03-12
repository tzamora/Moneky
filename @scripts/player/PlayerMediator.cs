using UnityEngine;
using System.Collections;
using System;
using System.Linq;

[FlagsAttribute()] 
public enum PlayerAnimationEnum
{
	None = 0x0,
	Run = 0x1,
	Jump = 0x2,
	SuperJump = 0x4,
	Falling = 0x8
}

[RequireComponent( typeof( CharacterController ) )]
public class PlayerMediator : MonoBehaviour 
{
	public bool superJump = false;
	
	public float SPEED = 3.5f;
	
	public float playerSpeed = 2f;
	
	public float playerJumpSpeed = 8f;
	
	public float maxYSpeed = 8f;
		
	public float playerGravity = 20.0f;

	public Vector3 move = Vector3.zero;
	
	public float pushVelocity = -0.3f;
	
	protected Vector3 gravity = Vector3.zero;
	
	protected CharacterController characterController;
	
	protected bool jump = false;
		
	protected float inputX = 0f;
	
	protected float timeHoldJump = 0f;
	
	protected float maxHoldTime = 0.3f;
	
	//protected float jump
	
	// Use this for initialization
	public virtual void Start ()
	{
		characterController = GetComponent<CharacterController>();
		
		Physics.gravity = new Vector3( 0f, playerGravity, 0f);
		
		gravity = Physics.gravity;
	}
	
	// Update is called once per frame
	public virtual void Update () 
	{	
		//
		// move the character
		//
		inputX = Input.GetAxis("Horizontal");
		
		if(Input.GetKey(KeyCode.Space))
		{
			//
			// while holding jump
			//
			
			timeHoldJump += Time.deltaTime;
			
			if(timeHoldJump > maxHoldTime)
			{
				jump = false;	
			}
			else
			{
				jump = true;
			}
		}
		else
		{
			jump = false;
			
			timeHoldJump = 0;
		}
		
		MoveCharacter();
	}
	
	private void MoveCharacter()
	{
		move.x = inputX * playerSpeed;
		
		if(characterController.isGrounded)
		{
			move.y = 0;
		}
		
		if(jump)
		{
			jump = false;
			
			if(superJump)
			{
				//playerJumpSpeed *= 1.7f;
			}
			
			move.y += playerJumpSpeed;
			
			move.y = Mathf.Clamp(move.y,0f, maxYSpeed);
		}
		
		Debug.Log("---->" + move);
		
		move.y += playerGravity * Time.deltaTime;
		
		characterController.Move( move * Time.deltaTime );
		
				
		//
		// keep the position of the player fixed on zero in z axis 
		//
		
		transform.position = new Vector3(transform.position.x, transform.position.y, 0);
	}
}
