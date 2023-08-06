using UnityEngine;

namespace ICO321 {
	public class EnemyAttack : MonoBehaviour {
		protected PlayerHealth player;
		private EnemyHealth enemyHealth;
		protected int playerLayer;

		protected void Awake() {
			playerLayer = LayerMask.NameToLayer("Player");
			enemyHealth = GetComponent<EnemyHealth>();
		}

		protected  void OnDisable() {
			player.OnPlayerDeath -= OnPlayerDeath;
		}

		protected  void OnEnable() {
			player = FindObjectOfType<PlayerHealth>();
			player.OnPlayerDeath += OnPlayerDeath;
		}

		protected  void Start() {
			enemyHealth = GetComponent<EnemyHealth>();
		}

		protected  void OnPlayerDeath() {
			enemyHealth.Kill();
		}
	}
}