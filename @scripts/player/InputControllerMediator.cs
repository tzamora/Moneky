using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Input controller mediator.
/// handle the player input
/// </summary>
public class InputControllerMediator : MonoBehaviour 
{	
	float inputX = 0f;
	
	float inputY = 0f;
	
	public bool stopped = false;
	
	//public static List<MovementFrameVO> movementInFrames;
	
	public static List<Vector3> movementInFrames;

	// Use this for initialization
	void Start () 
	{
		movementInFrames = new List<Vector3>();
		
		//movementInFrames = new List<MovementFrameVO>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		ListenInput();
	}
	
	void ListenInput()
	{
		MovementFrameVO movementFrame = new MovementFrameVO();
		
		//
		// on each frame save each value of the input x and jump events to send them to the other mini monkeys
		//
		
		inputX = Input.GetAxisRaw("Horizontal");
		
		if(Input.GetKeyDown( KeyCode.Z ) || Input.GetKeyDown( KeyCode.Space ) || Input.GetKeyDown( KeyCode.W ) )
		{
			Messenger.Broadcast(InputEvent.Jump);
			
			movementFrame.Jump = true;
		}
		
		if(Input.GetKeyDown( KeyCode.X ) || Input.GetKeyDown( KeyCode.K ))
		{
			Messenger.Broadcast(InputEvent.Throw);
		}
		
		if(Input.GetKeyDown( KeyCode.C ) || Input.GetKeyDown( KeyCode.Escape ) || Input.GetKeyDown( KeyCode.O ) )
		{
			Messenger.Broadcast(InputEvent.ChangeMonkey);
		}
		
		Vector2 direction = new Vector2(inputX, 0f);
		
		Messenger.Broadcast(InputEvent.Move, direction);
		
		movementFrame.InputX = inputX;
		
		movementInFrames.Add( transform.position );
		
		//movementInFrames.Add( movementFrame );
	}
	
	void leftButtonClickDownHandler ()
	{
		inputX = -1;
		
		Debug.Log("left button pressed");
	}

	void leftButtonClickUpHandler ()
	{
		inputX = 0;
	}
	
	void rightButtonClickDownHandler ()
	{
		inputX = 1;
		
		Debug.Log("right button pressed");
	}
	
	void rightButtonClickUpHandler ()
	{
		inputX = 0;
	}
	
	void stopRunButtonClickHandler ()
	{
	}
	
	void jumpButtonClickHandler ()
	{
		Messenger.Broadcast(InputEvent.Jump);
	}
}
