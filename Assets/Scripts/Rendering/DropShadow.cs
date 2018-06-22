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
	public Sprite shadowSprite;    //render the sprite manually so parenting doesn't cause issues
	public float bias = 0.0f;
	public bool autoCast = true;   //should we auto-cast a shadow from the target transform?  If not, wait for ShadowCast() calls



	void Update()
	{
		if(autoCast)
			ShadowCast( targetTransform.position );   //world space
	}


	public void ShadowCast( Vector3 pos )
	{
		int x = (int)Mathf.Floor(pos.x);
		int y = (int)Mathf.Floor(pos.y);
		int z = (int)Mathf.Floor(pos.z);

		bool out_of_bounds = x<0 || x >= TerrainGrid.i.xsize || y < 0 || z <0 || z >= TerrainGrid.i.zsize;   //it's fine if y >= terrainGrid.ysize, because the shadow will just go downward


		if(!out_of_bounds)
		{
			//start at x,y,z and move down until we hit a block.  Then, just place ourselves on top of that block
			for(; y >= 0; y--)
				if(y<TerrainGrid.i.ysize && TerrainGrid.i.grid[x,y,z] != TerrainGrid.GridCell.NONE)
				{
					//found non-empty cell; place ourselves on top and get outta here
					IsoRender.i.RenderFreeObject(shadowSprite, new Vector3(pos.x, y+1, pos.z), bias, Color.white);   //use world space
					break;
				}
		}
	}


}
