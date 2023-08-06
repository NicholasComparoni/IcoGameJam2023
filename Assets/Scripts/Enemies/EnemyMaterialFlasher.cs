using DG.Tweening;
using UnityEngine;

namespace ICO321 {
	public class EnemyMaterialFlasher : MonoBehaviour {
		[SerializeField]private EnemyHealth enemyHealth;
		[SerializeField] private SpriteRenderer spriteRenderer;
		[SerializeField] private float maxFlash;
		[SerializeField] private float flashDuration;
		private Material material;
		private void Awake() {
			enemyHealth = GetComponent<EnemyHealth>();
			enemyHealth.OnHit += OnHit;
			enemyHealth.OnDeath += OnDeath;
			spriteRenderer.material = new Material(spriteRenderer.material);
			material = spriteRenderer.material;
		}

		private void OnDeath() {
			enemyHealth.OnHit -= OnHit;
			enemyHealth.OnDeath -= OnDeath;
		}

		private void OnHit() {
			float flash = maxFlash;
			DOTween.To(() => flash, x => flash = x, 0, flashDuration)
				.OnUpdate(() => {
					material.SetFloat("_Flash",flash);		
				});
			
		}
	}
}