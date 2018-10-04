using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour {

	private float speed = 1;

	public Text speedText;
	public gameManager gameManager;

	// Use this for initialization
	void Start () {
		ChangeSpeed(1);
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void PressPause ()
	{
		ChangeSpeed(0);
	}
	public void Press1x ()
	{
		ChangeSpeed(1);
	}
	public void Press2x ()
	{
		ChangeSpeed(2);
	}
	public void Press4x ()
	{
		ChangeSpeed(4);
	}
	
	private void ChangeSpeed(float f)
	{
		speed = f;
		speedText.text = speedToStr();
		gameManager.changeSpeed(speed);
	}


	private string speedToStr()
	{
		if (speed == 0)
			return "Paused";
		else if (speed == 1)
			return "Speed : 1X";
		else if (speed == 2)
			return "Speed : 2X";
		else
			return "Speed : 4X";
	}
}
