using UnityEngine;
using System.Collections;

public class BossAIMediator : MonoBehaviour {
	
	private float timeSinceStart = 0f;
	
	public Vector3 StartPosition = Vector3.zero;
	
	public Vector3 EndPosition = Vector3.zero;
	
	public GameObject BossBigStone;
	
	public GameObject BossSmallStone;
	
	// Use this for initialization
	IEnumerator Start () 
	{
		EndPosition = new Vector3(45f, -10.12f, 0);
		
		var pointA = transform.position;
		
		yield return StartCoroutine( Shake(0.5f, 0.3f) );
	
		yield return new WaitForSeconds(0.7f);
	
		yield return StartCoroutine( Shake(0.5f, 0.3f) );
	
		yield return new WaitForSeconds(0.7f);
	
		yield return StartCoroutine( Shake(0.5f, 0.3f) );
		
		yield return StartCoroutine(MoveObject(transform, StartPosition, EndPosition, 3.0f));
		
		//
		// attack
		//
		
		while(true)
		{
			Attack();
			
			yield return new WaitForSeconds(3f);
		}
		
	}
	
	void Attack()
	{
		//
		// spawn a random number of rocks and throw them at diferent velocities
		//
		
		Vector3 spawnPoint = transform.position + new Vector3(0f, 3f, 0f);
		
		//GameObject bigStone = (GameObject) Spawner.Spawn(BossBigStone, spawnPoint, Quaternion.identity);
		
		GameObject smallStone = (GameObject) Spawner.Spawn(BossSmallStone, spawnPoint, Quaternion.identity);
		
		//bigStone.rigidbody.velocity = new Vector3( -1f, Random.Range(0.8f, 1f), 0f) * Random.Range(5f, 20f);
		
		smallStone.rigidbody.velocity = new Vector3( -1f, Random.Range(0.8f, 1f), 0f) * Random.Range(15f, 20f);
	}
	
	IEnumerator MoveObject (Transform transforms, Vector3 startPos,  Vector3 endPos, float time) 
	{
	    float i = 0f;
		
	    float rate = (float) 1.0/time;
	    
		while (i < 1.0) 
		{
	        i += Time.deltaTime * rate;
			
			transform.position = Vector3.Lerp(startPos, endPos, i);
	        
			yield return null; 
	    }
	}
	
	void Entrance()
	{
		//
		// the boss entrance
		//
		
		timeSinceStart += Time.deltaTime;
		
		transform.position = Vector3.Lerp(StartPosition, EndPosition, timeSinceStart);
	}
	
	IEnumerator Shake(float duration, float magnitude) 
	{    
	    float elapsed = 0.0f;
	    
	    Vector3 originalCamPos = Camera.main.transform.position;
	    
	    while (elapsed < duration) 
		{
	        elapsed += Time.deltaTime;          
	        
	        float percentComplete = elapsed / duration;        
			
	        float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);
	        
	        // map noise to [-1, 1]
	        float x = Random.value * 2.0f - 1.0f;
			
	        float y = Random.value * 2.0f - 1.0f;
	        
			x *= magnitude * damper;
	        
			y *= magnitude * damper;
	        
	        Camera.main.transform.position = originalCamPos + new Vector3(x, y, 0);
			
			Debug.Log(x + " 0000 " + y);
	            
	        yield return null;
	    }
	    
	    Camera.main.transform.position = originalCamPos;
	}
}
