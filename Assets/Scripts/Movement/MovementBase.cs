using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementCollider))]
public abstract class MovementBase : MonoBehaviour {

	// Use this for initialization
	public virtual void Start () {
		mCollider = GetComponent<MovementCollider>();
		transform.localPosition.Set(transform.localPosition.x, Mathf.Round(transform.localPosition.y) + mCollider.radius, transform.localPosition.z);
	}
	protected MovementCollider mCollider;
}
