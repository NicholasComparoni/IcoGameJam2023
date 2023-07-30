using ICO321;
using System;
using DG.Tweening;
using UnityEngine;

namespace ICO321 {
	public class PlayerHealth : MonoBehaviour {
		[SerializeField] private int maxHealth;
		[SerializeField] private int currentHealth;

		[Space] [SerializeField] private float graceTime;
		[SerializeField] private float maxDamageFlashAmount;

		[SerializeField] private SpriteRenderer spriteRenderer;
		[Space] [SerializeField] private AudioClip hitClip;
		[SerializeField] private AudioClip deadClip;
		[Space] [SerializeField] private GameObject deadVfx;
		private bool isDead;
		private float graceTimer;
		public event Action<int, int> OnHealthUpdated;
		public event Action OnPlayerDeath;

		private void Awake() {
			currentHealth = maxHealth;
			spriteRenderer.material = new Material(spriteRenderer.material);
		}

		private void Start() {
			OnHealthUpdated?.Invoke(currentHealth, maxHealth);
		}

		private void Update() {
			graceTimer -= Time.deltaTime;
		}

		public void Damage() {
			if (!isDead) {
				if (graceTimer < 0) {
					currentHealth -= 1;
					OnHealthUpdated?.Invoke(currentHealth, maxHealth);
					graceTimer = graceTime;
					if (currentHealth <= 0) {
						Die();
					}
					else {
						SfxManager.Instance.PlayClip(hitClip);
						float flash = 0;
						DOTween.To(() => flash, x => flash = x, maxDamageFlashAmount, graceTime).OnUpdate(() => { spriteRenderer.material.SetFloat("_Flash", flash); })
							.OnComplete(() => { spriteRenderer.material.SetFloat("_Flash", 0); });
					}
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