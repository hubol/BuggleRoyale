using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementCollider))]
[RequireComponent(typeof(InputReciever))]
public abstract class PickerUpper<T> : MonoBehaviour where T : MonoBehaviour {
	public bool autoPickup = true;
	[Range(0,2)]
	public int pickup;
	[Range(0,2)]
	public int drop;

	private T held;

	private MovementCollider mCollider;
	private InputReciever receiver;
	void Start(){
		mCollider = GetComponent<MovementCollider>();
		receiver = GetComponent<InputReciever>();
	}

	private T dropped;
	// Update is called once per frame
	void FixedUpdate () {
		// If we are not holding anything
		if (held == null){
			if (autoPickup || receiver.actions[pickup]){
				bool nullDropped = true;
				// Check all colliders
				foreach(MovementCollider mc in ColliderManager.i.colliders){
					// If we have a union
					if (mc.HasUnion(mCollider)){
						T t = mc.GetComponent<T>();
						if (t == dropped)
							nullDropped = false;
						// If the given MovementCollider has a T Component, update held
						else if (t != null){
							dropped = null;
							held = t;
							OnHeldChanged(held, false);
							return;
						}
					}
				}
				if (nullDropped)
					dropped = null;
			}
		}
		// If we are holding something
		else {
			// Update held position
			OnUpdateWithHeld(held);
			// Drop it
			if (receiver.actions[drop]){
				OnHeldChanged(held, true);
				dropped = held;
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
