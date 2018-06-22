using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditCube : MonoBehaviour
{
	void Start ()
	{
		//using our world position, put an entry into LevelExport	
		Vector3 pos = transform.position - LevelExport.i.offset;
		pos.x = Mathf.Round(pos.x);
		pos.y = Mathf.Round(pos.y);
		pos.z = Mathf.Round(pos.z);

		try
		{ 
			LevelExport.i.grid[(int)pos.x, (int)pos.y, (int)pos.z] = 1;

			//MIRROR LEVEL
			LevelExport.i.grid[(int)pos.z, (int)pos.y, (int)pos.x] = 1;
		}
		catch
		{
			Debug.LogError("Wrong coordinate: " + pos);
		}
	}
	

}
