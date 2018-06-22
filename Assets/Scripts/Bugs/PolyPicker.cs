using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolyPicker : PickerUpper<IsRolyPoly> {
	// If the base of this Beetle is above the center of the Poly
	protected override bool CanPickUp(IsRolyPoly t){ return mCollider.minX >= t.transform.localPosition.y; }

	// Poly should drop its gem if it has one
	protected override void OnHeldChanged(IsRolyPoly t, bool setEnabled){
		base.OnHeldChanged(t, setEnabled);
		if (!setEnabled){
			GemPicker gp = t.GetComponent<GemPicker>();
			if (gp == null)
				Debug.LogError("IsRolyPoly should have a GemPicker...");
			else
				gp.ForceDrop();
		}
	}

	// Put the Poly underneath you
	protected override void OnUpdateWithHeld(IsRolyPoly t){
		t.transform.localPosition = new Vector3(transform.localPosition.x, mCollider.minY, transform.localPosition.z);
	}
}
