using UnityEngine;
using System.Collections;

public class AboutButtonMediator : MonoBehaviour {
	
	tk2dUIItem button;
	
	private float timeSinceStart;
	
	bool move = false;
	
	// Use this for initialization
	void Start () 
	{
		button = GetComponent<tk2dUIItem>();
		
		button.OnClick += OnClickEventHandler;
	}
	
	void OnDestroy()
	{
		button.OnClick -= OnClickEventHandler;
	}
	
	void OnClickEventHandler()
	{
		timeSinceStart = 0;
		
		move = true;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(move)
		{
			timeSinceStart += Time.deltaTime;
			
			Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, new Vector3(50f, 0f, -10), timeSinceStart);
			
			if(timeSinceStart > 1f)
			{
				move = false;
			}
		}
		
		
	}
}
