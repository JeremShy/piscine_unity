using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public GameObject A;
	public GameObject S;
	public GameObject D;

	const float timeBetweenSpawn = 1.8f;
	float timeUntilNextSpawn;
	// Use this for initialization
	void Start () {
		timeUntilNextSpawn = 1;
	}
	
	// Update is called once per frame
	void Update () {
		timeUntilNextSpawn -= Time.deltaTime;
		if (timeUntilNextSpawn <= 0)
		{
			timeUntilNextSpawn = timeBetweenSpawn;
			spawn();
		}
	}
	void spawn(){
		int rand;
		GameObject go;

		rand = Random.Range(0, 3);
		if (rand == 0)
			go = this.A;
		else if (rand == 1)
			go = this.S;
		else
			go = this.D;
		GameObject.Instantiate(go);
	}
}
