using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGame : MonoBehaviour {


	public static EndGame eg;

	public Text resultText;
	public Text scoreText;
	public Text gradeText;
	public GameObject endGamePanel;

	void Awake()
	{
		if (eg == null)
			eg = this;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void print_panel(bool win)
	{
		endGamePanel.SetActive(true);
		if (win)
		{
			resultText.text = "You won !";
			resultText.color = Color.green;
		}
		else
		{
			resultText.text = "You lost !";
			resultText.color = Color.red;
		}
	}
}
