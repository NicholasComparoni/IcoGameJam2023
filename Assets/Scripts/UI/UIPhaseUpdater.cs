using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ICO321 {
	public class UIPhaseUpdater : MonoBehaviour {
		public List<Image> phaseImages;
		public TMP_Text counterText;

		private void Start() {
			if (PhaseManager.Instance == null) {
				Debug.LogError($"Missing PhaseManager");
				Destroy(this);
			}
			PhaseManager.Instance.OnNewPhase += OnNewPhase;
		}

		private void OnNewPhase(TypesUtility.Phase newPhase) {
			foreach (Image phaseImage in phaseImages) {
				phaseImage.DOKill();
				phaseImage.DOColor(PhaseManager.Instance.GetPhaseColor(), PhaseManager.Instance.phaseDuration / 10f);
			}
			counterText.text = ((int)newPhase + 1).ToString();
			counterText.transform.DOPunchScale(Vector3.one * 0.2f, 0.2f);
		}
	}
}