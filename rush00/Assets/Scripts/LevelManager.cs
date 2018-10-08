using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	public GameObject gameOverPanel;
	public GameObject nextLevelPanel;

	public AudioSource audioSource;
	public AudioSource musicSource;

	public AudioClip youWin;
	public AudioClip youLose;

	public List<AudioClip> musics;

	public static LevelManager instance = null;

	private List<Ennemy> ennemies;

	private void Awake() {
		if (!instance) {
			instance = this;
		}
	}

	private void OnEnable() {
		Ennemy.onEnnemyKilled += OnEnnemyDie;
	}

	private void OnDisable() {
		Ennemy.onEnnemyKilled -= OnEnnemyDie;
	}

	void Start() {
		ennemies = new List<Ennemy>();
		audioSource = GetComponent<AudioSource>();
		ennemies.AddRange(FindObjectsOfType<Ennemy>());


		musicSource.clip = musics[Random.Range(0, musics.Count)];
		musicSource.Play();
	}

	public void OnPlayerDie() {
		gameOverPanel.SetActive(true);
		PlayClip(youLose);
		StopGame();
	}

	public void OnEnnemyDie(Ennemy ennemy) {
		ennemies.Remove(ennemy);
		Debug.Log(ennemies.Count);
		if (ennemies.Count == 0 && !Player.player.gameOver) {
			nextLevelPanel.SetActive(true);
			StopGame();
			PlayClip(youWin);
		}
		Debug.Log(ennemies.Count);
	}

	private void StopGame() {
		Player.player.gameOver = true;
		foreach (Ennemy ennemy in ennemies) {
			if (ennemy) {
				ennemy.gameOver = true;
			}
		}
		ennemies.Clear();
	}

	public void ReloadLevel() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void NextLevel() {
		if (SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 1) {
			Debug.Log("End of the game");
		} else {
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		}
	}

	public void GoToMainMenu() {
		SceneManager.LoadScene(0);
	}

	private void PlayClip(AudioClip clip) {
		audioSource.clip = clip;
		audioSource.Play();
	}
}
