using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraScript_ex01 : MonoBehaviour {

	public playerScript_ex01 red;
	public playerScript_ex01 blue;
	public playerScript_ex01 yellow;


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.R))
		{
			red.reset();
			blue.reset();
			yellow.reset();
			select(red);
		}
		handleSelection();

		playerScript_ex01 selected;
		if (red.selected)
			selected = red;
		else if (blue.selected)
			selected = blue;
		else
			selected = yellow;
		transform.position = new Vector3(selected.transform.position.x, selected.transform.position.y + .4f, transform.position.z);
		if (red.wellPlaced && blue.wellPlaced && yellow.wellPlaced)
		{
			SceneManager.LoadScene("ex02", LoadSceneMode.Single);
		}
	}

	void handleSelection()
	{
		if (Input.GetKey(KeyCode.Alpha1))
		{
			select(red);
			unselect(blue);
			unselect(yellow);
		}
		else if (Input.GetKey(KeyCode.Alpha2))
		{
			unselect(red);
			select(blue);
			unselect(yellow);
		}
		else if (Input.GetKey(KeyCode.Alpha3))
		{
			unselect(red);
			unselect(blue);
			select(yellow);
		}
	}

	void select(playerScript_ex01 p)
	{
		if (p.selected)
			return ;
		p.selected = true;
		p.rb2d.constraints &= ~(RigidbodyConstraints2D.FreezePositionX);
		// p.rb2d.mass /= 100;
	}

	void unselect(playerScript_ex01 p)
	{
		if (!p.selected)
			return ;
		p.selected = false;
		p.rb2d.constraints |= RigidbodyConstraints2D.FreezePositionX;
	}
}
