using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementGravity : MovementBase {
	// Gravity vector
	public float gravity = -1;
	private Vector3 gravityVector;

	// Use this for initialization
	public override void Start () {
		base.Start();
		gravityVector = new Vector3(0,gravity,0);
	}

	// The working internal speed
	[HideInInspector]
	public Vector3 speed = new Vector3();

	// Update is called once per frame
	void FixedUpdate () {
		if (!mCollider.CollidesAt(transform.localPosition + speed, mCollider.impassableCells)){
			transform.Translate(speed);
			if (!mCollider.CollidesAt(transform.localPosition + gravityVector, mCollider.impassableCells))
				speed += gravityVector;
		}
		else if (speed.sqrMagnitude>0)
	    	OnGrounded();
	}

	void OnGrounded() {
		speed.Set(0,0,0);
	}
}
