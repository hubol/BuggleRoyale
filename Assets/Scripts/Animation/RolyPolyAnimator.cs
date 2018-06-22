using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Chooses animations for Roly Poly object.
[RequireComponent(typeof(MovementGravity), typeof(FreeObjectAnims))]
public class RolyPolyAnimator : MonoBehaviour
{
	MovementGravity movementGravity;
	FreeObjectAnims freeObjectAnims;

	void Awake ()
	{
		movementGravity = GetComponent<MovementGravity>();
		freeObjectAnims = GetComponent<FreeObjectAnims>();
	}
	
	void Update ()
	{
		if(movementGravity.applyGravity)
			freeObjectAnims.CurAnim = 0;
		else  //if we aren't applying gravity, we must be climbing
			freeObjectAnims.CurAnim = 1;
	}
}
