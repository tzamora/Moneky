using UnityEngine;
using System.Collections;

public enum MonkeyTypeEnum
{
	ExplosiveMonkey,
	TimeMonkey,
	MegaJumpMonkey,
	none
}

public class MonkeySpawnerMediator : MonoBehaviour {
	
	public float RespawnTime = 5f;
	
	public MonkeyTypeEnum MonkeyType = MonkeyTypeEnum.ExplosiveMonkey;
	
	public int NumberOfMonkeys = 0;
	
	public GameObject ExplosionMonkeyPrefab;
	
	public GameObject MegaJumpMonkeyPrefab;
	
	public GameObject TimeMonkeyPrefab;
	
	// Use this for initialization
	void Start () 
	{
		if(NumberOfMonkeys > 0)
		{
			for(int i = 0; i < NumberOfMonkeys; i++)
			{
				SpawnMonkey();
			}	
		}
		else
		{
			InvokeRepeating("SpawnMonkey", RespawnTime, RespawnTime);
		}
		
	}
	
	void SpawnMonkey()
	{
		switch(MonkeyType)
		{
			case MonkeyTypeEnum.ExplosiveMonkey:
				Spawner.Spawn(ExplosionMonkeyPrefab, transform.position + new Vector3(0f, 1f, 0f), Quaternion.identity);
			break;
			case MonkeyTypeEnum.TimeMonkey:
				Spawner.Spawn(TimeMonkeyPrefab, transform.position + new Vector3(0f, 1f, 0f), Quaternion.identity);
			break;
			case MonkeyTypeEnum.MegaJumpMonkey:
				Spawner.Spawn(MegaJumpMonkeyPrefab, transform.position + new Vector3(0f, 1f, 0f), Quaternion.identity);
			break;
		}
	}
}
