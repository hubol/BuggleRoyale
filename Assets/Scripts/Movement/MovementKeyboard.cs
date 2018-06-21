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

	// Update is called once per frame
	void FixedUpdate () {
		// Compute the desired speed from key input
		{
			Vector3 spd = new Vector3(0, 0, 0);
			if (Input.GetKey(right))
				spd = new Vector3(maxSpeed, 0, 0);
			else if (Input.GetKey(up))
				spd = new Vector3(0, 0, maxSpeed);
			else if (Input.GetKey(left))
				spd = new Vector3(-maxSpeed, 0, 0);
			else if (Input.GetKey(down))
				spd = new Vector3(0, 0, -maxSpeed);
			// Apply friction to speed
			speed = Vector3.MoveTowards(speed, spd, friction);
		}
		// Incrementally move
		mCollider.MoveByUntil(speed, mCollider.impassableCells);
	}
}
