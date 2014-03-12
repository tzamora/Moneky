using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class UnitPlayer : Unit
{
	public float TimeScaleMultiplier = 4;
	
	public float TimeScaleReduction = 0.25f;
	
	public bool enabledUnitPlayer = false;

	private Transform currentRespawner;
	
	public GameObject BombPrefab;
	
	public GameObject ChargingForcePrefab;
	
	public GameObject wallCollisionParticlePrefab;
	
	private GameObject currentChargingForceGO;

	private bool isDead = false;
	
	private bool bombAlive = false;
	
	public int side = 1;
	
	private float holdingChargeTime = 0f;
	
	public float ChargingTime = 2f;
	
	public AudioClip JumpSound;
	
	public AudioClip ThrowSound;
	
	public AudioClip CatchMonkeySound;
	
	public GameObject DieExplosionParticlePrefab;
	
	//
	// monkeys
	//
	
	public List<MiniMonkeyMediator> MyTimeMonkyes;
	
	public List<MiniMonkeyMediator> MyJumpMonkyes;
	
	public List<MiniMonkeyMediator> MyExplosionMonkyes;
	
	private List<MiniMonkeyMediator> CurrentMonkeyList;
	
	public MonkeyTypeEnum CurrentMonekyType = MonkeyTypeEnum.TimeMonkey;
	
	void Awake()
	{
		Messenger.AddListener<Vector2>(InputEvent.Move, MoveEventHandler);
		
		Messenger.AddListener(InputEvent.Jump, JumpEventHandler);
		
		Messenger.AddListener(InputEvent.Throw, ThrowEventHandler);
		
		Messenger.AddListener(InputEvent.ChangeMonkey, ChangeMonkeyEventHandler);
		
		CurrentMonkeyList = new List<MiniMonkeyMediator>();
	}
	
	void OnDestroy()
	{
		if(Messenger.eventTable.ContainsKey(InputEvent.Jump))
		{
			Messenger.RemoveListener(InputEvent.Jump, JumpEventHandler);	
		}
	
		Messenger.RemoveListener(InputEvent.ChangeMonkey, ChangeMonkeyEventHandler);
		
		Messenger.RemoveListener(InputEvent.Throw, ThrowEventHandler);
	}
	
	private void ChangeMonkeyEventHandler()
	{
		//
		// change in this order explosion, jump, time
		//
		
		switch(CurrentMonekyType)
		{
			case MonkeyTypeEnum.ExplosiveMonkey:
			{
				CurrentMonekyType = MonkeyTypeEnum.MegaJumpMonkey;
				CurrentMonkeyList = MyJumpMonkyes;
				break;
			}
			case MonkeyTypeEnum.MegaJumpMonkey:
			{
				CurrentMonekyType = MonkeyTypeEnum.TimeMonkey;
				CurrentMonkeyList = MyTimeMonkyes;
				break;
			}
			case MonkeyTypeEnum.TimeMonkey:
			{
				CurrentMonekyType = MonkeyTypeEnum.ExplosiveMonkey;
				CurrentMonkeyList = MyExplosionMonkyes;
				break;
			}
		}
		
		UpdateNextToThrowMonkeyUI();
	}
	
	private void ChangeCurrentMonkeyList(MonkeyTypeEnum monkeyType)
	{
		CurrentMonekyType = monkeyType;
		
		switch(monkeyType)
		{
			case MonkeyTypeEnum.ExplosiveMonkey:
			{
				CurrentMonkeyList = MyExplosionMonkyes;
				break;
			}
			case MonkeyTypeEnum.TimeMonkey:
			{
				CurrentMonkeyList = MyTimeMonkyes;
				break;
			}
			case MonkeyTypeEnum.MegaJumpMonkey:
			{
				CurrentMonkeyList = MyJumpMonkyes;
				break;
			}
		}
	}
	
	private void UpdateNextToThrowMonkeyUI()
	{
		Messenger.Broadcast("next_monkey", CurrentMonekyType, CurrentMonkeyList.Count);
	}
	
	private void ThrowEventHandler()
	{
		//
		// get the first minimonkey and remove it
		//
		
		MiniMonkeyMediator monkey = null;
		
		if(CurrentMonkeyList.Count > 0)
		{
			monkey = CurrentMonkeyList.Pop();
		}
		else
		{
			return;
		}
		
		//
		// update the next monkey to throw view
		//
		
		UpdateNextToThrowMonkeyUI();
		
		tk2dSprite miniMonkeySprite = monkey.GetComponent<tk2dSprite>();
		
		//
		// spawn a monkey 
		//
		
		GameObject monkeyGO = (GameObject) Spawner.Spawn(monkey.MonkeyTypePrefab, transform.position, Quaternion.identity);
		
		MonkeyMediator monkeyMediator = monkeyGO.GetComponent<MonkeyMediator>();
		
		monkeyMediator.Execute(this.gameObject);
		
		tk2dSprite monkeySprite = monkeyGO.GetComponent<tk2dSprite>();
		
		monkeySprite.SetSprite(miniMonkeySprite.spriteId);
		
		Destroy(monkey.gameObject);
		
		SoundManager.Get.PlayClip(ThrowSound, false);
	}
	
	private void ToggleForceEventHandler(bool enable)
	{
		ToggleForce( enable );
	}
	
	public override void Start ()
	{
		base.Start ();
		
		Messenger.Broadcast(CameraEvent.Focus, transform);
		
		MyExplosionMonkyes = new List<MiniMonkeyMediator>();
		MyTimeMonkyes = new List<MiniMonkeyMediator>();
		
		MyJumpMonkyes = new List<MiniMonkeyMediator>();
		
		UpdateNextToThrowMonkeyUI();
	}
	
	// Update is called once per frame
	public override void Update () 
	{
		if(GameData.Get.CurrentGameState != GameStatesEnum.playing)
		{
			return;
		}
		
		base.Update();
		
		if(!renderer.IsVisibleFrom(Camera.main))
		{
			transform.renderer.enabled = false;

			//Die();
		}
		else
		{
			transform.renderer.enabled = true;
		}
		
		CheckGround();
		
		//HeaderHit();
		
		//
		// check the timeScale
		//
		
		if(Time.timeScale == TimeScaleReduction)
		{
			CurrentDeltaTimeMultiplier = TimeScaleMultiplier;
		}
		else
		{
			CurrentDeltaTimeMultiplier = 1f;
		}
	}
	
	void PlaceBombEventHandler()
	{
		PlaceBomb();
	}
	
	void CrouchEventHandler()
	{
		Crouch(true);
	}
	
	void StandUpEventHandler()
	{
		Crouch(false);
	}
	
	void MoveEventHandler(Vector2 direction)
	{
		//
		// here we will make the input increase based on the time
		// we keep pressed forward
		//
		
		this.inputX = direction.x;
		
		//
		// get the direction
		//
		
		if(inputX > 0)
		{
			side = 1;		
		}
		
		if(inputX < 0)
		{
			side = -1;
		}
	}
	
	void JumpEventHandler()
	{
		Jump();
		
		float pitch = UnityEngine.Random.Range(0.5f, 0.9f);
		
		SoundManager.Get.PlayClip(JumpSound, false, pitch);
	}
	
	public void OnTriggerEnter(Collider other)
	{
		//
		// when we finish with the collider
		//
		
		if(other.tag == "jumper")
		{
			//
			// make the player do a super jump
			//
			
			SuperJump();
		}
		
		if(other.tag == "item")
		{
			//
			// since we know is a item get the item component
			//
			
			Messenger.Broadcast(SoundEvent.PlaySound, "key");
		}
		
		MiniMonkeyMediator miniMonkey = other.gameObject.GetComponent<MiniMonkeyMediator>();
		
		if(miniMonkey != null)
		{
			ChangeCurrentMonkeyList(miniMonkey.MonkeyType);
			
			if(!CurrentMonkeyList.Contains(miniMonkey))
			{
				CurrentMonkeyList.Add( miniMonkey );
				
				miniMonkey.gameObject.layer = 8;
				
				miniMonkey.id = CurrentMonkeyList.Count;
			
				miniMonkey.EnableMiniMonkey(this);
				
				SoundManager.Get.PlayClip(CatchMonkeySound, false);
				
				UpdateNextToThrowMonkeyUI();
			}
		}
	}
	
	public void Jump()
	{
		if(characterController.isGrounded)
		{
			jump = true;
		}
	}
	
	public void SuperJump()
	{
		jump = true;
		
		superJump = true;
	}
	
	public void Revive()
	{
		//
		// search for the latest spawner you have crossed
		//
		if(currentRespawner)
		{
			transform.position = currentRespawner.transform.position;

			Messenger.Broadcast(CameraEvent.Focus, this.transform);
			
			//
			// set again the playerZ to 0
			//
			
			RestoreZ();
			
			Invoke("IsDeadToTrue",2.0f);
		}
	}
	
	public void IsDeadToTrue()
	{
		isDead = false;
	}
	
	void Kill()
	{
		Messenger.Broadcast(CameraEvent.Unfocus);
		
		isDead = true;
				
		//
		// make the player fall
		//
		
		PushToFall();
	}
	
	/// <summary>
	/// Checks the ground. 
	/// </summary>
	public void CheckGround()
	{
		//
		// this code help us to keep the enemy from
		// falling from the platforms
		// throw a raycast
		// shotting downwards
		//
		
		Vector3 rayPosition = transform.position;
		
		rayPosition.y = rayPosition.y;
		
		float direction = -10f;
		
		//rayPosition.y = transform.position.y + 0.1f;
		
		Vector3 down = transform.TransformDirection(new Vector3(0f,direction,0f));
		
		Debug.DrawRay (rayPosition, down, Color.green);

		RaycastHit hit;  
		
		if (Physics.Raycast(rayPosition, down, out hit, Mathf.Abs( direction )))
		{
			if(hit.transform)
			{
				//
				// get the raycastlistener and invoke the OnRaycastHit event
				//
				
				RaycastListener raycastListener = hit.transform.GetComponent<RaycastListener>();
				
				if(raycastListener != null && raycastListener.OnRaycastHit != null)
				{
					raycastListener.OnRaycastHit(this.gameObject);
				}
			}
		}
	}
	
	public void HeaderHit()
	{
		//
		// this code help us to keep the enemy from
		// falling from the platforms
		// throw a raycast
		// shotting downwards
		//
		Vector3 rayPosition = transform.position;
		
		float direction = 2f;
		
		//rayPosition.y = transform.position.y + 0.1f;
		
		Vector3 down = transform.TransformDirection(new Vector3(0f,direction,0f));
		
		Debug.DrawRay (rayPosition, down, Color.red);
		
		RaycastHit[] raycasts = Physics.RaycastAll(rayPosition, down, Mathf.Abs( direction ));
		
		for(int i = 0; i < raycasts.Length; i++)
		{
			if(raycasts[i].transform)
			{
				Debug.Log("-00000");
				
				//
				// get the raycastlistener and invoke the OnRaycastHit event
				//
				
				SpecialStoneMediator specialStoneMediator = raycasts[i].transform.GetComponent<SpecialStoneMediator>();
				
				StoneMediator stoneMediator = raycasts[i].transform.GetComponent<StoneMediator>();
				
				if(specialStoneMediator != null)
				{
					//Debug.Log("chochando con piedra");
					
					Die();
				}
				
				if(specialStoneMediator != null)
				{
					Debug.Log("chochando con piedra");	
					
					Die();
				}
			}
		}
	}
	
	public void PlaceBomb()
	{
		//
		// spawn the bomb
		//
		
		Spawner.Spawn( BombPrefab, transform.position, Quaternion.identity );
		
		
		
		/*if(_playerInventory.Bombs > 0)
		{
			_playerInventory.Bombs--;
		}*/
	}
	
	public void ActivateBomb()
	{
		Messenger.Broadcast("activatebombs");
	}
	
	public void ToggleForce(bool enable)
	{
		if(bombAlive) 
		{
			ActivateBomb();
			
			bombAlive = false;
		}
		
		if(enable)
		{
			holdingChargeTime = Time.time;
			
			if(currentChargingForceGO == null)
			{
				currentChargingForceGO = (GameObject) Spawner.Spawn( ChargingForcePrefab, this.transform.position, Quaternion.identity );
				
				currentChargingForceGO.transform.parent = this.transform;
				
				currentChargingForceGO.transform.localPosition = Vector3.zero;
			}
		}
		else
		{
			//
			// when the button os released check the
			// amount of time it pased versus the time 
			// needed to spawn a bomb
			//
			
			if( (Time.time - holdingChargeTime) > ChargingTime)
			{
				Debug.Log("En serio!!!");
				
				if(!bombAlive)
				{
					PlaceBomb();
					
					bombAlive = true;
				}
			}
			
			holdingChargeTime = 0f;
			
			Destroy(currentChargingForceGO);
			
			currentChargingForceGO = null;
		}
	}
	
	public void Die()
	{
		if(isDead)
		{
			return;	
		}
		
		isDead = true;
		
		//
		// reset the timeScale in case we were affecting it
		//
		
		Time.timeScale = 1.0f;
		
		if(Messenger.eventTable.ContainsKey(InputEvent.Move))
		{
			Messenger.RemoveListener<Vector2>(InputEvent.Move, MoveEventHandler);
		}
		
		this.inputX = 0;
		
		//
		// spawn the die particle effect
		//
		
		Spawner.Spawn(DieExplosionParticlePrefab, transform.position, Quaternion.identity);
		
		tk2dSprite sprite = transform.gameObject.GetComponent<tk2dSprite>();
		
		sprite.color = new Color(1,1,1,0);
		
		Invoke("RestartLevel", 2f);
	}
	
	void RestartLevel()
	{
		CameraFade.StartAlphaFade(Color.white, false, 2f, 0f, () => { Application.LoadLevel(Application.loadedLevel); });	
	}
 }
