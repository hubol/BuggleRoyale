using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputReciever))]
public class MovementKeyboard : MovementBase {
	// Key names
	//public string right, up, left, down;
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

	protected InputReciever inputReciever;   //keyboard input gets piped through here now

	public override void Start ()
	{
		base.Start();
		inputReciever = GetComponent<InputReciever>();
	}

	// Update is called once per frame
	public virtual void FixedUpdate () {
		// Compute the desired speed from key input
		{
			Vector3 spd = new Vector3(0, 0, 0);
			if (inputReciever.right)
				spd = rightVec;
			else if (inputReciever.up)
				spd = upVec;
			else if (inputReciever.left)
				spd = leftVec;
			else if (inputReciever.down)
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
