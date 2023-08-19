using System;
using UnityEngine;

namespace ICO321 {
	public class SpriteEmissiveBeatManager : MonoBehaviour {
		[Serializable]
		public struct BeatGroup {
			public int channel;
			public SpriteRenderer[] sprites;
		}

		public BeatGroup[] beatGroups;
		public float emissive;

		private void Start() {
			if (BeatManager.Instance == null) Destroy(this);
			foreach (var bg in beatGroups) {
				foreach (var s in bg.sprites) {
					s.material = new Material(s.material);
					s.material.SetFloat("_Emissive", 0);
				}
			}
		}

		private void Update() {
			foreach (var bg in beatGroups) {
				foreach (var s in bg.sprites) {
					s.material.SetFloat("_Emissive", BeatManager.Instance.audioBandBuffer[bg.channel] * emissive);
				}
			}
		}
	}
}