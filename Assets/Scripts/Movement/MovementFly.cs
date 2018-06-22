using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementFly : MovementKeyboard {
	public float ascendSpeed = 0.4f;
	public float descendSpeed = -0.6f;
	public float flightFriction = 0.1f;

	private float ySpeed = 0;

	// Update is called once per frame
	public override void FixedUpdate () {
		base.FixedUpdate();
		// Compute ySpeed based on input
		{
			float ySpeedTarget = 0;
			if (inputReciever.action2){
				ySpeedTarget = ascendSpeed;
			}
			else if (inputReciever.action3){
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
