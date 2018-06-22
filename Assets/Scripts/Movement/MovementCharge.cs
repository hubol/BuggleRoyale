using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementCharge : MovementKeyboard, GroundedListener {
	public float onGroundedScalar = 1.05f;
	// Boost speed when you hit the ground
	public void OnGrounded(Vector3 direction){
		if (enabled)
			Vector3.Scale(charged, new Vector3(onGroundedScalar, onGroundedScalar, onGroundedScalar));
	}

	private Vector3 charged;
	// Use charged vector instead of input
	protected override Vector3 GetInputVector(){
		return charged;
	}

	public override void FixedUpdate(){
		// Stun after hitting a wall
		if (stunlock > 0){
			if (--stunlock == 0){
				completed = true;
			}
			return;
		}
		if (completed)
			return;
		
		// We are picking charge direction
		if (ready){
			charged = base.GetInputVector();
			if (charged.magnitude > 0)
				ready = false;
		}
		// We picked!!!!!
		else
			base.FixedUpdate();
	}

	// Initialize
	private bool ready = false;
	public void Ready(){
		stunlock = 0;
		completed = false;
		ready = true;
	}
	// Check if still ready. Useful for canceling
	public bool IsReady(){
		return ready;
	}

	// Defer
	private bool completed = false;
	public bool Completed(){
		return completed;
	}

	// Crash
	public int stunnedFor = 30;
	private int stunlock = 0;
	public override void OnMoveFailed(){
		if (!ready){
			stunlock = stunnedFor;
		}
	}
}
