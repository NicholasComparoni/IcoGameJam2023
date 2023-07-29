using System;
using UnityEngine;

namespace ICO321 {
	public class HorizontalScroller : MonoBehaviour {
		public float speed;
		private float speedMultiplier;

		private void Awake() {
			speedMultiplier = 1;
			if (LevelManager.Instance != null) {
				LevelManager.Instance.OnSpeedUpdated += OnSpeedUpdated;
			}
		}

		private void OnSpeedUpdated(float newSpeedMultiplier) {
			speedMultiplier = newSpeedMultiplier;
		}

		private void Update() {
			transform.position += Vector3.left * (speed * speedMultiplier * Time.deltaTime);
		}

	}
}