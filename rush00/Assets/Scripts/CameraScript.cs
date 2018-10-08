using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

	[Range(1f, 20f)]
	public float transitionDuration;

	private Vector3 offset;
	new private Camera camera;
	private Color[] colors = { Color.red, Color.blue, Color.magenta, Color.cyan, Color.yellow, Color.green };
	private int currentColorIndex = 0;
	private int nextIndex = 1;

	private float time = 0f;

	void Start () {
		offset = Player.player.transform.position - transform.position;
		camera = GetComponent<Camera>();
		StartCoroutine(ChangeColorIndices());
	}

	void LateUpdate () {
		if (Player.player) {
			transform.position = Player.player.transform.position - offset;
		}
		camera.backgroundColor = Color.Lerp(colors[currentColorIndex], colors[nextIndex], Mathf.Clamp(time / transitionDuration, 0.01f, .99f));
		time += Time.deltaTime;
		if (time >= transitionDuration) {
			time = 0f;
		}
	}

	IEnumerator ChangeColorIndices() {
		while (true) {
			yield return new WaitForSeconds(transitionDuration);
			currentColorIndex++;
			nextIndex++;
			if (currentColorIndex == colors.Length) {
				currentColorIndex = 0;
			}
			if (nextIndex == colors.Length) {
				nextIndex = 0;
			}
		}
	}

}
