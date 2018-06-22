using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Like FreeObjectRender, but actually has different animations it can use
public class FreeObjectAnims : MonoBehaviour
{
	[System.Serializable]
	public class Anim
	{
		public Sprite[] frames;	
		public float speed=30.0f;
	}

	public Anim[] anims;
	public float bias=0;


	public int CurAnim
	{
		get {  return curAnim;}
		set
		{
			if( curAnim != value )
			{
				curAnim = value;
				animPos = 0;   //reset animtion position when current animation changes
			}
		}
	}

	int curAnim=0;
	public float animPos=0;  //normalized;

	void Update()
	{
		animPos = (animPos + anims[curAnim].speed * Time.deltaTime / anims[curAnim].frames.Length ) % 1;   //get normalized animation position
		int frame = Mathf.Clamp( (int)(animPos * anims[curAnim].frames.Length), 0, anims[curAnim].frames.Length );   //get actual animation frame

		IsoRender.i.RenderFreeObject(anims[curAnim].frames[frame], transform.position, bias, Color.white);  //use world position!
	}

}
