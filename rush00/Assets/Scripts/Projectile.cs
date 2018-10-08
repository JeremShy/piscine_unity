using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	[Range(0.1f, 20f)]
	public float range;
	[Range(0.1f, 20f)]
	public float speed;

	public string targetTag;

	void Start() {
		Destroy(gameObject, range / speed);
	}

	void Update () {
		// Goes forward
		transform.Translate(Vector2.right * Time.deltaTime * speed);

		// handle range
	}

	void OnTriggerEnter2D(Collider2D collision) {

		if (sharedTags.Contains<string>(collision.tag)) {
			Destroy(gameObject);
		}
	}

	private string[] sharedTags = { "wall", "door" };
}
