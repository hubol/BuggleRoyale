using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementCollider : MonoBehaviour {
	// Can't be null... (is there an Editor notation for this?)
	public TerrainGrid grid;
	// Cells this Collider treats as solid
	public TerrainGrid.GridCell[] impassableCells;

	// Get contents by rounding floats. Returns NONE if x,y, or z is out of bounds [0, size)
	public TerrainGrid.GridCell GetGridCell(float x, float y, float z) {
		if (x < 0 || x >= grid.xsize || y < 0 || y >= grid.ysize || z < 0 || z >= grid.zsize)
			return TerrainGrid.GridCell.NONE;
		return grid.grid[(int)x, (int)y, (int)z];
	}

	[Range(0,0.5f)]
	public float radius;

	// Move this Collider by speed until it collides with any of the given GridCells
	public bool MoveByUntil(Vector3 speed, params TerrainGrid.GridCell[] types){
		if (speed.magnitude > 1){ // e.g. bigger than a cell
			int increments = 1 + (int)speed.magnitude;
			float s = 1f / increments;

			for (int i=0; i<increments; i++){
				Vector3 fu = new Vector3(s * speed.x * (i+1), s * speed.y * (i+1), s * speed.z * (i+1)); // stupid
				if (CollidesAt(transform.localPosition + fu, types)){
					return true;
				}
				else
					transform.Translate(fu);
			}
			return false;
		}
		// the movement is not bigger than a cell
		else if (CollidesAt(transform.localPosition + speed, types)){
			return true;
		}
		transform.Translate(speed);
		return false;
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
	void Start () { }
	
	// Update is called once per frame
	void Update () { }
}
