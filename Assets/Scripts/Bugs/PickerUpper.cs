using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementCollider))]
public abstract class PickerUpper<T> : MonoBehaviour where T : MonoBehaviour {
	public bool autoPickup = true;
	public string pickup;
	public string drop;

	private T held;

	private MovementCollider mCollider;
	void Start(){
		mCollider = GetComponent<MovementCollider>();
	}

	// Update is called once per frame
	void FixedUpdate () {
		// If we are not holding anything
		if (held == null){
			if (autoPickup || Input.GetKey(pickup)){
				// Check all colliders
				foreach(MovementCollider mc in ColliderManager.i.colliders){
					// If we have a union
					if (mc.HasUnion(mCollider)){
						T t = mc.GetComponent<T>();
						// If the given MovementCollider has a T Component, update held
						if (t != null){
							held = t;
							OnHeldChanged(held, false);
							return;
						}
					}
				}
			}
		}
		// If we are holding something
		else {
			// Update held position
			OnUpdateWithHeld(held);
			// Drop it
			if (Input.GetKey(drop)){
				OnHeldChanged(held, true);
				held = null;
			}
		}
	}

	protected virtual void OnHeldChanged(T t, bool setEnabled){
		MovementBase mb = t.gameObject.GetComponent<MovementBase>();
		if (mb != null)
			mb.enabled = setEnabled;
		MovementCollider mc = t.gameObject.GetComponent<MovementCollider>();
		if (mc != null)
			mc.enabled = setEnabled;
	}

	protected virtual void OnUpdateWithHeld(T t){
		t.transform.localPosition = new Vector3(transform.localPosition.x, mCollider.maxY, transform.localPosition.z);
	}
}
