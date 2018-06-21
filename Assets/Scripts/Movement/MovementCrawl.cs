using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementCrawl : MovementKeyboard {
	public float ascendSpeed;
	public float descendSpeed;

	private Vector3 ascendVec, descendVec;
	public override void Start() {
		base.Start();
		ascendVec = new Vector3(0, Mathf.Abs(ascendSpeed), 0);
		descendVec = new Vector3(0, -Mathf.Abs(descendSpeed), 0);
		grav = GetComponent<MovementGravity>();
	}

	private MovementGravity grav;
	private void SetGravityEnabled(bool gravEnabled){
		if (grav != null)
			grav.applyGravity = gravEnabled;
	}

	protected override Vector3 rightVec {
		get{
			SetGravityEnabled(false);
			if (mCollider.CollidesAtRelative(Consts.i.moveIncrement, 0, 0, TerrainGrid.GridCell.BASIC_BLOCK))
			    return ascendVec;
			else if (mCollider.CollidesAtRelative(-Consts.i.moveIncrement, 0, 0, TerrainGrid.GridCell.BASIC_BLOCK) && !mCollider.CollidesAtRelative(0, -Consts.i.moveIncrement, 0, TerrainGrid.GridCell.BASIC_BLOCK))
				return descendVec;
			SetGravityEnabled(true);
			return base.rightVec;
		}
	}
	protected override Vector3 leftVec {
		get{
			SetGravityEnabled(false);
			if (mCollider.CollidesAtRelative(Consts.i.moveIncrement, 0, 0, TerrainGrid.GridCell.BASIC_BLOCK) && !mCollider.CollidesAtRelative(0, -Consts.i.moveIncrement, 0, TerrainGrid.GridCell.BASIC_BLOCK))
				return descendVec;
			else if (mCollider.CollidesAtRelative(-Consts.i.moveIncrement, 0, 0, TerrainGrid.GridCell.BASIC_BLOCK))
				return ascendVec;
			SetGravityEnabled(true);
			return base.leftVec;
		}
	}
	protected override Vector3 upVec {
		get{
			SetGravityEnabled(false);
			if (mCollider.CollidesAtRelative(0, 0, Consts.i.moveIncrement, TerrainGrid.GridCell.BASIC_BLOCK))
				return ascendVec;
			else if (mCollider.CollidesAtRelative(0, 0, -Consts.i.moveIncrement, TerrainGrid.GridCell.BASIC_BLOCK) && !mCollider.CollidesAtRelative(0, -Consts.i.moveIncrement, 0, TerrainGrid.GridCell.BASIC_BLOCK))
				return descendVec;
			SetGravityEnabled(true);
			return base.upVec;
		}
	}
	protected override Vector3 downVec {
		get{
			SetGravityEnabled(false);
			if (mCollider.CollidesAtRelative(0, 0, Consts.i.moveIncrement, TerrainGrid.GridCell.BASIC_BLOCK) && !mCollider.CollidesAtRelative(0, -Consts.i.moveIncrement, 0, TerrainGrid.GridCell.BASIC_BLOCK))
				return descendVec;
			else if (mCollider.CollidesAtRelative(0, 0, -Consts.i.moveIncrement, TerrainGrid.GridCell.BASIC_BLOCK))
				return ascendVec;
			SetGravityEnabled(true);
			return base.downVec;
		}
	}

}
