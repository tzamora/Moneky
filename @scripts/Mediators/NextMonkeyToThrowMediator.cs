using UnityEngine;
using System.Collections;

public class NextMonkeyToThrowMediator : MonoBehaviour 
{	
	private tk2dSprite sprite; 
	
	public tk2dTextMesh MonkeyCounterLabel;
	
	void Awake()
	{
		sprite = GetComponent<tk2dSprite>();
		
		Messenger.AddListener<MonkeyTypeEnum, int>("next_monkey", SetNextMonkeyEventHandler);
	}
	
	void OnDisable()
	{
		Messenger.RemoveListener<MonkeyTypeEnum, int>("next_monkey", SetNextMonkeyEventHandler);
	}
	
	
	/*void SetNextMonkeyEventHandler(int spriteID)
	{
		if(spriteID > 0)
		{
			sprite.color = new Color(1,1,1,1);
			
			sprite.SetSprite(spriteID);	
		}
		else
		{
			sprite.color = new Color(1,1,1,0);
		}
		
	}*/
	
	void SetNextMonkeyEventHandler(MonkeyTypeEnum monkeyType, int monkeyCounter)
	{
		MonkeyCounterLabel.text = "x" + monkeyCounter;
		
		switch(monkeyType)
		{
			case MonkeyTypeEnum.ExplosiveMonkey:
				sprite.color = new Color(1,1,1,1);
				sprite.SetSprite("explosion_icon");
			break;
			case MonkeyTypeEnum.TimeMonkey:
				sprite.color = new Color(1,1,1,1);
				sprite.SetSprite("time_icon");
			break;
			case MonkeyTypeEnum.MegaJumpMonkey:
				sprite.color = new Color(1,1,1,1);
				sprite.SetSprite("jump_icon");
			break;
			case MonkeyTypeEnum.none:
				sprite.color = new Color(1,1,1,1);
				sprite.SetSprite("jump_icon");
			break;
		}
	}
}
