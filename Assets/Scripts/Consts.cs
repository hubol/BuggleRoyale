using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Store constants here; since it's a MonoBehaviour, we can adjust it in the inspector
public class Consts : MonoBehaviour
{
	public static Consts i;

	void Awake()
	{
		i = this;
	}

	public float moveIncrement = 0.01f;

}
