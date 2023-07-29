using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace ICO321 {
	public class UIPhaseUpdater : MonoBehaviour {
		public List<Image> phaseImages;

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
		}
	}
}