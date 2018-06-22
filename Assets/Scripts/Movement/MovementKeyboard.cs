using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputReceiver))]
public class MovementKeyboard : MovementBase {
	// Key names
	//public string right, up, left, down;
	public float keySpeed;
	// Maximum speed
	[HideInInspector]
	public virtual float maxSpeed {get{return keySpeed;}}
	// Acceleration and deceleration
	public float friction;

	// The working internal speed
	[HideInInspector]
	public Vector3 speed = new Vector3();

	protected virtual Vector3 rightVec {get{return new Vector3(maxSpeed, 0, 0);}}
	protected virtual Vector3 leftVec {get{return new Vector3(-maxSpeed, 0, 0);}}
	protected virtual Vector3 upVec {get{return new Vector3(0, 0, maxSpeed);}}
	protected virtual Vector3 downVec {get{return new Vector3(0, 0, -maxSpeed);}}

	// Compute the desired speed from key input
	protected virtual Vector3 GetInputVector(){
		Vector3 spd = new Vector3(0, 0, 0);
		if (inputReceiver.right)
			spd = rightVec;
		else if (inputReceiver.up)
			spd = upVec;
		else if (inputReceiver.left)
			spd = leftVec;
		else if (inputReceiver.down)
			spd = downVec;
		return spd;
	}

	protected InputReceiver inputReceiver;   //keyboard input gets piped through here now

	public override void Start ()
	{
		base.Start();
		inputReceiver = GetComponent<InputReceiver>();
	}

	// Update is called once per frame
	public virtual void FixedUpdate () {
		// Apply friction to speed
		speed = Vector3.MoveTowards(speed, GetInputVector(), friction);

		// Incrementally move
		if (speed.magnitude > 0 && mCollider.MoveByUntil(speed, mCollider.impassableCells)){
			OnMoveFailed();
		}
	}

	public virtual void OnMoveFailed(){ }
}
