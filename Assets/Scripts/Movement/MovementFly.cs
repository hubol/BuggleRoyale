using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementFly : MovementKeyboard {
	// Ascend key
	[Range(0,2)]
	public int ascend;
	// Descend key
	[Range(0,2)]
	public int descend;

	public float ascendSpeed = 0.4f;
	public float descendSpeed = -0.6f;
	public float flightFriction = 0.1f;

	// Multiply keyboard speed by scalar when in air
	public float flightSpeedScalar = 0.5f;
	// Speed is affected by flight
	public override float maxSpeed {get{
			if (mCollider.CollidesAtRelative(0,-Consts.i.moveIncrement,0,mCollider.impassableCells))
			    return base.maxSpeed;
			return base.maxSpeed * flightSpeedScalar;
		}}

	private float ySpeed = 0;

	// Update is called once per frame
	public override void FixedUpdate () {
		base.FixedUpdate();
		// Compute ySpeed based on input
		{
			float ySpeedTarget = 0;
			if (inputReciever.actions[ascend]){
				ySpeedTarget = ascendSpeed;
			}
			else if (inputReciever.actions[descend]){
				ySpeedTarget = descendSpeed;
			}
			// interpolating ySpeed towards ySpeedTarget
			if (ySpeed < ySpeedTarget){
				ySpeed += Mathf.Abs(flightFriction);
				if (ySpeed > ySpeedTarget)
					ySpeed = ySpeedTarget;
			}
			else if (ySpeed > ySpeedTarget){
				ySpeed -= Mathf.Abs(flightFriction);
				if (ySpeed < ySpeedTarget)
					ySpeed = ySpeedTarget;
			}
		}
		if (ySpeed != 0)
			mCollider.MoveByUntil(new Vector3(0,ySpeed,0),mCollider.impassableCells);
	}
}
