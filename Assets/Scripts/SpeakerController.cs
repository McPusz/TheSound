using System.Collections;
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

	public GUIStyle restartButton;
	private bool dead = false;
	private uint coins = 0;

	// Use this for initialization
	void Start () {
		rigidbody2D = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnGUI()
	{
		DisplayCoinsCount();
		DisplayRestartButton();
	}

	void FixedUpdate () 
	{
		bool jetpackActive = Input.GetKey("space");

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
		if (collider.gameObject.CompareTag ("Coins"))
			CollectCoin (collider);
		else if (collider.gameObject.CompareTag ("Obudowa"))
			HitByLaser (collider);
		else
			HitByLaser(collider);
	}

	void HitByLaser(Collider2D laserCollider)
	{
		dead = true;
		animator.SetBool("dead", true);
	}

	void CollectCoin(Collider2D coinCollider)
	{
		coins++;

		Destroy(coinCollider.gameObject);
	}

	void DisplayCoinsCount()
	{
		Rect coinIconRect = new Rect(10, 10, 32, 32);

		GUIStyle style = new GUIStyle();
		style.fontSize = 60;
		style.fontStyle = FontStyle.Bold;
		style.normal.textColor = Color.yellow;

		Rect labelRect = new Rect(coinIconRect.xMax, coinIconRect.y, 60, 32);
		GUI.Label(labelRect, coins.ToString(), style);
	}

	void DisplayRestartButton()
	{
		if (dead && grounded)
		{
			Rect buttonRect = new Rect(Screen.width * 0.35f, Screen.height * 0.45f, Screen.width * 0.30f, Screen.height * 0.1f);
			restartButton = new GUIStyle();
			if (GUI.Button(buttonRect, "Tap to restart!"))
			{
				Application.LoadLevel (Application.loadedLevelName);
			};
		}
	}
}
