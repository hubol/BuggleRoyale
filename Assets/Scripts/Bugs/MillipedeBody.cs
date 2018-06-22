using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Copyright (C) Sebastian Janisz, January 2016.
//All rights reserved.




//Logic to drive the positioning of body segments along the path that this GameObject takes.
//This is basically a 3D version of the player movement from the classic "Snake" game.
//Output is the public Vector3[], segmentPositions.
public class MillipedeBody : MonoBehaviour
{
	public int numBodySegments = 16;
	public float bodySegmentDistance = 1.0f;  //this will be counted off in the positional buffer to position each segment
	public Vector3[] segmentPositions;

	public bool initSegments = true;   //set to false if you already set them up in the inspector
	public bool flipXAndZAtStart = false;   //hack to get opposite millipede to be symmetrical


	//Historical record of transform.positions.
	//These are used to space out the body segments over the path this object has travelled.
	//LinkedList is used for efficiency as a first-in-last-out structure.
	LinkedList<Vector3> posBuffer = new LinkedList<Vector3>();

	private void Awake()
	{
		if(initSegments)
			segmentPositions = new Vector3[numBodySegments];
		else
			//populate posBuffer with segmentPositions
			if(!flipXAndZAtStart)
				for(int i=0; i<segmentPositions.Length; i++)
					posBuffer.AddLast(transform.TransformPoint(segmentPositions[i]));
			else
				for(int i=0; i<segmentPositions.Length; i++)
				{
					Vector3 pos =  transform.TransformPoint(segmentPositions[i]);
					float temp = pos.z;
					pos.z = pos.x;
					pos.x = temp;

					posBuffer.AddLast(pos);
				}

		if(flipXAndZAtStart)
		{
			//flip our position, too
			Vector3 pos = transform.position;
			float temp = pos.z;
			pos.z = pos.x;
			pos.x = temp;

			transform.position = pos;
		}
	}



	void FixedUpdate()
	{
		//Use fixed timestep for guaranteed smooth motion.
		
		
		//Push current position onto posBuffer, assuming we've moved at all.
		Vector3 pos = transform.localPosition;
		
		if( posBuffer.Count==0 || pos != posBuffer.First.Value)
			posBuffer.AddFirst(pos);


		//Now: walk through the positional buffer.  At each step, subtract from the distance left to the next body segment.
		//When it runs out, we will need to interpolate somewhere between the two recorded positions, based on distance.
		segmentPositions[0] = Vector3.zero;

		float dist_left = bodySegmentDistance;  //physical distance left to travel along the buffer before we stop and place a body segment
		int segment_cnt = 1;
		Vector3 last_pos = pos;
		int buffer_i=0;
		foreach( Vector3 rec_pos in posBuffer )
		{
			//get physical distance to previous pos.
			Vector3 d = rec_pos - last_pos;
			float mag_total = d.magnitude;
			float mag_left = mag_total;

			//eat away at mag_left, placing body segments along the way as necessary.
			while( mag_left > 0 )
			{
				if( mag_left >= dist_left )
				{
					mag_left -= dist_left;  //move mag_left along the line just enough; now, use it to lerp

					float lerp = (mag_total - mag_left) / mag_total;

					//place the body segment using this lerp value between last_pos and this one
					//We have to subtract our current local position, since tubeMesh positions need to be relative.
					segmentPositions[segment_cnt++] = Vector3.Lerp(last_pos, rec_pos, lerp) - pos;


					if( segment_cnt >= numBodySegments )
						break;   //all body segments are done, so break out of while loop

					//reload distance for next body segment
					dist_left = bodySegmentDistance;
				}
				else  //mag_left just chips away at dist left
				{ 
					dist_left -= mag_left;
					mag_left = 0;
				}
			}


			//set last_pos for next iteration
			last_pos = rec_pos;

			//keep track of buffer index; this allows us to truncate unnecessary records
			buffer_i++;

			//if we have placed all body segments, exit the foreach loop and truncate all remaining buffer indices
			if( segment_cnt >= numBodySegments )
				break;   //all body segments are done, so break out of while loop
		}


		while( segment_cnt < numBodySegments )  //in case the buffer was too small to accomodate all body segments; just place them at the end
			segmentPositions[segment_cnt++] = posBuffer.Last.Value - pos;
		

		//Drop indices that are too far back to be useful
		int slots_remaining = posBuffer.Count - buffer_i;   //if the full loop managed to execute, this would be 0
		for( int i=0; i<slots_remaining; i++ )
			posBuffer.RemoveLast();
	}



	private void OnDrawGizmos()
	{
		//positions are all relative to local space
		for(int i=0; i<segmentPositions.Length; i++)
			Gizmos.DrawSphere(transform.TransformPoint(segmentPositions[i]), 0.3f);
	}
}
