using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputReceiver))]
public class MovementJump : MovementGravity {
	// Key
	[Range(0,2)]
	public int jump;
	// Jump speed
	public float jumpSpeed;

	InputReceiver inputReciever;

	public override void Start()
	{
		base.Start();
		inputReciever = GetComponent<InputReceiver>();
	}

	// Update is called once per frame
	protected override void FixedUpdate2 () {
		// Jump events
		if (canJump && inputReciever.actions[jump]){
			SetYSpeed(jumpSpeed);
			canJump = false;
		}
	}

	// Whether you are grounded or not
	private bool canJump = false;

	protected override void OnGrounded2(Vector3 direction) {
		base.OnGrounded2(direction);
		if (Mathf.Sign(direction.y) != Mathf.Sign(jumpSpeed)) // TODO fix warning
			canJump = true;
	}
}
