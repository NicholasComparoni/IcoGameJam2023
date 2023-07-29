using System;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.UI;

namespace ICO321 {
	public class UICanvasManager : MonoBehaviour {
		private Canvas canvas;
		private Camera camera;
		[SerializeField] private UIFade uiFade;
		[SerializeField] private UIMessageDisplay uiMessageDisplay;

		private void Awake() {
			camera = Camera.main;
			canvas = GetComponent<Canvas>();
			canvas.worldCamera = Camera.main;
		}

		private void Start() {
			var canvasScaler = canvas.GetComponent<CanvasScaler>();
			canvasScaler.referenceResolution = new Vector2(camera.GetComponent<PixelPerfectCamera>().refResolutionX / 2f, camera.GetComponent<PixelPerfectCamera>().refResolutionY / 2f);
			canvasScaler.referencePixelsPerUnit = camera.GetComponent<PixelPerfectCamera>().assetsPPU;
			if (LevelManager.Instance != null) {
				LevelManager.Instance.ShowMessage += OnShowMessage;
				LevelManager.Instance.Fade += OnFade;
			}
		}

		private void OnFade(bool doFadeIn) {
			if (doFadeIn) uiFade.FadeIn();
			else uiFade.FadeOut();
		}

		private void OnShowMessage(string text, float time) {
			uiMessageDisplay.ShowMessage(text, time);
		}
	}
}