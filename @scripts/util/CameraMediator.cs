using UnityEngine;
using System.Collections;

public class CameraMediator : MonoBehaviour 
{
	Vector3 focusPosition;
	
	public float speed = 1;
	
	private int direction = 1;
	
	private Transform currentFocus;
	
	public float offsetX = 0;
	
	public float offsetY = 0;
	
	public float MinY = -5f;
	
	public float MaxY = 100f;
	
	void Awake()
	{
		currentFocus = null;
		
		Messenger.AddListener<Transform>(CameraEvent.Focus, FocusEventHandler);
		
		Messenger.AddListener<Vector3>(CameraEvent.StrictFocus, StrictFocusEventHandler);
		
		Messenger.AddListener(CameraEvent.Unfocus, Unfocus);
		
		Messenger.AddListener<int>(CameraEvent.ChangeDirection, ChangeDirectionEventHandler);
	}
	
	void FocusEventHandler( Transform transform )
	{
		currentFocus = transform;
		
		focusPosition = currentFocus.position;
	}
	
	void StrictFocusEventHandler(Vector3 position)
	{
		if(position != null)
		{
			focusPosition = position;
		}
	}
	
	void Unfocus()
	{
		currentFocus = null;
	}
	
	void ChangeDirectionEventHandler(int direction)
	{
		//
		// 1 for right
		// -1 for left
		//
	
		this.direction = direction;
	}
	
	// Update is called once per frame
	void Update ()
	{		
		if(currentFocus == null)
		{
			return;
		}
		
		focusPosition = currentFocus.position;
		
		Vector3 toPos = new Vector3( focusPosition.x + (direction * offsetX), Mathf.Clamp(focusPosition.y + offsetY, MinY, MaxY), transform.position.z );
		
		Vector3 pos =  Vector3.Lerp( transform.position, toPos, Time.deltaTime * speed );
		
		transform.position = pos;
	}
	
	void OnDisable()
	{
		Debug.Log("camara destruida");
	}
}