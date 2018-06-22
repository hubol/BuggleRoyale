using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Player control hub; can "possess" different bugs and control them.
//If they hold a direction while switching, this object will keep
//sending that direction to that bug so it keeps moving.
public class PlayerSoul : MonoBehaviour
{
	public InputReceiver[] bugs;
	public int possessed = 0;   //which one of the bugs in our list are we possessing?
	public string right, up, left, down, action1, action2, action3, bugswitch;

	public bool wasSwitchDown = false;

	private void FixedUpdate()
	{
		//NOTE: to hold inputs, we just simply don't reset the inputs on the bug we switch away from!

		//Reset inputs on possessed bug
		InputReceiver bp = bugs[possessed];
		bp.left = bp.right = bp.down = bp.up = bp.action1 = bp.action2 = bp.action3 = false;


		//Send messages to possessed bug
		if(Input.GetKey(right))
			bp.right = true;
		if(Input.GetKey(left))
			bp.left = true;
		if(Input.GetKey(up))
			bp.up = true;
		if(Input.GetKey(down))
			bp.down = true;
		if(Input.GetKey(action1))
			bp.action1 = true;
		if(Input.GetKey(action2))
			bp.action2 = true;
		if(Input.GetKey(action3))
			bp.action3 = true;

		//If they switch, leave directional inputs turned on, but zero action buttons
		if(Input.GetKey(bugswitch))
		{
			if(!wasSwitchDown)
			{ 
				wasSwitchDown = true;  //only switch once on press

				bp.action1 = false;
				bp.action2 = false;
				bp.action3 = false;

				possessed = (possessed+1) % bugs.Length;
			}
		}
		else
			wasSwitchDown = false;
	}



}
