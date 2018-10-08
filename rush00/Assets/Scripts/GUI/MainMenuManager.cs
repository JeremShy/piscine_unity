using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuManager : MonoBehaviour {

	public Camera currentCamera;

	[Header("Background color transition")]
	public Color startColor;
	public Color endColor;
	[Range(1f, 5f)]
	public float transitionDuration;

	[Header("Cursor")]
	public Texture2D cursor;

	private int timeDirection = 1;
	private float elapsedTime = 0;

	void Start() {
		Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
	}

	void Update() {
		elapsedTime += (Time.deltaTime / transitionDuration) * timeDirection;
		currentCamera.backgroundColor = Color.Lerp(startColor, endColor, elapsedTime);

		if (Mathf.Abs(elapsedTime) >= transitionDuration) {
			timeDirection *= -1;
		}
	}

	public void StartGame() {
		SceneManager.LoadScene(1);
	}

	public void QuitButton() {
		Application.Quit();
	}
}
