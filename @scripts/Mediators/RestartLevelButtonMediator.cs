using UnityEngine;
using System.Collections;

public class RestartLevelButtonMediator : MonoBehaviour {
	
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
		// restart the current level
		//
		
		CameraFade.StartAlphaFade(Color.white, false, 2f, 0f, () => { Application.LoadLevel(Application.loadedLevel); });
	}
}
