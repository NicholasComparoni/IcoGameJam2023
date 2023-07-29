using UnityEngine;
using Random = UnityEngine.Random;

namespace ICO321 {
	public class EnemyAsteroid : MonoBehaviour {
		[SerializeField] private float rotationSpeed;
		private EnemyHealth health;

		private void Awake() {
			rotationSpeed = Random.Range(-rotationSpeed, rotationSpeed);
			health = GetComponent<EnemyHealth>();
		}

		private void Update() {
			transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
		}

		private void OnCollisionEnter2D(Collision2D other) {
			var enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
			if (enemyHealth != null) {
				if (enemyHealth.enemyPhase == health.enemyPhase) {
					enemyHealth.Damage(health.enemyPhase);
					health.Kill();
				}
			}
		}
	}
}