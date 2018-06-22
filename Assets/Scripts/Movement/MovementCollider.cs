using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementCollider : MonoBehaviour {
	// Cells this Collider treats as solid
	public TerrainGrid.GridCell[] impassableCells;
	// The radius of this cube
	[Range(0,0.5f)]
	public float radius;

	// Get contents by rounding floats. Returns NONE if x,y, or z is out of bounds [0, size)
	public TerrainGrid.GridCell GetGridCell(float x, float y, float z) {
		if (x < 0 || x >= TerrainGrid.i.xsize || y < 0 || y >= TerrainGrid.i.ysize || z < 0 || z >= TerrainGrid.i.zsize)
			return TerrainGrid.GridCell.OOB;
		return TerrainGrid.i.grid[(int)x, (int)y, (int)z];
	}

	public float minX {get{return transform.localPosition.x - radius;}}
	public float maxX {get{return transform.localPosition.x + radius;}}
	public float minY {get{return transform.localPosition.y - radius;}}
	public float maxY {get{return transform.localPosition.y + radius;}}
	public float minZ {get{return transform.localPosition.z - radius;}}
	public float maxZ {get{return transform.localPosition.z + radius;}}

	// Whether there is a union between these two MovementColliders
	public bool HasUnion(MovementCollider a){
		return 	enabled &&
				(a.minX < maxX && a.maxX > minX) &&
				(a.minY < maxY && a.maxY > minY) &&
				(a.minZ < maxZ && a.maxZ > minZ);
	}

	// Move this Collider by speed until it collides with any of the given GridCells
	public bool MoveByUntil(Vector3 speed, params TerrainGrid.GridCell[] types){
		Vector3 curpos = transform.localPosition;
		bool collided = false;

		//Move by each axis individually, inching along at Consts.moveIncrement
		//Add Z
		while(speed.x > 0)
		{
			float add = Mathf.Min(speed.x, Consts.i.moveIncrement);   //amount to subtract
			curpos.x += add;
			speed.x -= add;
			if( CollidesAt(curpos, types ) )
			{
				curpos.x -= add;
				collided = true;
				break;
			}
		}


		//Sub Z
		while(speed.x < 0)
		{
			float add = Mathf.Max(speed.x, -Consts.i.moveIncrement);   //amount to subtract
			curpos.x += add;
			speed.x -= add;
			if( CollidesAt(curpos, types ) )
			{
				curpos.x -= add;
				collided = true;
				break;
			}
		}

		//Add Z
		while(speed.z > 0)
		{
			float add = Mathf.Min(speed.z, Consts.i.moveIncrement);   //amount to subtract
			curpos.z += add;
			speed.z -= add;
			if( CollidesAt(curpos, types ) )
			{
				curpos.z -= add;
				collided = true;
				break;
			}
		}


		//Sub Z
		while(speed.z < 0)
		{
			float add = Mathf.Max(speed.z, -Consts.i.moveIncrement);   //amount to subtract
			curpos.z += add;
			speed.z -= add;
			if( CollidesAt(curpos, types ) )
			{
				curpos.z -= add;
				collided = true;
				break;
			}
		}

		//Add Y
		while(speed.y > 0)
		{
			float add = Mathf.Min(speed.y, Consts.i.moveIncrement);   //amount to subtract
			curpos.y += add;
			speed.y -= add;
			if( CollidesAt(curpos, types ) )
			{
				curpos.y -= add;
				collided = true;
				break;
			}
		}


		//Sub Y
		while(speed.y < 0)
		{
			float add = Mathf.Max(speed.y, -Consts.i.moveIncrement);   //amount to add to position
			curpos.y += add;
			speed.y -= add;
			if( CollidesAt(curpos, types ) )
			{
				curpos.y -= add;
				collided = true;
				break;
			}
		}

		transform.localPosition = curpos;

		return collided;
	}

	// Convenience function
	public bool CollidesAtRelative(float x, float y, float z, params TerrainGrid.GridCell[] types){
		return CollidesAt(transform.localPosition + new Vector3(x,y,z), types);
	}

	// Tests whether this Collider has a collision with the given type of GridCell at the given location
	public bool CollidesAt(Vector3 at, params TerrainGrid.GridCell[] types) {
		return 	CollidesAt2(at.x+radius, at.y+radius, at.z+radius, types)||
				CollidesAt2(at.x-radius, at.y+radius, at.z+radius, types)||
				CollidesAt2(at.x+radius, at.y-radius, at.z+radius, types)||
				CollidesAt2(at.x-radius, at.y-radius, at.z+radius, types)||
				CollidesAt2(at.x+radius, at.y+radius, at.z-radius, types)||
				CollidesAt2(at.x-radius, at.y+radius, at.z-radius, types)||
				CollidesAt2(at.x+radius, at.y-radius, at.z-radius, types)||
				CollidesAt2(at.x-radius, at.y-radius, at.z-radius, types);
	}
	// Helper function for checking for multiple types
	private bool CollidesAt2(float x, float y, float z, TerrainGrid.GridCell[] types){
		TerrainGrid.GridCell res = GetGridCell(x,y,z);
		foreach(TerrainGrid.GridCell t in types){
			if (res == t)
				return true;
		}
		return false;
	}

	// Use this for initialization
	void Start () {
		ColliderManager.i.colliders.Add(this);
	}

	void OnDestroy() {
		ColliderManager.i.colliders.Remove(this);
	}
	
	// Update is called once per frame
	void Update () { }
}
