using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementGravity : MovementBase {
	// Gravity vector
	public float gravity = -1;
	private Vector3 gravityVector, testGravityVector;

	// Use this for initialization
	public override void Start () {
		base.Start();
		gravityVector = new Vector3(0,gravity,0);
		testGravityVector = new Vector3(0, Mathf.Sign(gravity)*0.1f, 0);
		// First grounded event
		if (mCollider.CollidesAt(transform.localPosition + testGravityVector, mCollider.impassableCells))
			OnGrounded(testGravityVector);
	}

	// The working internal speed
	private Vector3 speed = new Vector3();
	protected void SetYSpeed(float y){
		speed.y = y;
	}

	// Update is called once per frame
	public virtual void FixedUpdate () {
		// If we have vertical speed
		if (speed.magnitude > 0){
			if (mCollider.MoveByUntil(speed, mCollider.impassableCells))
	    		OnGrounded(speed);
			else
				speed += gravityVector; // accumulate gravity
		}
		// Otherwise acquire vertical speed
		else if (!mCollider.CollidesAt(transform.localPosition + testGravityVector, mCollider.impassableCells)){
			speed += gravityVector;
		}
	}

	// Deliver grounded event.
	protected virtual void OnGrounded(Vector3 direction) {
		this.
		speed.Set(0,0,0);
	}
}
