using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : Attackable {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void die() {
		if (onDeath)
			AudioManager.instance.Play (onDeath);
		Object.DestroyImmediate (this.gameObject);
		TownHall[] hall = FindObjectsOfType<TownHall>();
		foreach (TownHall i in hall) {
			if (i.isEnnemy == true) {
				i.SendMessage ("augmentSpawnTime");
			}
		}
	}
}
