using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//This holds the grid-based isometric game world.
public class TerrainGrid : MonoBehaviour
{
	public enum GridCell
	{
		NONE=0,
		BASIC_BLOCK
	};


	//Size of the grid.  Set in inspector
	public int xsize= 10;
	public int ysize = 10;
	public int zsize = 10;
	

	//If true, we'll just splat a bunch of cubes to represent the grid
	public bool drawAsCubes = true;
	public GameObject cubePrefab;



	//Actual grid is just a 3D array
	[HideInInspector]
	public GridCell[,,] grid;


	void Awake()
	{
		grid = new GridCell[xsize, ysize, zsize];

		//temp: just fill every cell for which x + y + z <= 10
		for(int x=0; x<xsize; x++)
			for(int y=0; y<ysize; y++)
				for(int z=0; z<zsize; z++)
					if( y == 0 )
						grid[x,y,z] = GridCell.BASIC_BLOCK;
					else
						grid[x,y,z] = GridCell.NONE;


		//temp: just fill in some bricks on the outer edge
		grid[0, 1, 0] = GridCell.BASIC_BLOCK;
		grid[1, 1, 0] = GridCell.BASIC_BLOCK;
		grid[2, 1, 0] = GridCell.BASIC_BLOCK;
		grid[3, 1, 0] = GridCell.BASIC_BLOCK;
		grid[0, 1, 1] = GridCell.BASIC_BLOCK;
		grid[1, 1, 1] = GridCell.BASIC_BLOCK;
		grid[2, 1, 1] = GridCell.BASIC_BLOCK;
		grid[3, 1, 1] = GridCell.BASIC_BLOCK;
	}


	private void Start()
	{
		if(drawAsCubes)
		{
			//visualize the array as cubes
			for(int x=0; x<xsize; x++)
				for(int y=0; y<ysize; y++)
					for(int z=0; z<zsize; z++)
						switch(grid[x,y,z])
						{
							default: break;
							case GridCell.BASIC_BLOCK:
								GameObject block = Instantiate(cubePrefab, transform, false);
								block.transform.localPosition = new Vector3(x,y,z);
								break;
						}
		}
	}






}
