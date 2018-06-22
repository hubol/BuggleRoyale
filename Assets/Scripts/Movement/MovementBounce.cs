using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBounce : MovementGravity {
	public float scale = -.45f;
	public float threshold = -1;

	protected override void OnGrounded2(Vector3 direction) {
		if (direction.y < 0)
			SetYSpeed((GetYSpeed() > threshold)?0:(direction.y*scale));
	}
}
