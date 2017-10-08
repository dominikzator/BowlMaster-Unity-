using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinSetter : MonoBehaviour {

	public GameObject pinSet;

	public bool nextPlayerReady = false;
	public Camera cam;


	//private ActionMaster actionMaster = new ActionMaster();
	private TurnManager turnManager;
	private Ball ball;
	private Animator animator;
	private PinCounter pinCounter;

	public Animator currentAnimator;
	public Animator previousAnimator;
	public bool pinsLowered = true;

	// Use this for initialization
	void Start () {
		pinsLowered = true;
		turnManager = FindObjectOfType<TurnManager> ();
		ball = FindObjectOfType<Ball> ();
		animator = GetComponent<Animator> ();
		pinCounter = FindObjectOfType<PinCounter> ();

		previousAnimator = FindObjectOfType<TurnManager> ().currentPanel.GetComponent<Animator> ();
		currentAnimator = FindObjectOfType<TurnManager> ().currentPanel.GetComponent<Animator> ();

	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log ("PinsLowered: " + pinsLowered);
	}

	public void RaisePins()
	{
		//Debug.Log("Raising Pins");

		Pin[] pins = FindObjectsOfType<Pin> ();

		foreach (Pin myPin in pins) {
			myPin.RaiseIfStanding ();
		}
	}

	public void LowerPins()
	{
		//Debug.Log ("Lowering pins");
		//raise standing pins only by distanceToRaise

		Pin[] pins = FindObjectsOfType<Pin> ();

		foreach (Pin myPin in pins) {
			myPin.Lower ();
		}
		if (ball.GetComponent<DragLaunch>().skipped && !turnManager.AllPlayersFinished()) {
			ball.Reset ();

		}
		//turnManager.ballReady = true;
		pinsLowered = true;
	}

	public void RenewPins()
	{
		//Debug.Log ("Renewing pins");
		Instantiate (pinSet, new Vector3 (0, 40, 1829), Quaternion.identity);
	}

	public void PerformAction (ActionMasterOld.Action action)
	{

		if (action == ActionMasterOld.Action.Tidy) {
			animator.SetTrigger ("tidyTrigger");	
		} 
		else if (action == ActionMasterOld.Action.Reset
			|| action == ActionMasterOld.Action.EndTurn) {
			//Debug.Log (action);
			nextPlayerReady = true;
			animator.SetTrigger ("resetTrigger");
			pinCounter.Reset ();
		}
		else if (action == ActionMasterOld.Action.EndGame) {
			nextPlayerReady = true;
			turnManager.currentGameManager.finished = true;
			animator.SetTrigger ("resetTrigger");
			pinCounter.Reset ();
			Debug.Log (turnManager.currentGameManager.name + " has finished the game");
		}

	}


}
