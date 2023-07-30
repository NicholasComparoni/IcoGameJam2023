using UnityEngine;

namespace ICO321 {
	public class EnemyDodgerMovement : MonoBehaviour {
		public float movementWidth;
		public float timeOffset = 0.5f;

		private float timer;

		private Vector3 leftOffset;
		private Vector3 rightOffset;

		private Rigidbody2D rb;

		private void Start() {
			timer = timeOffset;
			leftOffset = (transform.position + (transform.right * (-movementWidth / 2f))) - transform.parent.position;
			rightOffset = (transform.position + (transform.right * (movementWidth / 2f))) - transform.parent.position;
			rb = GetComponent<Rigidbody2D>();
		}

		private void Update() {
			timer += Time.deltaTime;
			Vector3 leftPoint = transform.parent.position + leftOffset;
            Vector3 rightPoint = transform.parent.position + rightOffset;
            Vector3 newPos = Vector3.Lerp(leftPoint, rightPoint, Mathf.PingPong(timer, 1.0f));
			rb.velocity = (newPos - transform.position) / Time.deltaTime;
        }
	}
}