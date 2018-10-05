using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour {
	public GameObject CubePrefab1;
	public GameObject CubePrefab2;
	public GameObject CubePrefab3;
	public GameObject[] tabCube;

	public float[] tabX;

	// Use this for initialization
	void Start () {
		Debug.Log ("Cube spawner initiated.\n");
		tabCube = new GameObject[3];
		tabCube [0] = CubePrefab1;
		tabCube [1] = CubePrefab2;
		tabCube [2] = CubePrefab3;

		tabX = new float[3];
		tabX [0] = -3.3f;
		tabX [1] = 0f;
		tabX [2] = 3.3f;

		Invoke ("spawnCube", 0.5f);
	}

	// Update is called once per frame
	void Update () {

	}

	void spawnCube() {
		float randomTime = Random.Range (0.85f, 1f);

		int type = Random.Range (0, 3);
		Instantiate (tabCube[type], new Vector3 (tabX [type], 4, -1), transform.rotation);

		Invoke ("spawnCube", randomTime);
	}
}
