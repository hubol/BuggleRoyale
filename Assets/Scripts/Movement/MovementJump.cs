using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputReciever))]
public class MovementJump : MovementGravity {
	// What key to jump with
	public string key;
	// Jump speed
	public float jumpSpeed;

	InputReciever inputReciever;


	public override void Start()
	{
		base.Start();
		inputReciever = GetComponent<InputReciever>();
	}

	// Update is called once per frame
	protected override void FixedUpdate2 () {
		// Jump events
		if (canJump && inputReciever.action1){
			SetYSpeed(jumpSpeed);
			canJump = false;
		}
	}

	// Whether you are grounded or not
	private bool canJump = false;

	protected override void OnGrounded(Vector3 direction) {
		base.OnGrounded(direction);
		if (Mathf.Sign(direction.y) != Mathf.Sign(jumpSpeed)) // TODO fix warning
			canJump = true;
	}
}
