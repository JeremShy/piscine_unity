using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerScript : Unit {

	// Use this for initialization
	public override void Start () {
		base.Start ();
	}

	void OnMouseDown() {
		if (Input.GetKey (KeyCode.LeftControl) || Input.GetKey (KeyCode.RightControl)) {
			UnitController.instance.addPlayer (this);
		}
		else
			UnitController.instance.playerClicked (this);
	}
		
	string directionToString(Direction direction) {
		if (direction == Direction.haut) {
			return ("haut");
		}
		if (direction == Direction.droite) {
			return ("droite");
		}
		if (direction == Direction.bas) {
			return ("bas");
		}
		if (direction == Direction.gauche) {
			return ("gauche");
		}
		if (direction == Direction.hautDroite) {
			return ("hautDroite");
		}
		if (direction == Direction.basDroite) {
			return ("basDroite");
		}
		if (direction == Direction.basGauche) {
			return ("basGauche");
		}
		if (direction == Direction.hautGauche) {
			return ("hautGauche");
		}
		return ("WTF");
	}

	public override void die() {
		if (onDeath)
			AudioManager.instance.Play (onDeath);
		Object.DestroyImmediate (this.gameObject);
	}
}

//Droite		: positif	- 0
//haut droite	: positif	- positif
//Haut			: 0			- positif
//Haut gauche	: negatif	- positif
//Gauche		: negatif	- 0
//Bas Gauche	: negatif	- negatif
//Bas			: 0			- negatif
//Bas Droite	: Positif	- negatif

// Haut			: 1
// Droite		: 2
// Bas			: 3
// Gauche		: 4
// Haut droite	: 5
// Bas droite	: 6
// Bas gauche	: 7
// Haut gauche	: 8