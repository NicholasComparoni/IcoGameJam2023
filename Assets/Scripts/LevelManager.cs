using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ICO321 {
	public class LevelManager : MonoBehaviour {
		public static LevelManager Instance;

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

		private void Awake() {
			if (Instance != null) {
				Destroy(this);
			}
			else {
				Instance = this;
			}
			player = GameObject.FindGameObjectWithTag("Player");
			player.GetComponent<PlayerHealth>().OnPlayerDeath += OnPlayerDeath;
		}

		private List<EnemyHealth> enemies;

		private void Start() {
			enemies = FindObjectsOfType<EnemyHealth>().ToList();
			foreach (var e in enemies) {
				e.OnDeath += OnEnemyKilled;
			}
		}

		private void OnEnemyKilled() {
			enemiesKilled++;
		}

		private void OnPlayerDeath() {
			//fare cose per il gameover
			OnLevelStateUpdated?.Invoke(LevelState.LoseLevel);
		}
	}
}