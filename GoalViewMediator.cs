using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GoalViewMediator : MonoBehaviour {
	
	public tk2dUIItem continueButton;
	
	// Use this for initialization
	void Start () 
	{
		continueButton.OnClick += ContinueEventHandler;
	}
	
	void ContinueEventHandler()
	{
		//
		// go to the next level
		//
		
		
		
		string levelName = "ISR.GameLevel" + StaticData.CurrentLevel;
		
		Application.LoadLevel(Application.loadedLevel + 1);
	}
	
	void OnFadeFinish()
	{
		//List
		
		//string levelName = "ISR.GameLevel" + StaticData.CurrentLevel;
		
		//Debug.Log("the following level will be loaded: " + levelName);
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
