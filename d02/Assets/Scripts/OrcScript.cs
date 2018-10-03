using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcScript : Unit {

	// Use this for initialization
	public override void Start () {
		base.Start ();
	}

	// Update is called once per frame
	void Update () {
		
	}
	public override void die() {
		Object.DestroyImmediate (this.gameObject);
		if (onDeath)
			AudioManager.instance.Play (onDeath);
	}
}
