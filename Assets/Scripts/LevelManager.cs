using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ICO321 {
	public class LevelManager : MonoBehaviour {
		public static LevelManager Instance;
		private ScenesManager scenesManager;

		public enum LevelState {
			PreLevel = 0,
			InLevel = 1,
			WinLevel = 2,
			LoseLevel = 3
		}

		public float speedMultiplier = 1;
		public LevelState levelState = LevelState.PreLevel;
		public int enemiesKilled;
		private GameObject player;

		public event Action<LevelState> OnLevelStateUpdated;
		public event Action<float> OnSpeedUpdated;
		public event Action<string, float> ShowMessage;
		public event Action<bool> Fade;

		private void Awake() {
			if (Instance != null) {
				Destroy(this);
			}
			else {
				Instance = this;
			}
			player = GameObject.FindGameObjectWithTag("Player");
			player.GetComponent<PlayerHealth>().OnPlayerDeath += OnPlayerDeath;
			scenesManager = FindObjectOfType<ScenesManager>();
			levelState = LevelState.InLevel;
			OnLevelStateUpdated?.Invoke(levelState);
		}

		private List<EnemyHealth> enemies;

		private void Start() {
			enemies = FindObjectsOfType<EnemyHealth>().ToList();
			foreach (var e in enemies) {
				e.OnDeath += OnEnemyKilled;
			}
			Fade?.Invoke(true);
			ShowMessage?.Invoke("Prepare Your Anus", 3);
		}

		private void OnEnemyKilled() {
			enemiesKilled++;
		}

		private void OnPlayerDeath() {
			//fare cose per il gameover
			levelState = LevelState.LoseLevel;
			OnLevelStateUpdated?.Invoke(levelState);

			StartCoroutine(GameOverSequence());
		}

		public void VictoryReached() {
			if (levelState == LevelState.InLevel) {
				levelState = LevelState.WinLevel;
				OnLevelStateUpdated?.Invoke(levelState);
				StartCoroutine(VictorySequence());
			}
		}

		private IEnumerator GameOverSequence() {
			ShowMessage("All your anus are belong to us", 4);
			float timer = 0;
			while (timer < 1) {
				OnSpeedUpdated?.Invoke(1 - timer);
				timer += Time.deltaTime;
				yield return null;
			}
			OnSpeedUpdated?.Invoke(0);
			yield return new WaitForSeconds(2);
			Fade(false);
			scenesManager.ReturnToMainMenu();
		}

		private IEnumerator VictorySequence() {
			ShowMessage("Space: The Anal Frontier", 4);
			float timer = 0;
			while (timer < 1) {
				OnSpeedUpdated?.Invoke(1 - timer);
				timer += Time.deltaTime;
				yield return null;
			}
			OnSpeedUpdated?.Invoke(0);
			yield return new WaitForSeconds(2);
			Fade(false);
			scenesManager.LoadNextLevel();
		}
	}
}