using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CameraScript_ex02 : MonoBehaviour {

	public playerScript_ex02 red;
	public playerScript_ex02 blue;
	public playerScript_ex02 yellow;

	[HideInInspector]
	public bool stopFollowing;
	private float time;

	public string nextLevel;

	// Use this for initialization
	void Start () {
		stopFollowing = false;
	}
	

	// Update is called once per frame
	void Update () {
		if (stopFollowing)
		{
			time += Time.deltaTime;
			if (time >= 3)
				Reset();
		}
		else
		{
			if (Input.GetKey(KeyCode.R))
			{
				Reset();
			}
			handleSelection();

			if (!stopFollowing)
			{
				playerScript_ex02 selected;
				if (red.selected)
					selected = red;
				else if (blue.selected)
					selected = blue;
				else
					selected = yellow;
				transform.position = new Vector3(selected.transform.position.x, selected.transform.position.y + .4f, transform.position.z);
			}
			if (red.wellPlaced && blue.wellPlaced && yellow.wellPlaced)
			{
				SceneManager.LoadScene(nextLevel, LoadSceneMode.Single);
			}
		}
	}

	public void Reset() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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

	void select(playerScript_ex02 p)
	{
		if (p.selected)
			return ;
		p.selected = true;
		p.rb2d.constraints &= ~(RigidbodyConstraints2D.FreezePositionX);
		// p.rb2d.mass /= 100;
	}

	void unselect(playerScript_ex02 p)
	{
		if (!p.selected)
			return ;
		p.selected = false;
		p.rb2d.constraints |= RigidbodyConstraints2D.FreezePositionX;
	}
}
