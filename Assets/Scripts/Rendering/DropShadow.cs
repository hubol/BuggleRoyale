using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Automatically moves this object down from the target object until it hits an obstacle (or goes out of bounds).
//If it goes out of bounds, it disables the sprite.
//Uses variable timestep update, intended for purely cosmetic purposes.
//Uses world space, so this can be parented under the object casting the shadow if we want.
public class DropShadow : MonoBehaviour
{
	public Transform targetTransform;  //drops a shadow from this transform
	public MonoBehaviour disableOnOutOfBounds;    //the component to enable/disable depending on if we went out of bounds
	public TerrainGrid terrainGrid;


	void Update()
	{
		Vector3 pos = targetTransform.position;   //world space
		
		int x = (int)Mathf.Floor(pos.x);
		int y = (int)Mathf.Floor(pos.y);
		int z = (int)Mathf.Floor(pos.z);

		bool out_of_bounds = x<0 || x >= terrainGrid.xsize || y < 0 || z <0 || z >= terrainGrid.zsize;   //it's fine if y >= terrainGrid.ysize, because the shadow will just go downward


		if( out_of_bounds )
			disableOnOutOfBounds.enabled = false;
		else
		{
			//start at x,y,z and move down until we hit a block.  Then, just place ourselves on top of that block
			for(; y >= 0; y--)
				if(y<terrainGrid.ysize && terrainGrid.grid[x,y,z] != TerrainGrid.GridCell.NONE)
				{
					//found non-empty cell; place ourselves on top and get outta here
					transform.position = new Vector3(targetTransform.position.x, y+1, targetTransform.position.z);   //use world space
					break;
				}

			if( y < 0 )   //went out of bounds
				disableOnOutOfBounds.enabled = false;
			else
				disableOnOutOfBounds.enabled = true;  //found a place to put the shadow
		}
		
				
	}
}
