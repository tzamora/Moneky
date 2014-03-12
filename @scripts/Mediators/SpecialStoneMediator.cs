using UnityEngine;
using System.Collections;

public class SpecialStoneMediator : MonoBehaviour 
{
	public GameObject ExplosionParticles;
	
	public AudioClip ExplosionSound;
	
	private GameObject player;
	
	// Use this for initialization
	void Start () 
	{
		player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () 
	{
		float distance = player.transform.position.x - transform.position.x;
		
		//Debug.Log(distance + " ----- ");
		
		if( Mathf.Abs(distance) < 5f)
		{
			rigidbody.useGravity = true;
		}
	}
	
	void OnCollisionEnter(Collision other)
	{
		ExplosiveMonkeyMediator explosiveMonkey = other.gameObject.GetComponent<ExplosiveMonkeyMediator>();
		
		//
		// spawn 
		//
		
		if(explosiveMonkey != null)
		{
			GameObject explosion = (GameObject) Spawner.Spawn(ExplosionParticles, transform.position, Quaternion.identity);
			
			Destroy(explosion, explosion.GetComponent<ParticleSystem>().duration);
			
			SoundManager.Get.PlayClip(ExplosionSound, false);
			
			Destroy(explosiveMonkey.gameObject);
			
			Destroy(this.gameObject);
		}	
		
		//this.des
	}
}
