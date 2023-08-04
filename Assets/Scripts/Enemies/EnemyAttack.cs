using System;
using UnityEngine;

namespace ICO321 {
	public class EnemyAttack : MonoBehaviour {
		protected PlayerHealth player;
		private EnemyHealth enemyHealth;
		protected int playerLayer;

		private void Awake() {
			playerLayer = LayerMask.NameToLayer("Player");
			enemyHealth = GetComponent<EnemyHealth>();
		}

		private void OnDisable() {
			player.OnPlayerDeath += OnPlayerDeath;
		}

		private void OnEnable() {
			player = FindObjectOfType<PlayerHealth>();
			player.OnPlayerDeath += OnPlayerDeath;
		}

		private void Start() {
			enemyHealth = GetComponent<EnemyHealth>();
		}

		private void OnPlayerDeath() {
			enemyHealth.Kill();
		}
	}
}