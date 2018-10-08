using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour {


	[Header("GUI Texts")]
	public Text weaponNameText;
	public Text ammoText;

	private Weapon currentWeapon;

	// Use this for initialization
	void Start () {
		
	}

	void OnEnable() {
		Player.onGrabWeapon += UpdateWeapon;
	}

	void OnDisable() {
		Player.onGrabWeapon -= UpdateWeapon;
	}

	// Update is called once per frame
	void Update () {
	
		if (currentWeapon) {
			ammoText.text = currentWeapon.unlimitedAmmo ? "∞" : currentWeapon.ammo.ToString();
		} else {
			ammoText.text = " - ";
		}

	}

	void UpdateWeapon(Weapon w) {
		currentWeapon = w;
		weaponNameText.text = w ? w.name : "No Weapon";
	}
}
