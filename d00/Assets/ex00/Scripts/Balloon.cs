using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour {

	static Vector3 speedUp = new Vector3(.4f, .4f, 0);
	static Vector3 speedDown = new Vector3(1.5f, 1.5f, 0);

	bool alive;

	float lifeTime;
	float souffle = 10;
	const float vitesseSoufleUp = 1f;

	// Use this for initialization
	void Start () {
		this.lifeTime = 0;
		alive = true;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector3 nextScale;

		if (!alive)
			return ;
		if (Input.GetKeyDown(KeyCode.Space) && souffle > 0)
		{
			souffle -= 1;
			nextScale = transform.localScale + Balloon.speedUp;
		}
		else
		{
			nextScale = transform.localScale - Balloon.speedDown * Time.deltaTime;
			souffle += vitesseSoufleUp * Time.deltaTime;
		}
		if (checkAlive())
		{
			this.lifeTime += Time.deltaTime;
			transform.localScale = nextScale;
		}
		else
		{
			Debug.Log("Balloon life time: " + Mathf.RoundToInt(lifeTime) + "s");
			this.alive = false;
			if (transform.localScale.x >= 3)
				GameObject.Destroy(gameObject);
		}
	}
	bool checkAlive()
	{
		if (transform.localScale.x <= .1f || transform.localScale.x >= 3)
			return (false);
		return (true);
	}
}
