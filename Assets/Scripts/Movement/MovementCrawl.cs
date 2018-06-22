using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementCrawl : MovementKeyboard {

	// Speed at which to ascend
	public float ascendSpeed;
	// Speed at which to descend
	public float descendSpeed;
	// Multiply the movement speed when sidling
	public float climbSpeedScalar;



	private Vector3 ascendVec, descendVec, climbSpeedScalarVec;
	public override void Start() {
		base.Start();
		ascendVec = new Vector3(0, Mathf.Abs(ascendSpeed), 0);
		descendVec = new Vector3(0, -Mathf.Abs(descendSpeed), 0);
		climbSpeedScalarVec = new Vector3(climbSpeedScalar, climbSpeedScalar, climbSpeedScalar);
		grav = GetComponent<MovementGravity>();
	}

	private MovementGravity grav;
	private void SetGravityEnabled(bool gravEnabled){
		if (grav != null)
			grav.applyGravity = gravEnabled;
	}

	protected override Vector3 rightVec {
		get{
			// Disable gravity (assume we are climbing)
			SetGravityEnabled(false);
			// We are ascending (there is a wall in this direction)
			if (mCollider.CollidesAtRelative(Consts.i.moveIncrement, 0, 0, TerrainGrid.GridCell.BASIC_BLOCK))
			    return ascendVec;
			// We are descending (there is a wall in the opposite direction)
			else if (mCollider.CollidesAtRelative(-Consts.i.moveIncrement, 0, 0, TerrainGrid.GridCell.BASIC_BLOCK) && !mCollider.CollidesAtRelative(0, -Consts.i.moveIncrement, 0, TerrainGrid.GridCell.BASIC_BLOCK))
				return descendVec;
			// We are sidling (there is a wall perpendicular to this direction)
			if (mCollider.CollidesAtRelative(0, 0, Consts.i.moveIncrement, TerrainGrid.GridCell.BASIC_BLOCK) || mCollider.CollidesAtRelative(0, 0, -Consts.i.moveIncrement, TerrainGrid.GridCell.BASIC_BLOCK)){
				Vector3 scaled = Vector3.Scale(base.rightVec, climbSpeedScalarVec);
				return scaled;
			}
			// No walls here, default movement
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
			if (!(mCollider.CollidesAtRelative(0, 0, Consts.i.moveIncrement, TerrainGrid.GridCell.BASIC_BLOCK) || mCollider.CollidesAtRelative(0, 0, -Consts.i.moveIncrement, TerrainGrid.GridCell.BASIC_BLOCK))){
				SetGravityEnabled(true);
				return base.leftVec;
			}
			Vector3 scaled = Vector3.Scale(base.leftVec, climbSpeedScalarVec);
			return scaled;
		}
	}
	protected override Vector3 upVec {
		get{
			SetGravityEnabled(false);
			if (mCollider.CollidesAtRelative(0, 0, Consts.i.moveIncrement, TerrainGrid.GridCell.BASIC_BLOCK))
				return ascendVec;
			else if (mCollider.CollidesAtRelative(0, 0, -Consts.i.moveIncrement, TerrainGrid.GridCell.BASIC_BLOCK) && !mCollider.CollidesAtRelative(0, -Consts.i.moveIncrement, 0, TerrainGrid.GridCell.BASIC_BLOCK))
				return descendVec;
			if (!(mCollider.CollidesAtRelative( Consts.i.moveIncrement, 0, 0, TerrainGrid.GridCell.BASIC_BLOCK) || mCollider.CollidesAtRelative(-Consts.i.moveIncrement, 0, 0, TerrainGrid.GridCell.BASIC_BLOCK))){
				SetGravityEnabled(true);
				return base.upVec;
			}
			Vector3 scaled = Vector3.Scale(base.upVec, climbSpeedScalarVec);
			return scaled;
		}
	}
	protected override Vector3 downVec {
		get{
			SetGravityEnabled(false);
			if (mCollider.CollidesAtRelative(0, 0, Consts.i.moveIncrement, TerrainGrid.GridCell.BASIC_BLOCK) && !mCollider.CollidesAtRelative(0, -Consts.i.moveIncrement, 0, TerrainGrid.GridCell.BASIC_BLOCK))
				return descendVec;
			else if (mCollider.CollidesAtRelative(0, 0, -Consts.i.moveIncrement, TerrainGrid.GridCell.BASIC_BLOCK))
				return ascendVec;
			if (!(mCollider.CollidesAtRelative( Consts.i.moveIncrement, 0, 0, TerrainGrid.GridCell.BASIC_BLOCK) || mCollider.CollidesAtRelative(-Consts.i.moveIncrement, 0, 0, TerrainGrid.GridCell.BASIC_BLOCK))){
				SetGravityEnabled(true);
				return base.downVec;
			}
			Vector3 scaled = Vector3.Scale(base.downVec, climbSpeedScalarVec);
			return scaled;
		}
	}

}
