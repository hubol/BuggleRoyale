using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementCollider))]
[RequireComponent(typeof(InputReceiver))]
public abstract class PickerUpper<T> : MonoBehaviour where T : MonoBehaviour {
	public bool autoPickup = true;
	[Range(0,2)]
	public int pickup;
	[Range(0,2)]
	public int drop;

	private T held;

	protected MovementCollider mCollider;
	protected InputReceiver receiver;
	void Start(){
		mCollider = GetComponent<MovementCollider>();
		receiver = GetComponent<InputReceiver>();
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
						if (t != null){
							// We just threw this away, we don't want it
							if (t == dropped)
								nullDropped = false;
							// If the given MovementCollider has a T Component, update held
							else if (CanPickUp(t)){
								dropped = null;
								held = t;
								OnHeldChanged(held, false);
								return;
							}
						}
					}
				}
				// We didn't have a union with anyone
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
				ForceDrop();
			}
		}
	}

	// Determine eligibility of t
	protected virtual bool CanPickUp(T t){ return true; }

	// Drop the held Object
	public void ForceDrop(){
		if (held != null){
			OnHeldChanged(held, true);
			dropped = held;
			held = null;
		}
	}

	// Pickup / Drop events
	protected virtual void OnHeldChanged(T t, bool setEnabled){
		MovementBase mb = t.gameObject.GetComponent<MovementBase>();
		if (mb != null)
			mb.enabled = setEnabled;
		foreach(MovementCollider mc in t.gameObject.GetComponents<MovementCollider>())
			mc.enabled = setEnabled;
	}

	// Called every FixedUpdate to "carry" the object with you
	protected virtual void OnUpdateWithHeld(T t){
		t.transform.localPosition = new Vector3(transform.localPosition.x, mCollider.maxY, transform.localPosition.z);
	}
}
