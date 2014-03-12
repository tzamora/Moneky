using UnityEngine;
using System.Collections;

public class StartGameButtonMediator : MonoBehaviour {
	
	private tk2dUIItem buttonItem;
	
	// Use this for initialization
	void Start () 
	{
		buttonItem = GetComponent<tk2dUIItem>();
		
		buttonItem.OnClick += OnClickEventHandler;
	}
	
	void OnClickEventHandler()
	{
		//
		// load the first level
		//
		
		CameraFade.StartAlphaFade(Color.white, false, 2f, 0f, () => { Application.LoadLevel("ISR.GameLevel0"); });
	}
}
