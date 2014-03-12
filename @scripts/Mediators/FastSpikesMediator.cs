using UnityEngine;
using System.Collections;

public class FastSpikesMediator : MonoBehaviour {
	
	public float OpenY = 0f;
	
	public float ClosedY = 0f;
	
	private float tmpOpen = 0f;
	
	private float tmpClose = 0f;
	
	private float timeSinceStart = 0f;
	
	private bool flag = false;
	
	// Use this for initialization
	void Start () 
	{
		tmpOpen = ClosedY;
			
		tmpClose = OpenY;
	}
	
	// Update is called once per frame
	void Update () 
	{
		timeSinceStart += Time.deltaTime * 4f;
		
		transform.localPosition = new Vector3(0f, Mathf.Lerp(tmpOpen,tmpClose, timeSinceStart), 0.4f);
		
		if(timeSinceStart > 1)
		{
			if(flag)
			{
				tmpOpen = ClosedY;
				
				tmpClose = OpenY;
				
				flag = false;
			}
			else
			{
				tmpOpen = OpenY;
				
				tmpClose = ClosedY;
				
				flag = true;
			}
			
			timeSinceStart = 0f;
		}
	}
	
	void OnTriggerEnter(Collider other)
	{
		UnitPlayer player = other.gameObject.GetComponent<UnitPlayer>();
		
		if(player!=null)
		{
			player.Die();	
		}
		
	}
}
