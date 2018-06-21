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

	[Range(0,1)]
	public float radius;

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
