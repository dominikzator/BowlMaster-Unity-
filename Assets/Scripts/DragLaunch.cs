using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Ball))]
public class DragLaunch : MonoBehaviour {

	public bool multiDrag = false;

	private Vector3 dragStart, dragEnd;
	private float startTime, endTime;
	private float xValue = 0;
	private Ball ball;

	public Camera cam;

	private MenuManager menuManager;
	private TurnManager turnManager;
	private PinSetter pinSetter;

	public bool skipped = false;

	// Use this for initialization
	void Start () {
		ball = GetComponent<Ball> ();
		turnManager = FindObjectOfType<TurnManager> ();
		menuManager = FindObjectOfType<MenuManager> ();
		pinSetter = FindObjectOfType<PinSetter> ();
	}

	public void MoveStart (float amount)
	{
		if(!ball.inPlay)
		{
			xValue += amount;
			//Debug.Log ("Ball moved " + amount);
			ball.transform.position = new Vector3 (Mathf.Clamp(xValue,-52f,52f), ball.transform.position.y, ball.transform.position.z);
			if (xValue < -52f) {
				xValue = -52f;
			}
			if (xValue > 52f) {
				xValue = 52f;
			}
		}
	}
	
	public void DragStart()
	{
		if (ball.BallOnStart && menuManager.camOnStart) {
			if (!ball.inPlay || multiDrag) {
				dragStart = Input.mousePosition;
				startTime = Time.time;
			}
		}
	}

	public void DragEnd()
	{
		if (ball.BallOnStart && menuManager.camOnStart) {
			if (!ball.inPlay || multiDrag) {
				dragEnd = Input.mousePosition;
				endTime = Time.time;
				float dragDuration = endTime - startTime;

				float launchSpeedX = (dragEnd.x - dragStart.x) / (dragDuration * 3);
				float launchSpeedZ = (dragEnd.y - dragStart.y) / dragDuration;

				Vector3 launchVelocity = new Vector3 (launchSpeedX, 0, launchSpeedZ);
				ball.Launch (launchVelocity);
				skipped = false;
				FindObjectOfType<PinSetter> ().pinsLowered = false;
				//cam.GetComponent<CameraControl> ().splineCameraStarted = false;
			}
		}
	}

	public void DragClick()
	{
		if (cam.GetComponent<CameraControl> ().cameraIsWatchingPins || cam.GetComponent<CameraControl> ().splineCameraStarted || ball.ballAfterReset) {

			if (!turnManager.AllPlayersFinished ()) {
				skipped = true;
				menuManager.splines [3].FollowSpline ();
				ball.Reset ();
			}
		}
	}
}
