using DG.Tweening;
using UnityEngine;

namespace ICO321 {
	public class UIFade : MonoBehaviour {
		private CanvasGroup canvasGroup;
		public float fadeDuration = 1;

		private void Awake() {
			canvasGroup = GetComponent<CanvasGroup>();
			canvasGroup.alpha = 1;
			FadeIn();
		}

		public void FadeOut() {
			canvasGroup.DOKill();
			canvasGroup.DOFade(1, fadeDuration);
		}

		public void FadeIn() {
			canvasGroup.DOKill();
			canvasGroup.DOFade(0, fadeDuration);
		}
	}
}