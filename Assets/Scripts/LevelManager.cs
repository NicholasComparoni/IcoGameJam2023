using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;

namespace ICO321 {
	public class LevelManager : MonoBehaviour {
		[SerializeField] private LevelSettings levelSettings;
		[SerializeField] private string startLevelMessage;
		[SerializeField] private string gameOverMessage;
		[SerializeField] private string victoryMessage;
		public static LevelManager Instance;
		[SerializeField] private AudioClip gameOverClip;
		[SerializeField] private AudioClip victoryClip;
		[SerializeField] private Volume gameStartOverVolume;
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

		private void OnValidate() {
			if (levelSettings != null) { }
		}

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
			float weight = 1;
			DOTween.To(() => weight, x => weight = x, 0, 3)
				.OnUpdate(() => { gameStartOverVolume.weight = weight; });

			OnLevelStateUpdated?.Invoke(levelState);
		}

		private List<EnemyHealth> enemies;

		private void Start() {
			enemies = FindObjectsOfType<EnemyHealth>().ToList();
			foreach (var e in enemies) {
				e.OnDeath += OnEnemyKilled;
			}
			Fade?.Invoke(true);
			ShowMessage?.Invoke(startLevelMessage, 2);
			if (levelSettings != null) {
				MusicManager.Instance.PlayTrack(levelSettings.trackNumber);
				var horizontalScrollers = FindObjectsOfType<HorizontalScroller>();
				foreach (var hs in horizontalScrollers) {
					hs.Setup(levelSettings);
				}
			}
		}

		private void Update() {
			if (Input.GetButtonDown("Cancel")) {
				Fade(false);
				scenesManager.ReturnToMainMenu();
			}
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
			float weight = 0;
			DOTween.To(() => weight, x => weight = x, 1, 3)
				.OnUpdate(() => { gameStartOverVolume.weight = weight; });
			SfxManager.Instance.PlayClip(gameOverClip);
			ShowMessage(gameOverMessage, 4);
			float timer = 0;
			while (timer < 1) {
				OnSpeedUpdated?.Invoke(1 - timer);
				timer += Time.deltaTime;
				yield return null;
			}
			OnSpeedUpdated?.Invoke(0);
			yield return new WaitForSeconds(2);
			Fade(false);
			scenesManager.ReloadLevel();
		}

		private IEnumerator VictorySequence() {
			SfxManager.Instance.PlayClip(victoryClip);
			ShowMessage(victoryMessage, 4);
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