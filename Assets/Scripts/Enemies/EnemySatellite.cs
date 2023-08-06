using System;
using UnityEngine;

namespace ICO321 {
	public class EnemySatellite : MonoBehaviour {
		[SerializeField] private EnemyHealth satelliteHealth;
		[SerializeField] private float stiffness = 10;
		public Transform followTarget;
		[SerializeField] private SpriteRenderer spriteRenderer;
		[SerializeField] private Transform facingTarget;
		private EnemyHealth enemyHealth;
		public event Action<EnemySatellite> Destroyed;

		// private void OnEnable() {
		// 	satelliteHealth.Reset();
		// }

		private void Update() {
			transform.position = Vector3.Lerp(transform.position, followTarget.position, stiffness * Time.deltaTime);

			if (facingTarget != null) {
				Vector2 directionToTarget = facingTarget.position - transform.position;
				float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;
				transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
			}
		}

		private void OnDestroy() {
			satelliteHealth.OnDeath -= OnDeath;
			Destroyed?.Invoke(this);
		}

		private void OnDisable() {
			Destroyed?.Invoke(this);
		}

		public void Setup(TypesUtility.Phase phase, EnemyHealth enemyHealth) {
			satelliteHealth.Reset();
			spriteRenderer.color = PhaseManager.Instance.GetPhaseColor(phase);
			satelliteHealth.EnemyPhase = phase;
			facingTarget = enemyHealth.transform;
			this.enemyHealth = enemyHealth;
			this.enemyHealth.OnDeath += OnDeath;
		}

		private void OnDeath() {
			Destroyed?.Invoke(this);
			facingTarget = null;
			this.enemyHealth.OnDeath -= OnDeath;
			enemyHealth = null;
			gameObject.SetActive(false);
		}
	}
}