using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

	public gameManager gameManager;
	
	private bool paused = false;
	public GameObject panel;
	public GameObject secondPanel;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!paused)
		{
			if (Input.GetKey(KeyCode.Escape))
			{
				paused = true;
				gameManager.pause(true);
				panel.SetActive(true);
			}
		}
	}

	public void Reprendre()
	{
		gameManager.pause(false);
		paused = false;
		panel.SetActive(false);
		secondPanel.SetActive(false);
	}

	 public void Quitter_un()
	 {
		panel.SetActive(false);
		secondPanel.SetActive(true);
	 }

	 public  void Quitter_deux()
	 {
		SceneManager.LoadScene("ex00");
	 }
}
