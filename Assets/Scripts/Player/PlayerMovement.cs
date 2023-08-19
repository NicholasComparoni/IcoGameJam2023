using UnityEngine;

namespace ICO321 {
	public class PlayerMovement : MonoBehaviour {
		public float movementSpeed = 5f;
		public float maxSpeed;
		[SerializeField] private ParticleSystem mainThrusters;
		[SerializeField] private ParticleSystem[] topThrusters;
		[SerializeField] private ParticleSystem[] bottomThrusters;
		private Rigidbody2D rb;
		private Vector2 direction;

		private void Awake() {
			rb = GetComponent<Rigidbody2D>();
		}

		private void Update() {
			direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));


			if (direction.x == 0) {
				TurnOffThrusters();
				mainThrusters.Stop();
				DoVerticalThrusters();
			}
			else if (direction.x > 0) {
				TurnOffThrusters();
				mainThrusters.Play();
				DoVerticalThrusters();
			}
			else if (direction.x < 0) {
				TurnOffThrusters();
				topThrusters[1].Play();
				bottomThrusters[1].Play();
				mainThrusters.Stop();
				DoVerticalThrusters();
			}


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

		private void TurnOffThrusters() {
			topThrusters[0].Stop();
			topThrusters[1].Stop();
			bottomThrusters[0].Stop();
			bottomThrusters[1].Stop();
		}

		private void DoVerticalThrusters() {
			if (direction.y > 0) {
				bottomThrusters[0].Play();
				bottomThrusters[1].Play();
			}
			// else {
			// 	bottomThrusters[0].Stop();
			// 	bottomThrusters[1].Stop();
			// }
			if (direction.y < 0) {
				topThrusters[0].Play();
				topThrusters[1].Play();
			}
			// else {
			// 	topThrusters[0].Stop();
			// 	topThrusters[1].Stop();
			// }

			// if (direction.y == 0) {
			// 	topThrusters[0].Stop();
			// 	topThrusters[1].Stop();
			// 	bottomThrusters[0].Stop();
			// 	bottomThrusters[1].Stop();
			// }
		}
	}
}