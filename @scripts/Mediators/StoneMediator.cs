using UnityEngine;
using System.Collections;

public class StoneMediator : MonoBehaviour 
{
	public GameObject ExplosionParticles;
	
	public AudioClip ExplosionSound;
	
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
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
