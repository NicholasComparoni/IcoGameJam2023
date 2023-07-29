using System;
using UnityEngine;

namespace ICO321 {
	public class UIPlayer : MonoBehaviour {
		[SerializeField] private RectTransform heartsContainer;
		[SerializeField] private GameObject heartPrefab;
		private GameObject player;
		private PlayerHealth playerPlayerHealth;

		private void Awake() {
			player = GameObject.FindGameObjectWithTag("Player");
			playerPlayerHealth = player.GetComponent<PlayerHealth>();
			playerPlayerHealth.OnHealthUpdated += OnPlayerHealthUpdated;
		}

		private void OnPlayerHealthUpdated(int current, int max) {
			int childCount = heartsContainer.childCount;

			for (int i = childCount - 1; i >= 0; i--) {
				Transform child = heartsContainer.GetChild(i);
				Destroy(child.gameObject);
			}
			for (int i = 0; i < current; i++) {
				var newHeart = Instantiate(heartPrefab, heartsContainer);
			}
		}
	}
}