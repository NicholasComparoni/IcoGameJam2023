using System;
using UnityEngine;

namespace ICO321 {
	public class VictoryZone : MonoBehaviour {
		private void OnTriggerEnter2D(Collider2D other) {
			Debug.Log($"trigger enter");
			if (other.gameObject.GetComponent<PlayerHealth>() != null) {
				Debug.Log($"Victory");
				LevelManager.Instance.VictoryReached();
			}
		}
	}
}