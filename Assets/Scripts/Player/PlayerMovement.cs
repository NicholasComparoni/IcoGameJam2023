using UnityEngine;

namespace ICO321 {
	public class PlayerMovement : MonoBehaviour {
		public float movementSpeed = 5f;
		public float maxSpeed;

		private Rigidbody2D rb;
		private Vector2 direction;

		private void Awake() {
			rb = GetComponent<Rigidbody2D>();
		}

		private void Update() {
			direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
			var position = transform.position;

			if (position.x < BoundariesManager.Instance.bounds.min.x) {
				direction += Vector2.right;
			}
			else if (position.x > BoundariesManager.Instance.bounds.max.x) {
				direction += Vector2.left;
			}

			if (position.y < BoundariesManager.Instance.bounds.min.y) {
				direction += Vector2.up;
			}
			else if (position.y > BoundariesManager.Instance.bounds.max.y) {
				direction += Vector2.down;
			}
			rb.velocity = direction * movementSpeed;
		}

		private void FixedUpdate() {
			rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
		}
	}
}