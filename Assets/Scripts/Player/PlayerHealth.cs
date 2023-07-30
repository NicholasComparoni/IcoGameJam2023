using ICO321;
using System;
using UnityEngine;

namespace ICO321 {
	public class PlayerHealth : MonoBehaviour {
		[SerializeField] private int maxHealth;
		[SerializeField] private int currentHealth;
		[SerializeField] private AudioClip hitClip;
		[SerializeField] private AudioClip deadClip;
		[SerializeField] private GameObject deadVfx;
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
				else {
					SfxManager.Instance.PlayClip(hitClip);
				}
			}
		}

		public void Kill() {
			currentHealth = 0;
			Damage();
		}

		public void Die() {
			SfxManager.Instance.PlayClip(deadClip);
			OnPlayerDeath?.Invoke();
			isDead = true;
			var deathVfx = Instantiate(deadVfx, transform);
			
			deathVfx.transform.SetParent(null);
			Destroy(gameObject);
		}
	}
}