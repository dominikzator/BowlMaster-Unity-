using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinCounter : MonoBehaviour {
	public Text standingDisplay;

	public bool ballLeftBox = false;


	private int lastStandingCount = -1;
	private int lastSettledCount = 10;
	private float lastChangeTime;
	private TurnManager turnManager;

	private Ball ball;

	public bool pinsSettled = false;
	// Use this for initialization
	void Start () {
		ball = FindObjectOfType<Ball> ();
		turnManager = FindObjectOfType<TurnManager> ();
		//print (gameManagers [0]);
		//print (gameManagers [1]);
	}
	
	// Update is called once per frame
	void Update () {
		standingDisplay.text = CountStanding ().ToString ();

		if (ballLeftBox && !pinsSettled) {
			UpdateStandingCountAndSettle ();
			standingDisplay.color = Color.red;
		}
		else if (ball.BallOnStart) {
			standingDisplay.color = Color.black;
		}
		else if (pinsSettled) {
			standingDisplay.color = Color.green;
		}
	}

	public void Reset ()
	{
		lastSettledCount = 10;
	}

	void OnTriggerExit(Collider col)
	{
		GameObject thingHit = col.gameObject;
		if (thingHit.GetComponent<Ball>()) {
			ballLeftBox = true;
		}
	}
		
	void UpdateStandingCountAndSettle()
	{
		//Update the lastStandingCount
		//Call PinsHaveSettled() when they have
		int currentStanding = CountStanding();

		if (currentStanding != lastStandingCount) {
			lastChangeTime = Time.time;
			lastStandingCount = currentStanding;
			return;
		}

		float settleTime = 3f; //How long to wait to consider pins settled
		if ((Time.time - lastChangeTime) > settleTime) {	//If last change > 3s ago
			PinsHaveSettled();
		}
	}

	void PinsHaveSettled()
	{
		pinsSettled = true;
		int standing = CountStanding ();
		int pinFall = lastSettledCount - standing;
		lastSettledCount = standing;
		//Debug.Log (pinFall);
		turnManager.currentGameManager.Bowl (pinFall);

		lastStandingCount = -1;	//Indicates pins have settled, and ball not back in box
		ballLeftBox = false;

	}

	public int CountStanding()
	{
		int PinStandingCounter = 0;
		Pin[] pins = FindObjectsOfType<Pin> ();

		foreach (Pin myPin in pins) {
			if (myPin.IsStanding ()) {
				PinStandingCounter++;
			}
		}

		return PinStandingCounter;
	}
}
