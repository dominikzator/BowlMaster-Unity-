using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour {

	public bool camOnStart = false;
	public int numberOfPlayers = 6;
	public GameObject[] playerPanels;
	public GameManager lastGameManager;
	public GameObject lastPanel;
	public GameManager currentGameManager;
	public GameObject currentPanel;
	public GameManager[] gameManagers;

	public Text winningText;
	public Text totalScoreText;
	public Button backToMenuButton;


	public GameObject helpObjects;
	public Sprite clickImage;
	public Sprite releaseImage;

	private GameManager[] tempValues = new GameManager[10];

	private MenuManager menuManager;

	private PinSetter pinSetter;

	public int iterator = 0;

	public int playersFinished = 0;

	private TurnManager turnManager;

	private Ball ball;

	public Dropdown numberOfPlayersDropdown;

	// Use this for initialization
	void Start () {
		turnManager = FindObjectOfType<TurnManager> ();
		menuManager = FindObjectOfType<MenuManager> ();
		gameManagers = FindObjectsOfType<GameManager> ();
		ball = FindObjectOfType<Ball> ();
		currentPanel = playerPanels [iterator];
		//lastPanel = playerPanels [iterator];


		for (int i = 0; i < gameManagers.Length; i++) {
			if (gameManagers [i].name == "Game Manager (player 1)") {
				tempValues [0] = gameManagers [i];
				//print ("execute 1");
			} 
			else if (gameManagers [i].name == "Game Manager (player 2)") {
				tempValues [1] = gameManagers[i];	
				//print ("execute 2");
			}
			else if (gameManagers [i].name == "Game Manager (player 3)") {
				tempValues [2] = gameManagers[i];	
				//print ("execute 2");
			}
			else if (gameManagers [i].name == "Game Manager (player 4)") {
				tempValues [3] = gameManagers[i];	
				//print ("execute 2");
			}
			else if (gameManagers [i].name == "Game Manager (player 5)") {
				tempValues [4] = gameManagers[i];	
				//print ("execute 2");
			}
			else if (gameManagers [i].name == "Game Manager (player 6)") {
				tempValues [5] = gameManagers[i];	
				//print ("execute 2");
			}
		}

		for (int i = 0; i < gameManagers.Length; i++) {
			gameManagers [i] = tempValues [i];
		}

		currentGameManager = gameManagers [iterator];
		//lastGameManager = gameManagers [iterator];
	}

	void ChangePlayerPanel (GameObject panel)
	{
		for (int i = 0; i < playerPanels.Length; i++) {
			playerPanels [i].SetActive (false);
		}
		panel.SetActive (true);
	}
	
	public void NextPlayer()
	{
		if (!AllPlayersFinished()) {
			
			lastPanel = currentPanel;
			lastGameManager = currentGameManager;

			while (true) {
				iterator++;
				if (iterator == numberOfPlayers) {
					iterator = 0;
				}
				if (gameManagers [iterator].finished == false) {
					currentPanel = playerPanels [iterator];
					currentGameManager = gameManagers [iterator];
					ball.GetComponent<MeshRenderer> ().material = turnManager.currentGameManager.ballMaterial;
					break;
				}
			}
		}
	}

	public void BowlForCurrentGameManager(int numberPins)
	{
		currentGameManager.Bowl (numberPins);
	}

	public bool AllPlayersFinished()
	{
		playersFinished = 0;
		for (int i = 0; i < numberOfPlayers; i++) {
			if (gameManagers [i].finished == true) {
				playersFinished++;
			}
		}
		if (playersFinished == numberOfPlayers) {
			//Debug.Log ("ALL PLAYERS FINISHED");

			for (int i = 0; i < numberOfPlayers; i++) {
				playerPanels [i].GetComponent<Animator> ().SetTrigger ("disappearTrigger");
			}

			return true;
		}
		else {
			
			return false;
		}
	}

	public void EndGame()
	{
		int bestScore = -1;
		int winningIndex = -1;
		Debug.Log ("Game Finished");
		for (int i = 0; i < numberOfPlayers; i++) {
			Debug.Log (playerPanels [i].GetComponent<ScoreDisplay> ().playerNameText.text.ToString()
			+ " final score: " +
			playerPanels [i].GetComponent<ScoreDisplay> ().frameTexts [9].text.ToString ());
			if (System.Int32.Parse(playerPanels [i].GetComponent<ScoreDisplay> ().frameTexts [9].text) > bestScore) {
				bestScore = System.Int32.Parse(playerPanels [i].GetComponent<ScoreDisplay> ().frameTexts [9].text);
				winningIndex = i;
			}
		}

		Debug.Log(playerPanels[winningIndex].GetComponent<ScoreDisplay>().playerNameText.text + " Won!");
		Debug.Log ("Score: " + bestScore);

		winningText.text = playerPanels [winningIndex].GetComponent<ScoreDisplay> ().playerNameText.text + " Won !";
		winningText.color = gameManagers [winningIndex].colorFrame;

		totalScoreText.text = "Total Score : " + bestScore;
		totalScoreText.color = gameManagers [winningIndex].colorFrame;

		winningText.GetComponent<Animator> ().SetTrigger ("appearTrigger");

	}

	public void RestartGame()
	{
		menuManager.gameCanvas.GetComponent<Canvas> ().enabled = false;
		menuManager.menuCanvas.GetComponent<Canvas> ().enabled = true;
		menuManager.splines [0].FollowSpline ();
	}

	public void DisappearEndTexts()
	{
		winningText.GetComponent<Animator> ().SetTrigger ("disappearTrigger");
		totalScoreText.GetComponent<Animator> ().SetTrigger ("disappearTrigger");
		backToMenuButton.GetComponent<Animator> ().SetTrigger ("disappearTrigger");
		Invoke("RestartGame", 1f);
		//numberOfPlayersDropdown.interactable = true;
	}

	public void Update()
	{
		//print (currentPanel);
		//Debug.Log(currentGameManager.name + "finished: " + currentGameManager.finished);
		//Debug.Log (currentPanel.name);
	}

	public void AppearHelp()
	{
		helpObjects.GetComponent<Animator> ().SetTrigger ("appearTrigger");
	}
}
