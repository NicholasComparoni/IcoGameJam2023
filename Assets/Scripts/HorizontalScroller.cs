using UnityEngine;

namespace ICO321 {
	public class HorizontalScroller : MonoBehaviour {
		public float speed;
		private float speedMultiplier;

		private void Start() {
			speedMultiplier = 1;
			if (LevelManager.Instance != null) {
				LevelManager.Instance.OnSpeedUpdated += OnSpeedUpdated;
			}
		}

		public void Setup(LevelSettings levelSettings) {
			speed = levelSettings.scrollingSpeed;
		}

		private void OnSpeedUpdated(float newSpeedMultiplier) {
			speedMultiplier = newSpeedMultiplier;
		}

		private void Update() {
			transform.Translate(Vector3.left * (speed * speedMultiplier * Time.deltaTime));
		}
	}
}