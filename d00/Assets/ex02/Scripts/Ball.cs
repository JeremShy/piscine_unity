using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
	private float speed = 0f;
	private float decelerateSpeed = 2f;
	public float lowBorder;
	public float highBorder;
	private bool stopped = true;

	public bool isStopped()
	{
		return (stopped);
	}

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
		float oldSpeed;
		Vector3 nextPos;

		if (speed != 0)
		{
			if (stopped)
				stopped = false;
			nextPos = this.transform.position + new Vector3(0, speed * Time.deltaTime, 0);
			if (nextPos.y >= highBorder)
			{
				speed *= -1;
				nextPos = this.transform.position + new Vector3(0, speed * Time.deltaTime, 0);
			}
			else if (nextPos.y <= lowBorder)
			{
				speed *= -1;
				nextPos = this.transform.position + new Vector3(0, speed * Time.deltaTime, 0);
			}
			oldSpeed = speed;
			speed = speed > 0 ? speed - decelerateSpeed * Time.deltaTime : speed + decelerateSpeed * Time.deltaTime;
			if ((speed < 0 && oldSpeed > 0) || (speed > 0 && oldSpeed < 0))
			{
				stopped = true;
				speed = 0;
			}
			this.transform.position = nextPos;
		}
	}
}