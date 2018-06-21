using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementKeyboard : MovementBase {
	// Key names
	public string right, up, left, down;
	// Maximum speed
	public float maxSpeed;
	// Acceleration and deceleration
	public float friction;

	// The working internal speed
	[HideInInspector]
	public Vector3 speed = new Vector3();

	protected virtual Vector3 rightVec {get{return new Vector3(maxSpeed, 0, 0);}}
	protected virtual Vector3 leftVec {get{return new Vector3(-maxSpeed, 0, 0);}}
	protected virtual Vector3 upVec {get{return new Vector3(0, 0, maxSpeed);}}
	protected virtual Vector3 downVec {get{return new Vector3(0, 0, -maxSpeed);}}

	// Update is called once per frame
	public virtual void FixedUpdate () {
		// Compute the desired speed from key input
		{
			Vector3 spd = new Vector3(0, 0, 0);
			if (Input.GetKey(right))
				spd = rightVec;
			else if (Input.GetKey(up))
				spd = upVec;
			else if (Input.GetKey(left))
				spd = leftVec;
			else if (Input.GetKey(down))
				spd = downVec;
			// Apply friction to speed
			speed = Vector3.MoveTowards(speed, spd, friction);
		}
		// Incrementally move
		if (speed.magnitude > 0 && mCollider.MoveByUntil(speed, mCollider.impassableCells)){
			OnMoveFailed();
		}
	}

	public virtual void OnMoveFailed(){ }
}
