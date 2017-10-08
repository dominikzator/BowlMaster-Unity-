using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

	public Vector3 launchVelocity;
	public bool inPlay = false;
	public Camera cam;

	private Vector3 ballStartPosition;
	private Rigidbody rigidbody;
	private AudioSource audioSource;

	private float lastLaunchTime;

	private TurnManager turnManager;

	public bool BallOnStart = false;

	private PinCounter pinCounter;

	private MenuManager menuManager;

	public bool ballAfterReset;

	public GameObject helpObjects;

	// Use this for initialization
	void Start () {
		turnManager = FindObjectOfType<TurnManager> ();
		pinCounter = FindObjectOfType<PinCounter> ();
		rigidbody = GetComponent<Rigidbody> ();
		menuManager = FindObjectOfType<MenuManager> ();
		ballAfterReset = true;
		rigidbody.useGravity = false;
		ballStartPosition = transform.position;

		//rigidbody.velocity = new Vector3 (0, 0, 400);	// strike Ball for testing

	}

	public void Launch (Vector3 velocity)
	{
		helpObjects.GetComponent<Animator> ().SetTrigger ("disappearTrigger");
		inPlay = true;
		ballAfterReset = false;
		menuManager.camOnStart = false;
		lastLaunchTime = Time.time;
		rigidbody.useGravity = true;
		rigidbody.velocity = velocity;

		audioSource = GetComponent<AudioSource> ();
		audioSource.Play ();
	}
	
	// Update is called once per frame
	void Update () {

		BallOnStart = (ballStartPosition == transform.position);

		//Debug.Log (BallOnStart);
		//Debug.Log("Ball On Start:"+ BallOnStart);
		//Debug.Log ("In play: " + inPlay);
		//Debug.Log ("Ball Left Box: " + pinCounter.ballLeftBox);
		if (inPlay && !pinCounter.ballLeftBox && !turnManager.AllPlayersFinished()) {
			if (Time.time - lastLaunchTime > 15f) {
				Debug.Log ("Ball 15 seconds inplay");
				ballAfterReset = true;
				Reset ();
			}

		}
	}

	public void Reset()
	{
			pinCounter.pinsSettled = false;
			inPlay = false;
			//transform.position = ball.transform.position + offset;
			transform.position = ballStartPosition;
			cam.transform.position = transform.position + cam.GetComponent<CameraControl> ().offset;
			rigidbody.velocity = new Vector3 (0, 0, 0);
			transform.eulerAngles = new Vector3 (0, 0, 0);
			rigidbody.Sleep ();
			rigidbody.useGravity = false;
			
			cam.GetComponent<CameraControl> ().ResetCamera ();
	}

	public void ResetButton()
	{
		if (!pinCounter.ballLeftBox) {
			pinCounter.pinsSettled = false;
			inPlay = false;
			//transform.position = ball.transform.position + offset;
			transform.position = ballStartPosition;
			cam.transform.position = transform.position + cam.GetComponent<CameraControl> ().offset;
			rigidbody.velocity = new Vector3 (0, 0, 0);
			transform.eulerAngles = new Vector3 (0, 0, 0);
			rigidbody.Sleep ();
			rigidbody.useGravity = false;

			cam.GetComponent<CameraControl> ().ResetCamera ();
		}
	}

	
}
