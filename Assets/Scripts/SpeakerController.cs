﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakerController : MonoBehaviour {

	public float jetpackForce = 75.0f;
	public float forwardMovementSpeed = 3.0f;
	private Rigidbody2D rigidbody2D;

	public Transform groundCheckTransform;
	private bool grounded;
	public LayerMask groundCheckLayerMask;
	Animator animator;

	public ParticleSystem jetpack;

	private bool dead = false;

	// Use this for initialization
	void Start () {
		rigidbody2D = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate () 
	{
		bool jetpackActive = Input.GetButton("Fire1");

		jetpackActive = jetpackActive && !dead;

		if (jetpackActive)
		{
			rigidbody2D.AddForce(new Vector2(0, jetpackForce));
		}

		if (!dead)
		{
			Vector2 newVelocity = rigidbody2D.velocity;
			newVelocity.x = forwardMovementSpeed;
			rigidbody2D.velocity = newVelocity;
		}

		UpdateGroundedStatus();

		AdjustJetpack(jetpackActive);
	}

	void UpdateGroundedStatus()
	{
		grounded = Physics2D.OverlapCircle(groundCheckTransform.position, 0.1f, groundCheckLayerMask);
		animator.SetBool("grounded", grounded);
	}

	void AdjustJetpack (bool jetpackActive)
	{
		jetpack.enableEmission = !grounded;
		jetpack.emissionRate = jetpackActive ? 300.0f : 75.0f; 
	}
		
	void OnTriggerEnter2D(Collider2D collider)
	{
		HitByLaser(collider);
	}

	void HitByLaser(Collider2D laserCollider)
	{
		dead = true;
	}
}
