using System;
using UnityEngine;

namespace ICO321 {
	public class VictoryZone : MonoBehaviour {
		private void OnTriggerEnter2D(Collider2D other) {
			if (other.gameObject.GetComponent<PlayerHealth>() != null) {
				LevelManager.Instance.VictoryReached();
			}
		}
	}
}