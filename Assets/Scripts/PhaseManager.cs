using System;
using UnityEngine;

namespace ICO321 {
	public class PhaseManager : MonoBehaviour {
		public static PhaseManager Instance;
		[SerializeField] private TypesUtility.Phase currentPhase;
		[ColorUsage(true,true)]public Color[] phaseColors;
		public float phaseDuration;
		private float counter;
		public event Action<TypesUtility.Phase> OnNewPhase;

		public TypesUtility.Phase CurrentPhase {
			get => currentPhase;
			private set {
				currentPhase = value;
				OnNewPhase?.Invoke(currentPhase);
			}
		}

		private void Awake() {
			if (Instance != null) {
				Destroy(this);
			}
			else {
				Instance = this;
			}
		}

		private void Start() {
			CurrentPhase = TypesUtility.Phase.Blue;
		}

		private void Update() {
			counter += Time.deltaTime;
			if (counter >= phaseDuration) {
				SwitchPhase();
			}
		}

		private void SwitchPhase() {
			CurrentPhase = (TypesUtility.Phase)((((int)CurrentPhase - 1) < 0 ? 2 : (int)CurrentPhase - 1));
			counter = 0;
		}

		public Color GetPhaseColor() {
			return phaseColors[(int)currentPhase];
		}
	}
}