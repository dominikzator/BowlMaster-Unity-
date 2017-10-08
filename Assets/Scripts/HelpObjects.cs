using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpObjects : MonoBehaviour {

	public Sprite clickSprite;
	public Sprite releaseSprite;

	public Image clickImage;
	public Text text;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetText(string input)
	{
		text.text = input;
	}

	public void SetSprite(Sprite input)
	{
		clickImage.GetComponent<Image> ().sprite = input;
	}

}
