using UnityEngine;

namespace ICO321 {
	public class PlayerMovement : MonoBehaviour {
		public float movementSpeed = 5f;

		private Rigidbody2D rb;

		private void Start() {
			rb = GetComponent<Rigidbody2D>();
		}

		private void Update() {
			var horizontalInput = Input.GetAxis("Horizontal");
			var verticalInput = Input.GetAxis("Vertical");
			var direction = new Vector2(horizontalInput, verticalInput);
			rb.velocity = direction * movementSpeed;
		}

		private void FixedUpdate() { }
	}
}