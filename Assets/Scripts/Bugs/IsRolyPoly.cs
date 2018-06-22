using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputReceiver))]
[RequireComponent(typeof(MovementCrawl))]
[RequireComponent(typeof(MovementCharge))]
[RequireComponent(typeof(GemPicker))]
public class IsRolyPoly : MonoBehaviour {
	[Range(0,2)]
	public int retreat;

	// Roly Poly behaviors
	private MovementCrawl crawl;
	private MovementCharge charge;
	private MovementKeyboard active;

	private InputReceiver inputReceiver;

	// Begin in crawl
	void Start () { 
		crawl = GetComponent<MovementCrawl>();
		charge = GetComponent<MovementCharge>();
		inputReceiver = GetComponent<InputReceiver>();
		SetActive(crawl);
	}

	// Helper function for switching active behavior
	private void SetActive(MovementKeyboard next){
		if (next == crawl)
			charge.enabled = false;
		else
			crawl.enabled = false;
		next.enabled = true;
		active = next;
	}

	private bool wasRetreat = false;
	// Behavior transitions
	void FixedUpdate(){
		bool freshRetreat = inputReceiver.actions[retreat] && !wasRetreat;

		// Crawl to Charge/Retreat
		if (active == crawl){
			if (freshRetreat){
				SetActive(charge);
				charge.Ready();
			}
		}
		// Charge/Retreat to Crawl
		else {
			if (charge.Completed() || (charge.IsReady() && freshRetreat))
				SetActive(crawl);
		}

		wasRetreat = inputReceiver.actions[retreat];
	}
}
