using UnityEngine;
using System.Collections;

public class BackToMenuButtonMediator : MonoBehaviour {
	
	tk2dUIItem uiItem;
	
	// Use this for initialization
	void Start () 
	{
		uiItem = GetComponent<tk2dUIItem>();
			
		uiItem.OnClick += OnClickEventHandler;
	}
	
	void OnClickEventHandler()
	{
		CameraFade.StartAlphaFade(Color.white, false, 2f, 0f, () => { Application.LoadLevel("ISR.Intro"); });
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
