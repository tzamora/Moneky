using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameData : MonoSingleton<GameData> 
{
	public GameStatesEnum CurrentGameState = GameStatesEnum.playing;
	
	public ConversationData GameConversationData;
}
