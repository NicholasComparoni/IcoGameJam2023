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
			var horizontalInput = Input.GetAxis("Horizontal");
			var verticalInput = Input.GetAxis("Vertical");
			direction = new Vector2(horizontalInput, verticalInput);
			var position = transform.position;

			if (position.x < BoundariesManager.Instance.bounds.min.x) {
				//position.x = BoundariesManager.Instance.bounds.min.x;
				//rb.velocity += Vector2.right;
				//rb.AddForce(Vector2.right * (movementSpeed + 1));
				direction += Vector2.right;
			}
			else if (position.x > BoundariesManager.Instance.bounds.max.x) {
				//position.x = BoundariesManager.Instance.bounds.max.x;
				//rb.AddForce(Vector2.left * (movementSpeed + 1));
				direction += Vector2.left;
			}

			if (position.y < BoundariesManager.Instance.bounds.min.y) {
				//position.y = BoundariesManager.Instance.bounds.min.y;
				//rb.AddForce(Vector2.up * (movementSpeed + 1));
				direction += Vector2.up;
			}
			else if (position.y > BoundariesManager.Instance.bounds.max.y) {
				//position.y = BoundariesManager.Instance.bounds.max.y;
				//rb.AddForce(Vector2.down * (movementSpeed + 1));
				direction += Vector2.down;
			}

			//transform.position = position;
			direction = direction.normalized;
			rb.velocity = direction * movementSpeed;
			rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
		}

		private void FixedUpdate() {
			rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
		}
	}
}