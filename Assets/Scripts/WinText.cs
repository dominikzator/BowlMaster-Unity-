using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinText : MonoBehaviour {

	public Text totalScoreText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void SetScoreTrigger()
	{
		totalScoreText.GetComponent<Animator> ().SetTrigger ("appearTrigger");
	}
}
