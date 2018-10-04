using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour {

	public void buttonStart()
	{
		SceneManager.LoadScene("ex01");
	}
	public void buttonStop()
	{
		Application.Quit();
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}
}
