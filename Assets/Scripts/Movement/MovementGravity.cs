using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementGravity : MovementBase {
	// Gravity vector
	public float gravity = -1;
	private Vector3 gravityVector, testGravityVector;

	bool wasGrounded = false;

	[HideInInspector]
	public bool applyGravity = true;

	// Use this for initialization
	public override void Start () {
		applyGravity = true;
		base.Start();
		gravityVector = new Vector3(0,gravity,0);
		testGravityVector = new Vector3(0, Mathf.Sign(gravity)*Consts.i.moveIncrement, 0);
	}

	// The working internal speed
	private Vector3 speed = new Vector3();
	protected void SetYSpeed(float y){
		speed.y = y;
	}

	// Update is called once per frame
	public void FixedUpdate () {
		if (applyGravity){
			if (mCollider.CollidesAt(transform.localPosition + testGravityVector, mCollider.impassableCells))
			{
				if(!wasGrounded)
					OnGrounded(speed);
				wasGrounded = true;
			}
			else
			{ 
				speed += gravityVector; // accumulate gravity
				wasGrounded = false;
			}
			mCollider.MoveByUntil(speed, mCollider.impassableCells);
			FixedUpdate2();
		}
	}

	protected virtual void FixedUpdate2(){ }

	// Deliver grounded event.
	protected virtual void OnGrounded(Vector3 direction) {
		speed.Set(0,0,0);
	}
}
