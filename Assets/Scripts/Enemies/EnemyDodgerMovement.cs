using UnityEngine;

namespace ICO321 {
	public class EnemyDodgerMovement : MonoBehaviour {
		public float movementWidth;
		public float timeOffset = 0.5f;
		//public float speed;

		private Vector3 leftPoint;
		private Vector3 rightPoint;

		private Rigidbody2D rb;

		private void Start() {
			var t = transform;
			Vector3 position = t.position;
			var right = t.right;
			leftPoint = position + (right * (-movementWidth / 2f));
			rightPoint = position + (right * (movementWidth / 2f));
			rb = GetComponent<Rigidbody2D>();
		}

		private void Update() {
			rb.MovePosition(Vector3.Lerp(leftPoint, rightPoint, Mathf.PingPong(Time.time + timeOffset, 1.0f)));
		}
	}
}