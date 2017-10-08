using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	private List<int> rolls = new List<int>();	//TODO make private

	private TurnManager turnManager;
	private MenuManager menuManager;
	private PinSetter pinSetter;
	private Ball ball;
	private ScoreDisplay scoreDisplay;

	public string playerName;
	public Color colorFrame;
	public Material ballMaterial;

	public bool finished = false;

	private bool infosShown = false;


	// Use this for initialization
	void Start () {
		turnManager = GameObject.FindObjectOfType<TurnManager> ();
		menuManager = GameObject.FindObjectOfType<MenuManager> ();
		pinSetter = GameObject.FindObjectOfType<PinSetter> ();
		ball = GameObject.FindObjectOfType<Ball> ();
		scoreDisplay = GameObject.FindObjectOfType<ScoreDisplay> ();

		//Testing for EndGame, if rolls below are added, One move is left for all players to finish
		//rolls.Add (0);
		//rolls.Add (0);
		//rolls.Add (0);
		//rolls.Add (0);
		//rolls.Add (0);
		//rolls.Add (0);
		//rolls.Add (0);
		//rolls.Add (0);
		//rolls.Add (0);
		//rolls.Add (0);
		//rolls.Add (0);
		//rolls.Add (0);
		//rolls.Add (0);
		//rolls.Add (0);
		//rolls.Add (0);
		//rolls.Add (0);
		//rolls.Add (0);
		//rolls.Add (0);
		//rolls.Add (0);
	}
	
	public void Bowl (int pinFall)
	{
		rolls.Add (pinFall);
		pinSetter.PerformAction (ActionMasterOld.NextAction (rolls));

		try{

			//Fills score to ScorePanel for current gameManager and CurrentPanel
			for(int i=0; i<turnManager.numberOfPlayers;i++)
			{
				if (gameObject.name == turnManager.gameManagers[i].name && turnManager.currentPanel.name == turnManager.playerPanels[i].name)
				{
					turnManager.currentPanel.GetComponent<ScoreDisplay>().FillRolls (rolls);
					turnManager.currentPanel.GetComponent<ScoreDisplay>().FillFrames(ScoreMaster.ScoreCumulative(rolls));
					Debug.Log(ScoreMaster.ScoreCumulative(rolls)[ScoreMaster.ScoreCumulative(rolls).Count-1]);
					menuManager.scoresPreview[i].text = ScoreMaster.ScoreCumulative(rolls)[ScoreMaster.ScoreCumulative(rolls).Count-1].ToString();
				}
			}
		}
		catch{
			//Debug.Log ("Something went wrong");
		}

	}

	void Update()
	{

	}


}
