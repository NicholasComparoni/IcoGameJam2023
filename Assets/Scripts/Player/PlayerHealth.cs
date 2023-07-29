using ICO321;
using System;
using UnityEngine;

namespace ICO321 {
	public class PlayerHealth : MonoBehaviour {
		[SerializeField] private int maxHealth;
		[SerializeField] private int currentHealth;
		private bool isDead;
		public event Action<int, int> OnHealthUpdated;
		public event Action OnPlayerDeath;

		private void Awake() {
			currentHealth = maxHealth;
		}

		private void Start() {
			OnHealthUpdated?.Invoke(currentHealth, maxHealth);
		}

		public void Damage() {
			if (!isDead) {
				currentHealth -= 1;
				OnHealthUpdated?.Invoke(currentHealth, maxHealth);
				if (currentHealth <= 0) {
					Die();
				}
			}
		}

		public void Die()
		{
            OnPlayerDeath?.Invoke();
            isDead = true;
        }
	}
}