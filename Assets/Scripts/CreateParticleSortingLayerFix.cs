using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateParticleSortingLayerFix : MonoBehaviour {

	private ParticleSystemRenderer particleSystem;
	// Use this for initialization
	void Start () {
		particleSystem = GetComponent<ParticleSystemRenderer> ();
		particleSystem.sortingLayerName = "Player";
		particleSystem.sortingOrder = -1;
	}
	
	// Update is called once per frame
	void Update () {
	}
}
