using UnityEngine;
using System.Collections;

public class ExplosiveMonkeyMediator : MonkeyMediator 
{	
	public float power = 30f;
	
	// Use this for initialization
	public override void Execute (GameObject player)
	{
		base.Execute (player);
		
		UnitPlayer unitPlayer = player.GetComponent<UnitPlayer>();
		
		rigidbody.velocity = new Vector3( unitPlayer.side * 1, 1, 0f) * power;
	}
}
