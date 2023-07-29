using System;
using System.Collections;
using UnityEngine;

namespace ICO321 {
	public class UIMenuManager : MonoBehaviour {
		private ScenesManager scenesManager;
		[SerializeField] private UIFade uiFade;
		private bool allowInput = true;

		private void Awake() {
			scenesManager = FindObjectOfType<ScenesManager>();
		}

		public void StartGame() {
			if (allowInput) {
				allowInput = false;
				StartCoroutine(LoadingGame());
				
			}
		}

		private IEnumerator LoadingGame() {
			uiFade.FadeOut();
			float t = 0;
			while (t < uiFade.fadeDuration) {
				t += Time.deltaTime;
				yield return null;
			}
			scenesManager.LoadNextLevel();
		}

		public void QuitGame() {
			Application.Quit();
		}
	}
}