using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

	public Camera cam;
	public Canvas menuCanvas, gameCanvas;

	public GameObject[] playerRaws;

	public Slider red1, green1, blue1;

	public Slider red2, green2, blue2;

	public Slider red3, green3, blue3;

	public Slider red4, green4, blue4;

	public Slider red5, green5, blue5;

	public Slider[,] sliders;

	public Dropdown[] materialDropdowns;
	public Dropdown numberOfPlayers;

	public Dropdown laneDropdown;

	public InputField[] inputFields;

	public Image[] colors;

	public GameObject[] ballPreviews;

	public GameObject floor;
	public GameObject swiper;

	private TurnManager turnManager;

	public SplineController[] splines;

	public GameObject[] scores;

	private Vector3 splineStartGamePos = new Vector3 (0f,40f, -34.7f);

	public Vector3 watchingPinsEndVector = new Vector3 (303.9f, 158.6f, 1606.9f);

	public bool camOnStart = false;

	private Ball ball;

	private bool gameStarted = false;

	public Text[] playersPreview;
	public Text[] scoresPreview;

	public GameObject helpObjects;

	public GameObject touchInput;

	void Start () {
		
		sliders = new Slider[,]{{red1,green1,blue1}, {red2,green2,blue2},{red3,green3,blue3},{red4,green4,blue4},{red5,green5,blue5}};

		for (int i = 0; i < 5; i++) {
			SwitchColorPlayer (i);
		}

		turnManager = FindObjectOfType<TurnManager> ();
		ball = FindObjectOfType<Ball> ();
		gameCanvas.enabled = false;
		splines = cam.GetComponents<SplineController> ();
		splines [0].FollowSpline ();
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (splines [1].transform.position);
		if (splines [1].transform.position == splineStartGamePos) {
			//Debug.Log ("Game Started");	
			if (!gameStarted) {
				FindObjectOfType<PinSetter> ().currentAnimator.SetTrigger ("appearTrigger");
				helpObjects.GetComponent<Animator> ().SetTrigger ("appearTrigger");
				gameStarted = true;

			}
			camOnStart = true;
			//turnManager.ballReady = true;
			cam.GetComponent<CameraControl> ().splineCameraStarted = false;
		} 
		//else {
		//	camOnStart = false;
		//}
		//Debug.Log ("CamOnStart:" + camOnStart);
	}
	public void SwitchColorPlayer(int value)
	{
		colors[value].color = new Color32 ((byte)sliders[value,0].value, (byte)sliders[value,1].value, (byte)sliders[value,2].value, (byte)255f);
	}
		
	public void ChangeBallMaterial(int index)
	{
		string currentMaterial = materialDropdowns[index].options [materialDropdowns[index].value].text;
		Material yourMaterial = Resources.Load(currentMaterial) as Material;
		ballPreviews[index].GetComponent<MeshRenderer> ().material = yourMaterial;
	}

	public void ChangeLaneMaterial()
	{
		string currentMaterial = laneDropdown.options [laneDropdown.value].text;
		currentMaterial += " Lane";
		Material yourMaterial = Resources.Load(currentMaterial) as Material;
		floor.GetComponent<MeshRenderer> ().material = yourMaterial;

		currentMaterial = currentMaterial.Replace ("Lane", "Swiper");
		yourMaterial = Resources.Load(currentMaterial) as Material;
		swiper.GetComponent<MeshRenderer> ().material = yourMaterial;
	}

	public void UpdatePlayerRaws()
	{
		for (int i = 0; i < 5; i++) {
			playerRaws [i].SetActive (false);
		}

		for (int i = 0; i <= numberOfPlayers.value; i++) {
			playerRaws [i].SetActive (true);
		}
	}

	public void StartTheGame()
	{
		int playersValue = numberOfPlayers.value + 1;

		turnManager.numberOfPlayers = playersValue;

		for (int i = 0; i < 5; i++) {
			if (inputFields [i].text != "") {
				turnManager.gameManagers [i].playerName = inputFields [i].text;
			} else {
				turnManager.gameManagers [i].playerName = "Player " + (i + 1).ToString ();
			}
			turnManager.gameManagers [i].colorFrame = colors [i].color;
			turnManager.gameManagers [i].ballMaterial = ballPreviews [i].GetComponent<MeshRenderer> ().material;
		}

		ChangeCanvas ();

		SetPreviews ();


		for (int i = 0; i < turnManager.numberOfPlayers; i++) {
			ScoreDisplay.panelColors[i] = turnManager.gameManagers [i].colorFrame;
			scores [i].GetComponent<ScoreDisplay> ().SetFrameColorToAll ();
			scores [i].GetComponent<ScoreDisplay> ().SetPlayerName (turnManager.gameManagers [i].playerName);
		}


	}

	void SetPreviews ()
	{
		for (int i = 0; i < 5; i++) {
			scoresPreview [i].enabled = false;
			playersPreview [i].enabled = false;
		}
		for (int i = 0; i < turnManager.numberOfPlayers; i++) {
			scoresPreview [i].enabled = true;
			playersPreview [i].enabled = true;
		}

		for (int i = 0; i < turnManager.numberOfPlayers; i++) {
			playersPreview [i].text = turnManager.gameManagers [i].playerName;
			playersPreview[i].color = turnManager.gameManagers [i].colorFrame;

			scoresPreview [i].text = "0";
			scoresPreview[i].color = turnManager.gameManagers [i].colorFrame;
		}
	}

	void ChangeCanvas()
	{
		menuCanvas.GetComponent<Canvas> ().enabled = false;
		gameCanvas.GetComponent<Canvas> ().enabled = true;
		ball.GetComponent<MeshRenderer> ().material = turnManager.currentGameManager.ballMaterial;
		Debug.Log ("After follow spline");
		//splines [0].DisableNodeObjects ();
		splines[1].FollowSpline();

		//Debug.Log (splines [1].transform.position);
	}
}
