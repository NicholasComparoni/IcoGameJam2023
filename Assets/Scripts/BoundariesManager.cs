using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ICO321 {
	public class BoundariesManager : MonoBehaviour {
		public static BoundariesManager Instance;
		private Camera mainCamera;
		public Bounds bounds = new Bounds();

		private void Awake() {
			if (Instance != null) Destroy(this);
			else {
				Instance = this;
			}
			mainCamera = Camera.main;
		}

		private void Start() {
			var min = mainCamera.ViewportToWorldPoint(Vector3.zero);
			min.z = -100;
			var max = mainCamera.ViewportToWorldPoint(Vector3.one);
			max.z = 100;
			bounds.min = min;
			bounds.max = max;
		}

		public bool IsInsideBounds(Vector3 point) {
			return bounds.Contains(point);
		}
	}
}