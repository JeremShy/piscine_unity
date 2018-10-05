using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UiTurretButton : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	// private Image image;
	public	towerScript towerPrefab;
	public clickableMenu clickableMenuPrefab;
	private GameObject copy;

	public Canvas mainCanvas;

	public GameObject flyIcon;
	public GameObject noFlyIcon;
	public Text energyText;
	public Text damageText;
	public Text rangeText;
	public Text waitText;

	private bool dragging = false;
	private Image img;

	// Use this for initialization
	void Start () {
		energyText.text = towerPrefab.energy.ToString();
		damageText.text = towerPrefab.damage.ToString();
		rangeText.text = towerPrefab.range.ToString();
		waitText.text = towerPrefab.fireRate.ToString();
		if (towerPrefab.type == towerScript.Type.canon) // couldn't find it in the prefabs ?
		{
			noFlyIcon.SetActive(true);
			flyIcon.SetActive(false);
		}
		else
		{
			noFlyIcon.SetActive(false);
			flyIcon.SetActive(true);
		}
		img = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
		if (gameManager.gm.playerEnergy < towerPrefab.energy && (img.color != Color.red))
			img.color = Color.red;
		else if (gameManager.gm.playerEnergy >= towerPrefab.energy && (img.color == Color.red))
			img.color = Color.white;
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		if (gameManager.gm.playerEnergy < towerPrefab.energy)
			dragging = false;
		else
		{
			dragging = true;
			copy = GameObject.Instantiate(this.gameObject, transform.position, transform.rotation, transform.parent);
			copy.SetActive(true);
		}
	}


	public void OnDrag(PointerEventData eventData)
	{
		if (dragging == true)
		{
			Vector3 pos = Camera.main.ScreenToWorldPoint(eventData.position);
			pos.z = copy.transform.position.z;
			copy.transform.position = pos;
		}
	}

	 public void OnEndDrag(PointerEventData eventData)
	{
		if (dragging == true)
		{
			dragging = false;
			Vector3 pos = Camera.main.ScreenToWorldPoint(eventData.position);
			pos.z = 10;

			RaycastHit2D hit = Physics2D.Raycast(pos , new Vector3(0, 0, -1));
			if (hit.collider != null)
			{
				if (hit.collider.tag == "empty")
				{
					gameManager.gm.playerEnergy -= towerPrefab.energy;
					towerScript ts = GameObject.Instantiate(towerPrefab, hit.collider.transform.position, hit.collider.transform.rotation, hit.collider.transform.parent);
					clickableMenu clickableMenu = GameObject.Instantiate(clickableMenuPrefab, ts.transform.position, ts.transform.rotation, ts.transform.parent);
					clickableMenu.mainCanvas = transform.parent.parent.parent.GetComponent<Canvas>();
					clickableMenu.boundTower = ts;
				}
			}
			GameObject.Destroy(copy);
		}
	}

}
