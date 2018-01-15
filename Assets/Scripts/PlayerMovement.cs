using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public bool FacingLeft;
	public float MoveSpeed = 1.5f;
	public float JumpPower = 200;
	
	private float _velocity;
	private bool _grounded;
	private float _fallSpeed;
	private Animator _animator;


	private void Start()
	{
		_animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		PlayerMove ();
	}

	void PlayerMove ()
	{
		float horizontal = Input.GetAxis("Horizontal");

		if (Input.GetButtonDown("Jump") && _grounded) {
			Jump();
		}

		if (horizontal < 0.0f && !FacingLeft)
		{
			FlipPlayer ();
		}
		else if (horizontal > 0.0f && FacingLeft)
		{
			FlipPlayer ();
		}

		_velocity = horizontal * MoveSpeed;
		gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(_velocity, gameObject.GetComponent<Rigidbody2D>().velocity.y);
		_animator.SetFloat("Speed", Math.Abs(_velocity));
	}
	
	void Jump ()
	{
		// JMUPING CODE
		GetComponent<Rigidbody2D>().AddForce(Vector2.up * JumpPower);
		_grounded = false;
		_animator.SetBool("Jumping", true);
	}

	void FlipPlayer ()
	{
		FacingLeft = !FacingLeft;

		Vector2 localScale = gameObject.transform.localScale;
		localScale.x *= -1;
		transform.localScale = localScale;
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		if (col.gameObject.CompareTag("Ground"))
		{
			_grounded = true;
			_animator.SetBool("Jumping", false);
		}
	}
}
