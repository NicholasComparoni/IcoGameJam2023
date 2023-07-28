using UnityEngine;

namespace ICO321 {
	public class DestroyOutOfBounds : MonoBehaviour {
		public bool destroy;

		private void Update() {
			if (!BoundariesManager.Instance.IsInsideBounds(transform.position)) {
				if (destroy)
					Destroy(gameObject, 1);
				else {
					gameObject.SetActive(false);
				}
			}
		}
	}
}