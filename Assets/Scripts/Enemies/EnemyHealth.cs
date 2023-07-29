using ICO321;
using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace ICO321 {
	public class EnemyHealth : MonoBehaviour {
		[FormerlySerializedAs("phase")] public TypesUtility.Phase enemyPhase;
		public event Action OnDeath;

		public bool Damage(TypesUtility.Phase atkPhs) {
			if (atkPhs == enemyPhase) {
				Die();
				return true;
			}
			else return false;
		}

		private void Die() {
			OnDeath?.Invoke();
			Destroy(gameObject);
		}
    }
}