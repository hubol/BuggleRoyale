using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Copies the hard-coded grid into TerrainGrid (assumes correct size)
[RequireComponent(typeof(TerrainGrid))]
public class SetupTerrainGrid : MonoBehaviour
{

	void Start ()
	{
		TerrainGrid tg = GetComponent<TerrainGrid>();
		for(int x=0; x<LevelInit.LEVEL_INIT.GetLength(0); x++)	
			for(int y=0; y<LevelInit.LEVEL_INIT.GetLength(1); y++)
				for(int z=0; z<LevelInit.LEVEL_INIT.GetLength(2); z++)
					if(LevelInit.LEVEL_INIT[x,y,z]==1)
						tg.grid[x,y,z] = TerrainGrid.GridCell.BASIC_BLOCK;

	}
}
