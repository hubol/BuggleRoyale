using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderManager : MonoBehaviour {
	public List<MovementCollider> colliders = new List<MovementCollider>();
	public static ColliderManager i;

	void Awake()
	{
		i = this;
	}
}
