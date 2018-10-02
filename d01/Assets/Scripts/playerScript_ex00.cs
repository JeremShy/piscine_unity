using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript_ex00 : MonoBehaviour {
	public float speed;
	public float jump_speed;
	public bool selected;
	public string exit_tag;
	public float	maxSpeed;

	private bool grounded = true;
	private Vector3 startpos;


	[HideInInspector]
	public Rigidbody2D	rb2d;
	
	[HideInInspector]
	public bool			wellPlaced = false;
	void Start () {
		startpos = transform.position;
		rb2d = GetComponent<Rigidbody2D>();
	}
	void OnTriggerStay2D(Collider2D other)
	{
		if ((other.tag == "wall" || other.tag == "Player") && !other.isTrigger)
			grounded = true;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == exit_tag)
			wellPlaced = true;
	}

	private void OnTriggerExit2D(Collider2D other) {
		// if ((other.tag == "wall" || other.tag == "player"))
		// 	grounded = false;
		if (other.tag == exit_tag)
			wellPlaced = false;
	}

	void FixedUpdate () {
		if (!selected)
			return ;
		lateralUpdate();
		verticalUpdate();
	}

	void lateralUpdate() {
		float move = Input.GetAxisRaw("Horizontal");
		if (move == 0)
			return ;
		Vector2 movement = new Vector2(move * speed, 0);
		if (Mathf.Abs(rb2d.velocity.x) > maxSpeed)
			return ;
		rb2d.AddForce(movement);
	}

	void verticalUpdate()
	{
		if (!grounded)
			return ;
		if (Input.GetKey(KeyCode.Space))
		{
			rb2d.AddForce(new Vector2(0, jump_speed));
			grounded = false;
		}
	}

	public void reset() {
		Debug.Log("Reseting");
		transform.position = startpos;
		rb2d.velocity = Vector2.zero;
	}
}
