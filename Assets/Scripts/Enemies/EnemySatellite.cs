using System;
using UnityEngine;

namespace ICO321 {
	public class EnemySatellite : MonoBehaviour {
		[SerializeField] private EnemyHealth satelliteHealth;
		[SerializeField] private float stiffness = 10;
		public Transform followTarget;
		[SerializeField] private SpriteRenderer spriteRenderer;
		[SerializeField] private Transform facingTarget;
		public event Action<EnemySatellite> Destroyed;

		private void Awake() {
			satelliteHealth.OnDeath += OnSatelliteDeath;
		}

		private void OnEnable() {
			satelliteHealth.Reset();
		}

		private void OnSatelliteDeath() {
			Destroyed?.Invoke(this);
		}

		private void Update() {
			transform.position = Vector3.Lerp(transform.position, followTarget.position, stiffness * Time.deltaTime);

			if (facingTarget != null) {
				Vector2 directionToTarget = facingTarget.position - transform.position;
				float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;
				transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
			}
		}

		private void OnDestroy() {
			//Debug.LogWarning($"Should not destroy this [{name}]");
			satelliteHealth.OnDeath -= OnDeath;
		}

		public void Setup(TypesUtility.Phase phase, EnemyHealth enemyHealth) {
			satelliteHealth.Reset();
			spriteRenderer.color = PhaseManager.Instance.GetPhaseColor(phase);
			satelliteHealth.EnemyPhase = phase;
			facingTarget = enemyHealth.transform;
			enemyHealth.OnDeath += OnDeath;
		}

		private void OnDeath() {
			satelliteHealth.OnDeath -= OnDeath;
			facingTarget = null;
			Destroyed?.Invoke(this);
			gameObject.SetActive(false);
		}
	}
}