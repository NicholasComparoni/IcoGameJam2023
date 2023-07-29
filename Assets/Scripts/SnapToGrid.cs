using System;
using UnityEngine;

public class SnapToGrid : MonoBehaviour {
	public Vector2 snap = Vector2.one;

	private void OnDrawGizmos() {
		transform.position = Snapping.Snap(transform.position, snap);
	}

	private void Awake() {
		Destroy(this);
	}
}