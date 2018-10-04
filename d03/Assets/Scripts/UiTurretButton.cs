using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UiTurretButton : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	// private Image image;
	public	GameObject towerPrefab;
	private GameObject copy;

	// Use this for initialization
	void Start () {
		// image = GetComponent<Image>();
		// copy = GameObject.Instantiate(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		Debug.Log("OnBeginDrag");
		copy = GameObject.Instantiate(this.gameObject, transform.position, transform.rotation, transform.parent);

		copy.SetActive(true);
	}


	public void OnDrag(PointerEventData eventData)
	{
		Vector3 pos = Camera.main.ScreenToWorldPoint(eventData.position);
		pos.z = copy.transform.position.z;
		copy.transform.position = pos;
	}

	 public void OnEndDrag(PointerEventData eventData)
	{
		Debug.Log("OnEndDrag");
		// GameObject.Destroy(copy);
		Vector3 pos = Camera.main.ScreenToWorldPoint(eventData.position);
		pos.z = 10;

		RaycastHit2D hit = Physics2D.Raycast(pos , new Vector3(0, 0, -1));
		if (hit.collider == null)
		{
			Debug.Log("No collider.");
		}
		else
		{
			if (hit.collider.tag != "empty")
			{
				Debug.Log("Collision not empty");
			}
			else
			{
				Debug.Log("Collided an empty tile !");
				GameObject.Instantiate(towerPrefab, hit.collider.transform.position, hit.collider.transform.rotation, hit.collider.transform.parent);
			}
		}
		GameObject.Destroy(copy);
	}

}
