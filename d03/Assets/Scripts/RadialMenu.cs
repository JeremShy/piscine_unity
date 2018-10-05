using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialMenu : MonoBehaviour {

	public Button upgradeButton;
	public Button downGradeButton;
	public Text upgradePrice;
	public Text downgradePrice;
	public Text sellPrice;

	[HideInInspector]
	public towerScript boundTower;

	[HideInInspector]
	public clickableMenu boundMenu;

	private bool upped = false;

	// Use this for initialization
	void Start () {
		updateInfos();
	}

	public void cancelButtonFunc()
	{
		Debug.Log("Cancel button clicked !");
		upped = false;
		GameObject.Destroy(gameObject);
	}

	private int get_sell_count()
	{
		int c = 0;

		if (!boundTower)
		{
			Debug.LogError("Error ! 34");
			return (0);
		}
		towerScript tower = boundTower;
		GameObject go;
		while (tower != null)
		{
			c += tower.energy;
			go = tower.downgrade;
			if (!go)
				return (c / 2);
			tower = go.GetComponent<towerScript>();
		}
		return (c / 2);
	}

	public void upgradeButtonFunc()
	{
		if (boundTower.upgrade == null)
		{
			Debug.Log("This shouldn't happen ! 43");
			return ;
		}
		int energy = boundTower.upgrade.GetComponent<towerScript>().energy;
		if (gameManager.gm.playerEnergy < energy)
			return ;
		gameManager.gm.playerEnergy -= energy;
		towerScript next = GameObject.Instantiate(boundTower.upgrade, boundTower.transform.position, boundTower.transform.rotation, boundTower.transform.parent).GetComponent<towerScript>();
		GameObject.Destroy(boundTower.gameObject);
		boundTower = next;
		boundMenu.boundTower = boundTower;
		updateInfos();
	}

	public void downgradeButtonFunc()
	{
		if (boundTower.downgrade == null)
		{
			Debug.Log("This shouldn't happen ! 50");
			return ;
		}
		int energy = (boundTower.GetComponent<towerScript>().energy / 2);
		gameManager.gm.playerEnergy += energy;
		towerScript next = GameObject.Instantiate(boundTower.downgrade, boundTower.transform.position, boundTower.transform.rotation, boundTower.transform.parent).GetComponent<towerScript>();
		GameObject.Destroy(boundTower.gameObject);
		boundTower = next;
		boundMenu.boundTower = boundTower;
		updateInfos();
	}

	public void sellButtonFunc()
	{
		int energy = get_sell_count();
		gameManager.gm.playerEnergy += energy;
		GameObject.Destroy(boundTower.gameObject);
		GameObject.Destroy(boundMenu.gameObject);
		GameObject.Destroy(gameObject);
	}

	void updateInfos()
	{
		if (boundTower.upgrade != null)
		{
			upgradeButton.gameObject.SetActive(true);
			upgradePrice.gameObject.SetActive(true);
			upgradePrice.text = boundTower.upgrade.GetComponent<towerScript>().energy.ToString();
		}
		else
		{
			upgradeButton.gameObject.SetActive(false);
			upgradePrice.gameObject.SetActive(false);
		}
		if (boundTower.downgrade != null)
		{
			downGradeButton.gameObject.SetActive(true);
			downgradePrice.gameObject.SetActive(true);
			downgradePrice.text = (boundTower.GetComponent<towerScript>().energy / 2).ToString();
		}
		else
		{
			downGradeButton.gameObject.SetActive(false);
			downgradePrice.gameObject.SetActive(false);
		}
		sellPrice.text = get_sell_count().ToString();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonUp(1))
			upped = true;
		if (upped && Input.GetMouseButtonDown(1))
		{
			GameObject.Destroy(gameObject);
		}
	}
}