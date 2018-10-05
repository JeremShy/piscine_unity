using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class playerPanelScript : MonoBehaviour {

	// Use this for initialization
	public Text hpText;
	public Text energyText;
	void Start () {
		if (SceneManager.GetActiveScene().name == "lvl2")
		{
			gameManager.gm.playerStartEnergy = Mathf.CeilToInt(gameManager.gm.playerStartEnergy / 2);
			gameManager.gm.playerMaxHp = Mathf.CeilToInt(gameManager.gm.playerMaxHp * 3 / 4);
		}
		refresh();
	}
	
	// Update is called once per frame
	void Update () {
		refresh();
	}

	void refresh()
	{
		hpText.text = gameManager.gm.playerHp.ToString();
		energyText.text = gameManager.gm.playerEnergy.ToString();
	}
}
