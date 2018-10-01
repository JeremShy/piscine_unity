using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
	private float speed = 0f;
	private float decelerateSpeed = 2f;

	public void setSpeed(float f)
	{
		this.speed = f;
	}

	public float getSpeed()
	{
		return (speed);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float oldSpeed = speed;
		if (speed != 0)
		{
			this.transform.Translate(0, speed * Time.deltaTime, 0);
			speed = speed > 0 ? speed - decelerateSpeed * Time.deltaTime : speed + decelerateSpeed * Time.deltaTime;
			if ((speed < 0 && oldSpeed > 0) || (speed > 0 && oldSpeed < 0))
				speed = 0;
		}
	}
}