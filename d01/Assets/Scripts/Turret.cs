using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

	public enum MissileColor{Red, Blue, Yellow, White};
	public GameObject missilePrefab;
	
	public float	timeBetweenShots;
	public float	startTime;
	public float	shootSpeed;
	public MissileColor missileColor;

	private float	time;
	private Vector2 normalOrientation = new Vector2(0, 1);
	private float rotationOffset = -90f;


	// Use this for initialization
	void Start () {
		time = startTime;
	}
	
	Vector2 rotate(float angle, Vector2 src)
	{
		return Quaternion.AngleAxis(angle, Vector3.forward) * src.normalized;
	}

	void shoot()
	{
		Vector2 missileVector = rotate(this.transform.rotation.eulerAngles.z, normalOrientation) * shootSpeed;
		float missileAngle = this.transform.rotation.eulerAngles.z + rotationOffset;
		Collider2D c = GetComponent<Collider2D>();

		GameObject obj = GameObject.Instantiate(missilePrefab);
		Missile missile = obj.GetComponent<Missile>();
		missile.transform.rotation = Quaternion.Euler(0, 0, missileAngle);
		missile.speed = missileVector;
		missile.lifetime = 5;
		missile.transform.position = transform.position;
		if (missileColor == MissileColor.Red)
		{
			obj.layer = LayerMask.NameToLayer("Red_obstacle");
			obj.GetComponent<SpriteRenderer>().color = Color.red;
		}
		else if (missileColor == MissileColor.Blue)
		{
			obj.layer = LayerMask.NameToLayer("Blue_obstacle");
			obj.GetComponent<SpriteRenderer>().color = Color.blue;
		}
		else if (missileColor == MissileColor.Yellow)
		{
			obj.layer = LayerMask.NameToLayer("Yellow_obstacle");
			obj.GetComponent<SpriteRenderer>().color = Color.yellow;
		}
		else
		{
			obj.layer = LayerMask.NameToLayer("Default");
			obj.GetComponent<SpriteRenderer>().color = Color.white;
		}
		// print (this.transform.rotation.eulerAngles.z);
		// print (missileAngle);
	}

	// Update is called once per frame
	void FixedUpdate () {
		time += Time.fixedDeltaTime;
		if (time >= timeBetweenShots)
		{
			shoot();
			time = 0;
		}
	}
}
