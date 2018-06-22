using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Takes a bunch of LevelEditCubes in the scene and exports them.
public class LevelExport : MonoBehaviour
{
	public int xsize=10;
	public int ysize=10;
	public int zsize=10;


	public Vector3 offset;   //if the origin cube isn't at 0,0,0 by accident

	public int[,,] grid;
	public static LevelExport i;

	public string exportString;

	bool saved = false;

	private void Awake()
	{
		grid = new int[xsize, ysize, zsize];
		for(int x=0; x<xsize; x++)
			for(int y=0; y<ysize; y++)
				for(int z=0; z<zsize; z++)
					grid[x,y,z] = 0;
		i = this;
	}

	//On LevelEditCubes.Start, they will access the grid and set it accordingly.


	private void Update()
	{
		if(!saved)
		{
			saved = true;
			//export the grid in a format that would initialize an array.
			exportString = "int["+xsize+","+ysize+","+zsize+"] LEVEL_INIT = {";
			for(int x=0; x<xsize; x++)
			{
				exportString += "{";
				for(int y=0; y<ysize; y++)
				{
					exportString += "{";
					for(int z=0; z<zsize; z++)
						exportString += grid[x,y,z] + ",";
					exportString += "},";
				}

				exportString += "},";
			}

			exportString += "};";
		}


		int _DEBUG_CATCH = 0;
	}


}
