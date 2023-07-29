using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ICO321 {
	public class ScenesManager : MonoBehaviour {
		public string mainScene;
		public string nextLevel;
		private bool isLoading;

		public void ReturnToMainMenu() {
			StartCoroutine(LoadSceneAsync(mainScene));
		}

		public void LoadNextLevel() {
			StartCoroutine(LoadSceneAsync(nextLevel));
		}

		public void LoadScene(string sceneToLoad) {
			StartCoroutine(LoadSceneAsync(sceneToLoad));
		}

		private IEnumerator LoadSceneAsync(string sceneToLoad) {
			if (!isLoading) {
				isLoading = true;
				AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneToLoad);

				while (!asyncLoad.isDone) {
					yield return null;
				}
			}
		}
	}
}