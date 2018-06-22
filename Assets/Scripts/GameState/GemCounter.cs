using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Counts gems on either side of the play area.
//Also, counts down the time and declares a winner.
public class GemCounter : MonoBehaviour
{
	public enum GameState
	{
		GAMEPLAY,
		WINNER
	};
	public GameState state = GameState.GAMEPLAY;

	public float timeLeft=300.0f;
	public float showWinnerTime = 20.0f;
	public Text scoreText;

	int left_count = 0;
	int right_count = 0;

	private void FixedUpdate()
	{
		//count the gems on either side of the play area.
		// Z > X means right side,
		// X < Z means left side.
		switch(state)
		{
			case GameState.GAMEPLAY:
				left_count = 0;
				right_count = 0;

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

				if( timeLeft <= 0 )
				{
					//Declare winner!
					state = GameState.WINNER;
				}
				break;

			case GameState.WINNER:
				if( left_count > right_count )
					scoreText.text = "LEFT WINS!!!";

				if( right_count > left_count )
					scoreText.text = "RIGHT WINS!!!";

				if( left_count == right_count )
					scoreText.text = "TIE!!!";

				timeLeft -= Time.fixedDeltaTime;

				if( -timeLeft > showWinnerTime )
					SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex );  //restart the level


				break;
		}
	}

}
