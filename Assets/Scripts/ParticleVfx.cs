using System;
using UnityEngine;

namespace ICO321 {
	public class ParticleVfx : MonoBehaviour {
		private Color color;
		private ParticleSystem particleSystem;
		public Color Color {
			get => color;
			set {
				color = value;
				ParticleSystem.MainModule particleSystemMain = particleSystem.main;
				particleSystemMain.startColor = value;
			}
		}

		private void Awake() {
			particleSystem = GetComponent<ParticleSystem>();
		}

		public void Play() {
			particleSystem.Play();
		}
	}
}