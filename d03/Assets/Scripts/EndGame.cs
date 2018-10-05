using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour {


	public static EndGame eg;

	public Text resultText;
	public Text scoreText;
	public Text gradeText;
	public GameObject endGamePanel;

	public Button tryAgainButton;
	public Button nextLevelButton;

	public string nextLevel;

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

	public void nextLevelButtonPress()
	{
		SceneManager.LoadScene(nextLevel);
	}
	public void tryAgainButtonPress()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	int calculateScore(bool win) // Returns an int between 1 and 5, 1 being 'F' and 5  being 'SSS+'
	{
		int score;
		if (gameManager.gm.playerHp == gameManager.gm.playerMaxHp)
			score = 5;
		else if (gameManager.gm.playerHp > gameManager.gm.playerMaxHp - 5)
			score = 4;
		else if (gameManager.gm.playerHp > gameManager.gm.playerMaxHp / 2 && gameManager.gm.playerEnergy > gameManager.gm.playerStartEnergy / 2)
			score = 3;
		else if (gameManager.gm.playerHp > gameManager.gm.playerMaxHp / 4)
			score = 2;
		else
			score = 1;
		if (win && score < 5)
			score++;
		return (score);
			
	}

	public void print_panel(bool win)
	{
		PauseMenu.paused = true;
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
		if (!win)
		{
			tryAgainButton.gameObject.SetActive(true);
			nextLevelButton.gameObject.SetActive(false);
		}
		else
		{
			tryAgainButton.gameObject.SetActive(false);
			nextLevelButton.gameObject.SetActive(true);
		}
	}
}
