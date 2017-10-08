using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

	public Ball ball;

	public Vector3 offset;

	//Values for Smooth Camera Reset Using Vector3.Lerp
	private Vector3 startPosition;		
	private Vector3 startRotation;
	private Vector3 watchingPinPosition;
	private Vector3 watchingPinRotation;
	private float currentLerpTime = 0;
	private float lerpTime = 2;

	private TurnManager turnManager;
	private MenuManager menuManager;
	private PinSetter pinSetter;

	public bool splineCameraStarted = false;

	//Help flags for Camera states
	public bool cameraWantsGoBack = false;
	public bool cameraIsWatchingPins = false;

	public bool gameFinishedFlag = false;

	// Use this for initialization
	void Start () {
		turnManager = FindObjectOfType<TurnManager> ();
		menuManager = FindObjectOfType<MenuManager> ();
		pinSetter = FindObjectOfType<PinSetter> ();
		startPosition = transform.position;
		startRotation = transform.rotation.eulerAngles;
	}

	// Update is called once per frame
	void Update () {

		//Debug.Log (menuManager.splines[1].transform.position);

		if (menuManager.splines [2].transform.position == menuManager.watchingPinsEndVector && pinSetter.pinsLowered && !turnManager.AllPlayersFinished()) {
			//Debug.Log ("Camera Watching Pins End Vector");
			menuManager.splines [3].FollowSpline ();
			ball.Reset ();
		}

		if (pinSetter.nextPlayerReady && splineCameraStarted && turnManager.AllPlayersFinished () == false) {

			pinSetter.nextPlayerReady = false;

			turnManager.NextPlayer ();
			turnManager.lastPanel.GetComponent<Animator> ().SetTrigger ("disappearTrigger");
			turnManager.currentPanel.GetComponent<Animator> ().SetTrigger ("appearTrigger");

		} 
		else if (turnManager.AllPlayersFinished () && !gameFinishedFlag) {
			gameFinishedFlag = true;
			turnManager.EndGame ();
		}
		if (cameraWantsGoBack && !splineCameraStarted) {							//If camera Reset flag is set

			splineCameraStarted = true;
			cameraWantsGoBack = false;
			if (!ball.BallOnStart) {
				menuManager.splines [1].FollowSpline ();
			}

		}
		else if (ball.transform.position.z <= 1829f) {		//normal Camera follow, when ball is released
			
			transform.position = ball.transform.position + offset;
		} 
		else if (ball.transform.position.z > 1829f)			//Camera watching pins after ball throw
		{
			//turnManager.ballReady = false;
			if (!cameraIsWatchingPins) {
				WatchPinsCamera ();
			}
		}
	}

	public void WatchPinsCamera()
	{
		menuManager.splines [2].FollowSpline ();
		cameraIsWatchingPins = true;
	}
		
	public void ResetCamera()
	{
		cameraWantsGoBack = true;
		splineCameraStarted = false;
		currentLerpTime = 0;
		cameraIsWatchingPins = false;
	}
}
