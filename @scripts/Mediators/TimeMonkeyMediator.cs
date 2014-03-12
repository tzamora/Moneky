using UnityEngine;
using System.Collections;

public class TimeMonkeyMediator : MonkeyMediator 
{	
	public float power = 5f;
	
	public GameObject TimeParticlePrefab;
	
	public AudioClip TimeFreezeSound;
	
	public float TimeScaleReduction = 0.25f;
	
	// Use this for initialization
	public override void Execute (GameObject player)
	{
		base.Execute (player);
		
		UnitPlayer unitPlayer = player.GetComponent<UnitPlayer>();
		
		rigidbody.velocity = new Vector3( unitPlayer.side * 0.5f, 1, 0f) * power;
		
		StartCoroutine(FreezeTime());
	}
	
	IEnumerator FreezeTime()
	{
		yield return new WaitForSeconds(1f);
		
		//
		// spawn the particle and destroy the monkey
		//
		
		Vector3 pos = new Vector3(transform.position.x, transform.position.y, 0f);
		
		GameObject particleGO = (GameObject) Spawner.Spawn( TimeParticlePrefab, pos, Quaternion.identity);
		
		ParticleSystem particles = particleGO.GetComponent<ParticleSystem>();
		
		particles.Play();
		
		SoundManager.Get.PlayClip(TimeFreezeSound, false);
		
		//Destroy(particles, particles.duration);
		
		Time.timeScale = TimeScaleReduction;
		
		yield return new WaitForSeconds(1f);
		
		Time.timeScale = 1f;
		
		Destroy(particles);
		
		Destroy(this.gameObject);
	}
}
