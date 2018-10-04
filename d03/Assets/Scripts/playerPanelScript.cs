using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class playerPanelScript : MonoBehaviour {

	// Use this for initialization
	public Text hpText;
	public Text energyText;
	public gameManager gameManager;
	void Start () {
		refresh();
	}
	
	// Update is called once per frame
	void Update () {
		refresh();
	}

	void refresh()
	{
		hpText.text = gameManager.playerHp.ToString();
		energyText.text = gameManager.playerEnergy.ToString();
	}
}
