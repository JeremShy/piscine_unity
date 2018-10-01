using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Club : MonoBehaviour {

	public Ball ball;
	public GameObject hole;
	const float speed = -1.5f;
	private float timePressed;
	bool descending = false;
	bool waiting = false;
	bool ended = false;
	// Use this for initialization
	void Start () {
	}

	float abs(float f)
	{
		return (f < 0 ? -1 * f : f);
	}

	int	find_direction() {
		if (ball.transform.position.y < hole.transform.position.y)
			return (-1);
		else
			return (1);
	}

	// Update is called once per frame
	void Update () {
		if (ended)
			return ;
		if (abs(ball.getSpeed()) < 1)
		{
			if (abs(ball.transform.position.y - hole.transform.position.y) < .3)
			{
				GameObject.Destroy(ball.gameObject);
				ended = true;
			}
		}
		if (!ball.isStopped())
			return ;
		if (ball.getSpeed() == 0 && waiting)
		{
			waiting = false;
			this.transform.position = ball.transform.position + new Vector3(0, .7f, 0) * find_direction();
			return ;
		}

		if (Input.GetKey(KeyCode.Space))
		{
			descending = true;
			this.transform.Translate(0, speed * Time.deltaTime * find_direction() * -1, 0);
			timePressed += Time.deltaTime;
		}
		else if (descending)
		{
			hit();
			descending = false;
			timePressed = 0;
			waiting = true;
		}
	}

	void hit()
	{
		ball.setSpeed(timePressed * 10 * find_direction() * -1);
	}
}
