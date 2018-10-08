using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGenerator : MonoBehaviour {

	public List<GameObject> weaponsPrefabs;

	void Start () {
		Instantiate(weaponsPrefabs[Random.Range(0, weaponsPrefabs.Count)], transform.position, Quaternion.identity);
		Destroy(gameObject);
	}
}
