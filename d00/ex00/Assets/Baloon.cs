using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baloon : MonoBehaviour
{

	private double _souffle = 50;
	private Vector3 _explode;
	private System.DateTime _begin;

	// Use this for initialization
	void Start ()
	{
		_explode = transform.localScale + transform.localScale;
		_begin = System.DateTime.Now;
	}

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Space)) {
			if (_souffle > 0) {
				this.transform.localScale += new Vector3 (0.2f, 0.2f, 0.2f);
				_souffle -= 5;
				Vector3 Vect = this.transform.localScale;
				if (Vect.x >= _explode.x) {
					endOfGame();
				}
			}
		} else {
			Vector3 Vect = this.transform.localScale - new Vector3 (0.015f, 0.015f, 0.015f);
			_souffle += 0.2f;
			if (Vect.x > 0 && Vect.y > 0 && Vect.z > 0) {
				this.transform.localScale = Vect;
			}
			else
				endOfGame();
		}
	}

	void endOfGame() {
		System.DateTime end = System.DateTime.Now;
		System.TimeSpan s = 	end - _begin;
		int ttp = Mathf.RoundToInt ((float)(s.TotalMilliseconds) / 1000f);
		Debug.Log ("Balloon life time: " + ttp + "s");
		GameObject.Destroy (this.gameObject);
	}
}
