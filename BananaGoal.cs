using UnityEngine;
using System.Collections;

public class BananaGoal : MonoBehaviour {
	
	public GameObject GoalViewPrefab;
	
	private bool showGoalView = false;
	
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.Rotate(Vector3.up);
	}
	
	void OnTriggerEnter(Collider other)
	{
		//
		// restart the time scale
		//
		
		Time.timeScale = 1.0f;
		
		//
		// spawn the view
		//
		if(!showGoalView)
		{
			Spawner.Spawn( GoalViewPrefab, Camera.main.transform.position + new Vector3(0f, 0f, 5f), Quaternion.identity );	
			
			showGoalView = true;
		}
		
	}
}
