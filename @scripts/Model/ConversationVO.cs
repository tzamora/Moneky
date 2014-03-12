using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ConversationVO
{
	public string ConversationID;
	
	public List<MessageVO> _messages;
	
	public List<MessageVO> Messages {
		get 
		{
			if(_messages == null)
			{
				_messages = new List<MessageVO>();
			}
			
			return this._messages;
		}
		set 
		{
			_messages = value;
		}
	}
}

