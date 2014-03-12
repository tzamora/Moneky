using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class UnityExtensions
{
	public static void Hide(this GameObject gameObject)
	{
		if(gameObject.renderer)
		{
			gameObject.renderer.enabled = false;
			
			gameObject.active = false;
		}
		
		foreach (Transform child in gameObject.transform)
		{
			child.gameObject.Hide();
		}
	}
	
	public static void Show(this GameObject gameObject)
	{
		if(gameObject.renderer)
		{
			gameObject.renderer.enabled = true;
			
			gameObject.SetActive(true);
		}
		
		foreach (Transform child in gameObject.transform)
		{
			child.gameObject.Show();
		}
	}
	
	public static bool isTouchingWall (this CharacterController characterController)
	{
		return (characterController.collisionFlags & CollisionFlags.Sides) != 0;
	}
	
	static readonly System.Random random = new System.Random ();

	public static void Shuffle<T> (this IList<T> list)
	{
		int n = list.Count;
		while (n > 1) {
			n--;
			int k = random.Next (n + 1);
			T value = list [k];
			list [k] = list [n];
			list [n] = value;
		}
	}
	
	/// <summary>
	/// Taken from http://stackoverflow.com/questions/9033/hidden-features-of-c/407325#407325
	/// instead of doing (enum & value) == value you can now use enum.Has(value)
	/// </summary>
	/// <typeparam name="T">Type of enum</typeparam>
	/// <param name="type">The enum value you want to test</param>
	/// <param name="value">Flag Enum Value you're looking for</param>
	/// <returns>True if the type has value bit set</returns>
	public static bool Has<T> (this System.Enum type, T value)
	{
		return (((int)(object)type & (int)(object)value) == (int)(object)value);
	} 

	public static void FadeSprite(this tk2dSprite sprite, float alpha)
	{
		Color spriteColor = sprite.color;
			
		sprite.color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, alpha);
	}
	
	//
	//checks if a renderer is visible from a specified camera
	//
	public static bool IsVisibleFrom(this Renderer renderer, Camera camera)
	{
		Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
		return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
	}
	
	/// <summary>
	/// Spawns the child game object in the center of the specified parent
	/// </summary>
	/// <returns>
	/// The new game object.
	/// </returns>
	/// <param name='parent'>
	/// Parent.
	/// </param>
	/// <param name='prefab'>
	/// Prefab.
	/// </param>
	public static GameObject SpawnChildGameObject(this GameObject parent, GameObject prefab)
	{
		GameObject go = (GameObject) Spawner.Spawn( prefab, Vector3.zero, Quaternion.identity);
		
		go.transform.parent = parent.transform;
		
		go.transform.localPosition = Vector3.zero;
		
		return go;
	}
	
	public static T Pop<T>(this List<T> theList)
	{
	 var local = theList[theList.Count - 1];
	 theList.RemoveAt(theList.Count - 1);
	 return local;
	}
      
	public static void Push<T>(this List<T> theList, T item)
	{
	    theList.Add(item);
	}
}
