using System;
using DG.Tweening;
using UnityEngine;

namespace ICO321 {
	public class Bullet : MonoBehaviour {
		public int type;
		private Vector3 direction;
		public float speed;
		public Vector3 Direction {
			get => direction;
			set { direction = value.normalized; }
		}

		private void Awake() {
			direction = direction.normalized;
		}

		private void Update() {
			float angleDeg = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler(0f, 0f, angleDeg);
			transform.position += Direction * (speed * Time.deltaTime);
		}

		private void OnTriggerEnter2D(Collider2D other) { }
	}
}