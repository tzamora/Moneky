using UnityEngine;
using System.Collections;
using System;

public class RaycastListener : MonoBehaviour
{
	/// <summary>
	/// Raises the raycast hit event.
	/// </summary>
	/// <typeparam name='GameObject'>
	/// The caster of the ray being hit here.
	/// </typeparam>
	public Action<GameObject> OnRaycastHit;
	
	public void Hit( GameObject raycaster ) 
	{
		if(OnRaycastHit != null)
		{
			OnRaycastHit( raycaster );	
		}
	}
}
