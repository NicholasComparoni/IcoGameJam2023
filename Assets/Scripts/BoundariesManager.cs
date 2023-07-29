using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ICO321 {
	public class BoundariesManager : MonoBehaviour {
		public static BoundariesManager Instance;
		private Camera mainCamera;
		public Bounds bounds = new Bounds();
		public event Action<Bounds> OnBoundsUpdated; 
		private void Awake() {
			if (Instance != null) Destroy(this);
			else {
				Instance = this;
			}
			mainCamera = Camera.main;
			
		}

		private void OnDrawGizmos() {
			mainCamera = Camera.main;
			Start();
			Gizmos.color = Color.green;
			Gizmos.DrawWireCube(bounds.center, bounds.size);
		}

		private IEnumerator Start() {
			yield return null;
			var min = mainCamera.ViewportToWorldPoint(Vector3.zero);
			min.z = -100;
			var max = mainCamera.ViewportToWorldPoint(Vector3.one);
			max.z = 100;
			bounds.min = min;
			bounds.max = max;
			bounds.extents += Vector3.forward * 10;
			OnBoundsUpdated?.Invoke(bounds);
		}

		public bool IsInsideBounds(Vector3 point) {
			return bounds.Contains(point);
		}
	}
}