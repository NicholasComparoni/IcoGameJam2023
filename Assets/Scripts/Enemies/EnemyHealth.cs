using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace ICO321 {
	public class EnemyHealth : MonoBehaviour {
		[FormerlySerializedAs("phase")] public TypesUtility.Phase enemyPhase;
		public event Action OnDeath;
		public GameObject deathVfx;
		[SerializeField] private AudioClip deathClip;

		public void Kill() {
			Die();
		}

		public bool Damage(TypesUtility.Phase bulletPhase) {
			if (bulletPhase == enemyPhase) {
				Die();
				return true;
			}
			else return false;
		}

		private void Die() {
			var vfx = PoolManager.Instance.GetItem(deathVfx.name);
			vfx.transform.position = transform.position;
			vfx.GetComponent<ParticleVfx>().Color = PhaseManager.Instance.GetPhaseColor(enemyPhase);
			vfx.GetComponent<ParticleVfx>().Play();
			OnDeath?.Invoke();
			SfxManager.Instance.PlayClip(deathClip, 0.3f);
			Destroy(gameObject);
		}
	}
}