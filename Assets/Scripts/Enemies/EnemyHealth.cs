using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace ICO321 {
	public class EnemyHealth : MonoBehaviour {
		[FormerlySerializedAs("phase")] public TypesUtility.Phase enemyPhase;
		public event Action OnDeath;
		public GameObject deathVfx;
		[SerializeField] private AudioClip deathClip;
		private bool isDead;

		public void Kill() {
			Die();
		}

		public void Damage(TypesUtility.Phase bulletPhase) {
			if (bulletPhase == enemyPhase) {
				Die();
			}
		}

		private void Die() {
			if (!isDead) {
				isDead = true;
				var vfx = PoolManager.Instance.GetItem(deathVfx.name);
				vfx.transform.position = transform.position;
				vfx.GetComponent<ParticleVfx>().Color = PhaseManager.Instance.GetPhaseColor(enemyPhase);
				vfx.GetComponent<ParticleVfx>().Play();
				SfxManager.Instance.PlayClip(deathClip, 0.3f);
				OnDeath?.Invoke();
				Destroy(gameObject);
			}
		}
	}
}