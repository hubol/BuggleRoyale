using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Spins object around y axis
public class SimpleSpin : MonoBehaviour
{
	public float spinSpeed = 360.0f;

	Quaternion baseRot;

	float yangle=0;

	private void Start()
	{
		baseRot = transform.localRotation;	
	}

	private void Update()
	{
		yangle += Time.deltaTime * spinSpeed;
		transform.localRotation = Quaternion.Euler(0, yangle, 0) * baseRot;
	}

}
