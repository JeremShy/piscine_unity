using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButton : MonoBehaviour {
	public GameObject boundDoor;
	public GameObject boundDoor2 = null;

	// Use this for initialization
	void Start () {
		
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player" && other.isTrigger == false)
		{
			if (boundDoor2 == null)
			{
				if (boundDoor.activeSelf)
					boundDoor.SetActive(false);
				else
					boundDoor.SetActive(true);
			}
			else
			{
				if (boundDoor2.activeSelf)
				{
					boundDoor.SetActive(true);
					boundDoor2.SetActive(false);
				}
				else
				{
					boundDoor.SetActive(false);
					boundDoor2.SetActive(true);	
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
