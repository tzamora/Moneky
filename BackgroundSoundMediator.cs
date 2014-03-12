using UnityEngine;
using System.Collections;

public class BackgroundSoundMediator : MonoBehaviour {
	
	public AudioClip BackgroundMusic;
	
	// Use this for initialization
	void Start () 
	{
		SoundManager.Get.PlayClip(BackgroundMusic, true);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
