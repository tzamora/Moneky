using UnityEngine;
using System.Collections;

public class NeedlesMediator : MonoBehaviour 
{
	void OnTriggerEnter(Collider other)
	{
		UnitPlayer player = other.GetComponent<UnitPlayer>();
		
		if(player)
		{
			//
			// destroy the player 
			//
			
			player.Die();
		}
	}
}
