using UnityEngine;

namespace ICO321 {
	public class FollowTarget : MonoBehaviour {
		public Transform targetTransform;

		private void Start() {
			transform.SetParent(null);
		}

		private void Update() {
			transform.position = targetTransform.position;
		}
	}
}