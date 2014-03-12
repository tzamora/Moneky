using UnityEngine;
using System.Collections;

public class MegaJumpMonkeyMediator : MonkeyMediator 
{	
	public int side = 0;
	
	public float jumpPower = 30f;
	
	public GameObject MonkeyDestroyParticlesPrefab;
	
	public AudioClip TrampolineSound;
	
	public override void Execute (GameObject player)
	{
		base.Execute (player);
		
		//
		// place the monkey a few steps in front of me
		//
		
		UnitPlayer unitPlayer = player.GetComponent<UnitPlayer>();
		
		transform.position = player.transform.position + new Vector3(unitPlayer.side * 1f, 0f, 0f);
	}
	
	void OnTriggerEnter(Collider other)
	{
		//
		// get the player and make it jump
		//
		
		UnitPlayer player = other.gameObject.GetComponent<UnitPlayer>();
		
		if(player != null)
		{
			player.SuperJump();
			
			SoundManager.Get.PlayClip(TrampolineSound, false);
			
			GameObject particleGO = (GameObject) Spawner.Spawn(MonkeyDestroyParticlesPrefab, transform.position, Quaternion.identity);
			
			ParticleSystem particles = particleGO.GetComponent<ParticleSystem>();
			
			Destroy(particles, particles.duration);
			
			Destroy(this.gameObject, particles.duration);
		}
	}
	
	public float speed = 6.0F;
	
    public float jumpSpeed = 8.0F;
    
	public float gravity = 15.0F;
    
	private Vector3 moveDirection = Vector3.zero;
    
	void Update() 
	{
        CharacterController controller = GetComponent<CharacterController>();
        
        moveDirection.y -= gravity * Time.deltaTime;
        
		controller.Move(moveDirection * Time.deltaTime);
    }
}
