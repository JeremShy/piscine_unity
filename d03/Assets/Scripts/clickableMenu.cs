using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clickableMenu : MonoBehaviour {

	[HideInInspector]
	public towerScript boundTower;
	public RadialMenu radialMenuPrefab;
	public Canvas mainCanvas;

	private GameObject towerTile;

	CircleCollider2D col;

	private void OnMouseOver() {
		if (Input.GetMouseButtonDown(1))
		{
			print("Clicked !");
			RadialMenu menu = GameObject.Instantiate(radialMenuPrefab, transform.position, transform.rotation, mainCanvas.transform);
			RectTransform menuRect = menu.GetComponent<RectTransform>();
			menuRect.position = transform.position;
			menu.boundTower = boundTower;
			menu.boundMenu = this;
		}
	}

	// Use this for initialization
	void Start () {
		col = GetComponent<CircleCollider2D>();

		towerTile = null;
		foreach (Transform child in boundTower.transform)
		{
			if (child.tag == "tower")
			{
				towerTile = child.gameObject;
				break;
			}
		}
		if (towerTile == null)
		{
			Debug.LogError("Couldn't find towerTile");
		}
		transform.position = towerTile.transform.position;
		transform.position += new Vector3(0, 0, -1);
		CircleCollider2D towerCol = towerTile.gameObject.GetComponent<CircleCollider2D>();
		col.offset = towerCol.offset;
		col.radius = towerCol.radius;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
