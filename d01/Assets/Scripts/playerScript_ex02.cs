using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript_ex02 : MonoBehaviour {
	public float speed;
	public float jump_speed;
	public bool selected;
	public string exit_tag;
	public float	maxSpeed;

	public CameraScript_ex02 camera;

	private bool grounded = true;


	[HideInInspector]
	public Rigidbody2D	rb2d;
	
	[HideInInspector]
	public bool			wellPlaced = false;
	private Vector3 startPos;
	void Start () {
		rb2d = GetComponent<Rigidbody2D>();
		startPos = transform.position;
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
		if (other.tag == "teleporter")
		{
			Teleporter tp = other.GetComponent<Teleporter>();
			transform.position = tp.boundTp.transform.position;
		}
		if (other.tag == "missile")
		{
			die();
		}
		if (other.tag == "hole")
		{
			camera.stopFollowing = true;
		}
	}

	void die()
	{
		transform.position = startPos;
		rb2d.velocity = Vector2.zero;
	}

	private void OnTriggerExit2D(Collider2D other) {
		if (other.tag == exit_tag)
			wellPlaced = false;
	}

	void FixedUpdate () {
		if (!selected || camera.stopFollowing)
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
}
