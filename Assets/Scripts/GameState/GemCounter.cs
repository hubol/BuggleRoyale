using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Counts gems on either side of the play area.
//Also, counts down the time and declares a winner.
public class GemCounter : MonoBehaviour
{
	public float timeLeft=300.0f;
	public Text scoreText;

	private void FixedUpdate()
	{
		//count the gems on either side of the play area.
		// Z > X means right side,
		// X < Z means left side.

		int left_count = 0;
		int right_count = 0;

		foreach(MovementCollider mc in ColliderManager.i.colliders)
		{
			IsGem gem = mc.GetComponent<IsGem>();
			if( gem != null )
			{
				Vector3 pos = gem.transform.position;
				if(pos.z > pos.x)
					right_count++;
				if( pos.x > pos.z )
					left_count++;
			}
		}


		scoreText.text = left_count + "                             " + Mathf.Floor(timeLeft) + "                             " + right_count;

		timeLeft -= Time.fixedDeltaTime;		
	}

}
