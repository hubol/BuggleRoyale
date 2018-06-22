using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Renders a "free object" automatically to the IsoRender component
//using its XYZ position and the provided Sprite.
public class FreeObjectRender : MonoBehaviour
{
	public Sprite sprite;
	public IsoRender isoRender;
	public float bias;

	void Update()
	{
		isoRender.RenderFreeObject(sprite, transform.position, bias, Color.white);  //use world position!
	}

}
