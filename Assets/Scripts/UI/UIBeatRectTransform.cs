using UnityEngine;

namespace ICO321 {
	public class UIBeatRectTransform : MonoBehaviour {
		public int channel;
		public Vector3 direction = Vector3.right;
		public RectTransform[] onBeatY;

		private void Update() {
			float band = BeatManager.Instance.audioBandBuffer[channel];
			foreach (var rt in onBeatY) {
				rt.localScale = Vector3.one + direction * band;
			}
		}
	}
}