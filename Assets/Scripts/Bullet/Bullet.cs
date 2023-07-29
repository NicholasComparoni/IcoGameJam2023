using UnityEngine;

namespace ICO321 {
	public class Bullet : MonoBehaviour {
		[SerializeField] private SpriteRenderer spriteRenderer;
		[SerializeField] private TypesUtility.Phase phase;
		[SerializeField] private Vector3 direction;

		private Collider2D col2D;
		public float speed;
		public Vector3 Direction {
			get => direction;
			set { direction = value.normalized; }
		}
		public TypesUtility.Phase Phase {
			get => phase;
			set {
				phase = value;
				spriteRenderer.color = PhaseManager.Instance.GetPhaseColor();
			}
		}

		private void Awake() {
			direction = direction.normalized;
			col2D = GetComponent<Collider2D>();
			if (col2D == null) {
				Debug.LogError($"{name} is a Bullet with no Collider2D");
			}
			else {
				col2D.isTrigger = true;
			}
		}

		private void Update() {
			float angleDeg = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler(0f, 0f, angleDeg);
			transform.position += Direction * (speed * Time.deltaTime);
		}

		private void OnTriggerEnter2D(Collider2D other) {
			var enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
			if (enemyHealth != null) {
				enemyHealth.Damage(phase);
			}
			var playerHealth = other.gameObject.GetComponent<PlayerHealth>();
			if (playerHealth != null) {
				playerHealth.Damage();
			}
			gameObject.SetActive(false);
		}
	}
}