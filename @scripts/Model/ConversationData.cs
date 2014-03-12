using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

[System.Serializable]
public class ConversationData
{
	//
	// conversations list
	//
	
	public List<ConversationVO> _conversations;
	
	public ConversationVO GetConversation(string conversationID )
	{
		 ConversationVO conversation = 
			_conversations.Where( c => c.ConversationID == conversationID).FirstOrDefault();
		
		return conversation;
	}
	
	//
	// Getters and setters
	//
	
	public List<ConversationVO> Conversations
	{
		get 
		{
			if(_conversations == null)
			{
				_conversations = new List<ConversationVO>();
			}
			
			return this._conversations;
		}
		set {
			_conversations = value;
		}
	}
}

