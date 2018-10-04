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

	int calculateScore(bool win) // Returns an int between 1 and 5, 1 being 'F' and 5  being 'SSS+'
	{
		if (gameManager.gm.playerHp == gameManager.gm.playerMaxHp)
			return (5);
		if (gameManager.gm.playerHp > gameManager.gm.playerMaxHp - 5)
			return (4);
		if (gameManager.gm.playerHp > gameManager.gm.playerMaxHp / 2 && gameManager.gm.playerEnergy > gameManager.gm.playerStartEnergy / 2)
			return (3);
		if (gameManager.gm.playerHp > gameManager.gm.playerMaxHp / 4)
			return (2);
		return (1);
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
		scoreText.text = gameManager.gm.score.ToString();
		int score = calculateScore(win);
		if (score == 1)
		{
			gradeText.text = "F";
			gradeText.color = Color.red;
		}
		else if (score == 2)
		{
			gradeText.text = "D";
			gradeText.color = Color.red;
		}
		else if (score == 3)
		{
			gradeText.text = "C";
			gradeText.color = Color.yellow;
		}
		else if (score == 4)
		{
			gradeText.text = "A";
			gradeText.color = Color.green;
		}
		else if (score == 5)
		{
			gradeText.text = "S";
			gradeText.color = Color.green;
		}
	}
}
