using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour {
	
	static float	abs(float val)
	{
		if (val < 0f)
			return (val * -1f);
		else
			return (val);
	}

	float speed;
	public GameObject BottomLine;
	public GameObject disappearLine;
	public KeyCode goodKey;
	void Start () {
		speed = Random.Range(6, 10) * -1;
	}
	
	void Update () {
		this.transform.localPosition += new Vector3(0, this.speed * Time.deltaTime, 0);
		if (this.transform.position.y <= disappearLine.transform.position.y)
		{
			GameObject.Destroy(this.gameObject);
			Debug.Log("Error ! You did not press the key.");
		}
		if (Input.GetKeyDown(this.goodKey))
		{
			float precision;

			precision = Cube.abs(this.transform.position.y - BottomLine.transform.position.y) * 100;
			Debug.Log("Precision: " + precision);
			GameObject.Destroy(this.gameObject);
		}
	}
}
