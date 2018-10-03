using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour {

	public Vector2 speed;
	public float lifetime;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.Translate(speed * Time.fixedDeltaTime);
		lifetime -= Time.fixedDeltaTime;
		if (lifetime <= 0)
			GameObject.Destroy(gameObject);
	}
}
