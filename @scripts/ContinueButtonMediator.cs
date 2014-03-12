using UnityEngine;
using System.Collections;

public class ContinueButtonMediator : MonoBehaviour {

	public tk2dUIItem continueButton;
	
	// Use this for initialization
	void Start () 
	{
		continueButton = GetComponent<tk2dUIItem>();
		
		continueButton.OnClick += ContinueEventHandler;
	}
	
	void ContinueEventHandler()
	{
		//
		// get the current level and go to the next one
		//
		
		int currentLevel = StaticData.CurrentLevel;
		
		CameraFade.StartAlphaFade(Color.white, false, 2f, 0f, () => { Application.LoadLevel("ISR.GameLevel" + (currentLevel + 1)); });
	}
}
