using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract	 class Attackable : MonoBehaviour {
	public float life;
	public AudioClip onDeath;

	public bool takeDamage(float damage) {
		life -= damage;
		if (life <= 0) {
			die ();
			return (true);
		}
		return (false);
	}

	public abstract void die();
}
