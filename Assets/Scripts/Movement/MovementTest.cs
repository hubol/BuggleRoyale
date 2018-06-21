using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTest : MovementBase {
	
	// Update is called once per frame
	void FixedUpdate () {
		if (mCollider.CollidesAt(transform.localPosition, TerrainGrid.GridCell.BASIC_BLOCK))
		    Debug.Log("Fuck you");
	}
}
