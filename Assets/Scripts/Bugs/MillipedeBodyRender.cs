﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Renders MillipedeBody component into IsoRender
[RequireComponent(typeof(MillipedeBody))]
public class MillipedeBodyRender : MonoBehaviour
{
	public IsoRender isoRender;
	public Sprite[] bodySegmentAnim;
	public float bodySegmentBias = 0.0f;
	public DropShadow dropShadow;
	public float animLength = 1.0f;  //length in seconds of the animation
	public bool animating = true; 
	public float segmentAnimOffset = 0.1f;



	MillipedeBody millipedeBody;
	float animTime = 0;   //normalized time of the animation

	private void Awake()
	{
		millipedeBody = GetComponent<MillipedeBody>();
	}


	void Update()
	{
		if( animating )
			animTime += (Time.deltaTime / animLength) % 1;
		

		for(int i=0; i<millipedeBody.segmentPositions.Length; i++)
		{ 
			Vector3 pos = transform.TransformPoint(millipedeBody.segmentPositions[i]);
			int frame = (int)(((animTime + segmentAnimOffset*i)%1) * bodySegmentAnim.Length);
			isoRender.RenderFreeObject(bodySegmentAnim[frame], pos, bodySegmentBias);
			if(dropShadow)
				dropShadow.ShadowCast(pos);
		}
	}
}