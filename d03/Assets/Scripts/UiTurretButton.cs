using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UiTurretButton : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	// private Image image;
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
		Debug.Log("OnDrag");
		Vector3 pos = Camera.main.ScreenToWorldPoint(eventData.position);
		pos.z = copy.transform.position.z;
		copy.transform.position = pos;
		// transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
	}

	 public void OnEndDrag(PointerEventData eventData)
	{
		Debug.Log("OnEndDrag");
		// copy.SetActive(tr);
	}

}
