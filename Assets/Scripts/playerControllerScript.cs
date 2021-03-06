﻿using UnityEngine;
using System.Collections;

public class playerControllerScript : MonoBehaviour {

	public float maxSpeed = 10f;
	bool facingRight = true;

	Animator anim;

	bool grounded = false;
	public bool enemycontact = false;
	public Transform groundCheck;
	float groundRadius = 0.2f;
	public LayerMask whatIsGround;
	public LayerMask whatIsEnemy;
	public float jumpForce = 700f;
	public bool moveWhileJumping = false;

	bool doubleJump = false;

	// Use this for initialization
	void Start () {
	
		anim = GetComponent<Animator> ();

	}
	
	// Update is called once per frame
	void Update () {
	
		if ((grounded || !doubleJump) && Input.GetKeyDown (KeyCode.Space)) {
				
			anim.SetBool("Ground", false);
			rigidbody2D.AddForce(new Vector2(0, jumpForce));

			if (!doubleJump && !grounded)
				doubleJump = true;

		}

	}

	void FixedUpdate() {

		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		enemycontact = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsEnemy);
		anim.SetBool ("Ground", grounded);

		if (grounded)
			doubleJump = false;

		anim.SetFloat ("vSpeed", rigidbody2D.velocity.y);

		if (!grounded && !moveWhileJumping)
			return;

		float move = Input.GetAxis ("Horizontal");

		anim.SetFloat ("Speed", Mathf.Abs (move));

		rigidbody2D.velocity = new Vector2 (move * maxSpeed, rigidbody2D.velocity.y);

		if (move > 0 && !facingRight)
						Flip ();
				else if (move < 0 && facingRight)
						Flip ();

	}

	void Flip() {

		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;


	}
}
