using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour {

	private int _vitesse;
	// Use this for initialization
	void Start () {
		_vitesse = Random.Range (8, 15);
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.A)) {
			if (this.transform.position.x < 0) {
				printPrecAndDestroy ();
			}
		}
		if (Input.GetKeyDown (KeyCode.S)) {
			if (this.transform.position.x == 0) {
				printPrecAndDestroy ();
			}
		}
		if (Input.GetKeyDown (KeyCode.D)) {
			if (this.transform.position.x > 0) {
				printPrecAndDestroy ();
			}
		}
		float yMove;
		yMove = Time.deltaTime * _vitesse * -1;
		Vector3 move = new Vector3(0, yMove, 0);
		transform.Translate (move);

		if (transform.position.y <= -4)
			GameObject.Destroy (this.gameObject);
	}

	float getAbs(float f) {
		if (f >= 0)
			return (f);
		else
			return (f * -1.0f);
	}

	void printPrecAndDestroy() {
		Debug.Log("Precision: " + getAbs(-4f - transform.position.y));
		GameObject.Destroy (this.gameObject);
	}
}

