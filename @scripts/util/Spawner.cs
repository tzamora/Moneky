using UnityEngine;
using System.Collections;

public class Spawner : MonoSingleton<Spawner> {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public static Object Spawn(GameObject prefab)
	{
		return GameObject.Instantiate( prefab, Vector3.zero, Quaternion.identity );
	}
	
	public static Object Spawn(GameObject prefab, Vector3 position, Quaternion rotation)
	{
		return GameObject.Instantiate( prefab, position, rotation );
	}
}
