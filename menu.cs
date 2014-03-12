using UnityEngine;
using System.Collections;

public class menu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI () 
	{
		// Automatic Layout
		if(GUILayout.Button ("Level 0"))
		{
			CameraFade.StartAlphaFade(Color.white, false, 2f, 0f, () => { Application.LoadLevel(1); });
		}
		
		if(GUILayout.Button ("Level 1"))
		{
			CameraFade.StartAlphaFade(Color.white, false, 2f, 0f, () => { Application.LoadLevel(2); });
		}
		
		if(GUILayout.Button ("Level 2"))
		{
			CameraFade.StartAlphaFade(Color.white, false, 2f, 0f, () => { Application.LoadLevel(3); });
		}
		
		if(GUILayout.Button ("Level 3"))
		{
			CameraFade.StartAlphaFade(Color.white, false, 2f, 0f, () => { Application.LoadLevel(4); });
		}
		
		if(GUILayout.Button ("Level 4"))
		{
			CameraFade.StartAlphaFade(Color.white, false, 2f, 0f, () => { Application.LoadLevel(5); });
		}
		
		if(GUILayout.Button ("Level 5"))
		{
			CameraFade.StartAlphaFade(Color.white, false, 2f, 0f, () => { Application.LoadLevel(6); });
		}
		
	}
}
