using System;
using UnityEngine;

namespace ICO321 {
	public class EnemyHealth : MonoBehaviour {
		[SerializeField] private float health = 1;
		private float defaultHealth;
		[SerializeField] private bool canHaveSatellites;
		[SerializeField] private bool deactivateOnDeath;
		[SerializeField] private EnemySatelliteManager enemySatelliteManager;
		[SerializeField] private TypesUtility.Phase enemyPhase;
		public event Action OnDeath;
		public event Action OnHit;
		public GameObject deathVfx;

		[SerializeField] private AudioClip hitClip;
		[SerializeField] private AudioClip deathClip;
		private bool isDead;

		public TypesUtility.Phase EnemyPhase {
			get => enemyPhase;
			set => enemyPhase = value;
		}

		private void Awake() {
			if (enemySatelliteManager == null) enemySatelliteManager = GetComponent<EnemySatelliteManager>();
			defaultHealth = health;
		}

		private void OnDestroy() {
			if (deactivateOnDeath) {
				
				gameObject.SetActive(false);
			}
			else {
				Destroy(gameObject);
			}
			OnDeath?.Invoke();
		}

		public void Kill() {
			Die();
		}

		public void Damage(TypesUtility.Phase bulletPhase) {
			//Debug.Log($"{bulletPhase} , {EnemyPhase}");
			if (bulletPhase == EnemyPhase) {
				health--;
				if (health <= 0) {
					Die();
				}
				else {
					SfxManager.Instance.PlayClip(hitClip, 0.3f);
				}
			}
			else {
				OnHit?.Invoke();
				//colpito da un bullet di colore diverso!
				if (canHaveSatellites) {
					enemySatelliteManager.AddSatellite(bulletPhase);
				}
			}
		}

		private void Die() {
			if (!isDead) {
				isDead = true;
				var vfx = PoolManager.Instance.GetItem(deathVfx.name);
				vfx.transform.position = transform.position;
				vfx.GetComponent<ParticleVfx>().Color = PhaseManager.Instance.GetPhaseColor(EnemyPhase);
				vfx.GetComponent<ParticleVfx>().Play();
				SfxManager.Instance.PlayClip(deathClip, 0.3f);
				OnDeath?.Invoke();
				if (deactivateOnDeath) {
					gameObject.SetActive(false);
				}
				else {
					Destroy(gameObject);
				}
			}
		}

		public void Reset() {
			health = defaultHealth;
			isDead = false;
		}
	}
}