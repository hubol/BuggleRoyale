using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//Renders isometric sprites, both free and grid-based.
//NOTE: it's necessary that IsoRender's Update occur before other objects which call its render,
//so it can clear its pool.
public class IsoRender : MonoBehaviour {
	public static IsoRender i;

	public Gradient heightGradient;   //colors applies to terrain based on y
	public TerrainGrid terrainGrid;   //link to terrain grid
	public Sprite basicBlockSprite;   //sprite to use for BASIC_BLOCK type
	public GameObject spritePrefab;   //prefab to use for rendered sprites
	public Camera mainCamera;   //the camera we need to point towards

	public int spritePoolInitial = 1;   //number of sprites to pool, initially

	//You can scale the amounts to match the tile size on the sprites.
	public float xscale = 1.0f;
	public float yscale = 1.0f;
	public float zscale = 1.0f;

	SpriteRenderer[] gridSprites;    //pooled grid objects
	int sprCount = 0;   //count grid sprites so we can pull them out of the pool




	void Start()
	{ 
		i = this;

		//Fill up the pool with sprites
		gridSprites = new SpriteRenderer[spritePoolInitial];
		for(int i=0; i<gridSprites.Length; i++)
			gridSprites[i] = MakeSprite();
	}


	void Update()
	{
		//Clear all sprites...
		sprCount = 0;
		for(int i=0; i<gridSprites.Length; i++)
			gridSprites[i].enabled = false;

		//Draw terrain grid!
		for( int x=0; x<terrainGrid.xsize; x++)
			for( int y=0; y<terrainGrid.ysize; y++)
				for( int z=0; z<terrainGrid.zsize; z++)
				{
					//Since x,y,z is actually the lower-left-back corner of an object, we should render it with 0.5 added to it,
					//so we place the centered sprite at the grid cell center
					float xfloat = (x+0.5f)*xscale, yfloat = (y+0.5f)*yscale, zfloat = (z+0.5f)*zscale;
					switch( terrainGrid.grid[x,y,z] )
					{
						default: break;
						case TerrainGrid.GridCell.BASIC_BLOCK:
							SpriteRenderer spr = UnpoolSprite(basicBlockSprite, xfloat, yfloat, zfloat);
							float height_normalized = ((float)y) / terrainGrid.ysize;   //get height so we can look up color
							if( x > z )  //flip color on opposite side of play area
								height_normalized = 1 - height_normalized;
							
							if( x != z)   //leave blocks whte in the middle
								spr.color = heightGradient.Evaluate(height_normalized);
							break;
					}
				}
	}


	
	//Renders a free (non-grid) object in the world.
	//You can use a positive "bias" value to nudge the object towards
	//the camera.
	public void RenderFreeObject(Sprite sprite, Vector3 pos, float bias, Color color)
	{
		SpriteRenderer spr = UnpoolSprite(sprite, pos.x, pos.y, pos.z);
		spr.color = color;
		//now we need to scootch this towards the camera using bias
		Vector3 spr_pos = spr.transform.localPosition;
		spr_pos += mainCamera.transform.TransformDirection(Vector3.forward) * -bias;
		spr.transform.localPosition = spr_pos;
	}



	SpriteRenderer UnpoolSprite(Sprite spr, float x, float y, float z)
	{
		if( gridSprites.Length <= sprCount )   //we don't have enough sprites pooled; make more
		{
			SpriteRenderer[] newpool = new SpriteRenderer[gridSprites.Length+1];   //allocate larger array
			for(int i=0; i<gridSprites.Length; i++)
				newpool[i] = gridSprites[i];  //copy old array to new array

			//create a new sprite
			newpool[newpool.Length-1] = MakeSprite();

			//dump old array
			gridSprites = newpool;
		}

		SpriteRenderer myspr = gridSprites[sprCount];
		myspr.enabled = true;
		myspr.sprite = spr;
		myspr.transform.localPosition = new Vector3(x,y,z);

		//look towards the camera!
		myspr.transform.rotation = Quaternion.LookRotation(mainCamera.transform.TransformDirection(Vector3.forward));
		
		sprCount++;
		return myspr;
	}


	//makes and returns a brand new isometric sprite
	SpriteRenderer MakeSprite()
	{
		return Instantiate(spritePrefab, transform, false).GetComponent<SpriteRenderer>();
	}


}
