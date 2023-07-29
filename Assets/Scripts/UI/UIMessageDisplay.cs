using DG.Tweening;
using TMPro;
using UnityEngine;

namespace ICO321 {
	public class UIMessageDisplay : MonoBehaviour {
		private CanvasGroup canvasGroup;
		[SerializeField] private TMP_Text label;
		[SerializeField] private float fadeTime;

		private void Awake() {
			canvasGroup = GetComponent<CanvasGroup>();
			canvasGroup.alpha = 0;
		}

		public void ShowMessage(string text = "Test", float time = 2) {
			label.text = text;
			Sequence sequence = DOTween.Sequence();
			sequence.Append(canvasGroup.DOFade(1, fadeTime));
			sequence.AppendInterval(time);
			sequence.Append(canvasGroup.DOFade(0, fadeTime));
		}
	}
}