using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownHall : Building {

	public Unit unit;
	public Vector2 spawnOffset;
	private float _spawnTime = 10;
	public bool isEnnemy;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("spawnUnit", 0, _spawnTime);
	}
	
	// Update is called once per frame
	void Update () {
	}

	void spawnUnit() {
		Instantiate (unit, transform.position + (Vector3)spawnOffset, transform.rotation);
	}

	void augmentSpawnTime() {
		_spawnTime += 2.5f;
		CancelInvoke ();
		InvokeRepeating ("spawnUnit", 0, _spawnTime);
		Debug.Log ("augmenting spawn time");
	}

	public override void die() {
		if (onDeath)
			AudioManager.instance.Play (onDeath);
		Object.DestroyImmediate (this.gameObject);
		if (isEnnemy) {
			Debug.Log ("Player won !");
		}
	}
}
