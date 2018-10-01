using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Club : MonoBehaviour {

	public Ball ball;
	const float speed = -1.5f;

	private float timePressed;
	
	bool descending = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.Space))
		{
			descending = true;
			this.transform.Translate(0, speed * Time.deltaTime, 0);
			timePressed += Time.deltaTime;
		}
		else if (descending)
		{
			hit();
			descending = false;
			timePressed = 0;
		}
	}

	void hit()
	{
		// TODO: Hit depending of how long the space bar was pressed.
	}
}
