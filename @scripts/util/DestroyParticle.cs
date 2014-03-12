using UnityEngine;
using System.Collections;

public class DestroyParticle : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
		//
		// get the particle system
		//
		
		ParticleSystem particles = GetComponent<ParticleSystem>();
		
		//
		// destroy the particle when the duration finishes
		//
		
		Destroy(gameObject, particles.duration);
		
	}
}
