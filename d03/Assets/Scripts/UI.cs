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

	public void canonBarClick()
	{
		Debug.Log("Clicked on the canon.");
	}
	public void gatlingBarClick()
	{
		Debug.Log("Clicked on the gatling.");
	}
	public void rocketBarClick()
	{
		Debug.Log("Clicked on the rocket.");
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
