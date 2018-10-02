using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {
	public enum Direction {Left, Right};

	public Vector2 leftSpeed;

	private Rigidbody2D rb2d;
	
	public float moveTime; // Temps avant lequel la plateforme part dans l'autre sens
	public Direction direction;
	
	private float time = 0;
	

	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float	lastTime;
		float	computedTime;

		lastTime = time;
		time = Mathf.Clamp(time + Time.fixedDeltaTime, 0, moveTime);
		computedTime = time - lastTime;
		// transform.Translate(leftSpeed * computedTime * (direction == Direction.Right ? 1 : -1));
		rb2d.MovePosition(rb2d.position + leftSpeed * computedTime * (direction == Direction.Right ? 1 : -1));
		if (time == moveTime)
		{
			time = 0;
			direction = (direction == Direction.Left ? Direction.Right : Direction.Left);
		}
	}
}
