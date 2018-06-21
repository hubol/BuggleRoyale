using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementCollider))]
public abstract class MovementBase : MonoBehaviour {

	// Use this for initialization
	public virtual void Start () {
		mCollider = GetComponent<MovementCollider>();
	}
	protected MovementCollider mCollider;

}
