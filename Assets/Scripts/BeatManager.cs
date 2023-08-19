using System;
using UnityEngine;

namespace ICO321 {
	public class BeatManager : MonoBehaviour {
		public static BeatManager Instance;
		[SerializeField] private AudioSource audioSource;
		private static float[] samples = new float[512];
		private static float[] frequencyBand = new float[8];
		private static float[] bandBuffer = new float[8];
		private static float[] bufferDecrease = new float[8];

		private float[] frequencyBandHighest = new float[8];
		public float[] audioBand = new float[8];
		public float[] audioBandBuffer = new float[8];

		public float amplitude;
		public float amplitudeBuffer;
		public float amplitudeHighest = -10;

		private void Awake() {
			if (Instance != null) {
				Destroy(this);
			}
			else {
				Instance = this;
			}
		}

		public void SetAudioSource(AudioSource newAudioSource) {
			audioSource = newAudioSource;
			amplitudeHighest = 0;
			frequencyBandHighest = new float[8];
			Debug.Log($"AudioSource updated {audioSource}");
		}

		private void Update() {
			GetSpectrumAudioSource();
			MakeFrequencyBands();
			BandBuffer();
			CreateAudioBands();
			GetAmplitude();
		}

		private void GetSpectrumAudioSource() {
			audioSource.GetSpectrumData(samples, 0, FFTWindow.Blackman);
		}

		private void MakeFrequencyBands() {
			int count = 0;
			for (int i = 0; i < 8; i++) {
				float average = 0;
				int sampleCount = (int)Mathf.Pow(2, i) * 2;

				if (i == 7) {
					sampleCount += 2;
				}
				for (int j = 0; j < sampleCount; j++) {
					average += samples[count] * (count + 1);
					count++;
				}
				average /= count;
				frequencyBand[i] = average;
			}
		}

		private void BandBuffer() {
			for (int i = 0; i < 8; ++i) {
				if (frequencyBand[i] > bandBuffer[i]) {
					bandBuffer = frequencyBand;
					bufferDecrease[i] = 0.005f;
				}

				if (frequencyBand[i] < bandBuffer[i]) {
					bandBuffer[i] -= bufferDecrease[i];
					bufferDecrease[i] *= 1.2f;
				}
			}
		}

		private void CreateAudioBands() {
			for (int i = 0; i < 8; i++) {
				if (frequencyBand[i] > frequencyBandHighest[i]) {
					frequencyBandHighest[i] = frequencyBand[i];
				}
				audioBand[i] = frequencyBand[i] / frequencyBandHighest[i];
				audioBandBuffer[i] = bandBuffer[i] / frequencyBandHighest[i];
			}
		}

		private void GetAmplitude() {
			float currentAmplitude = 0;
			float currentAmplitudeBuffer = 0;
			for (int i = 0; i < audioBand.Length; i++) {
				currentAmplitude += audioBand[i];
				currentAmplitudeBuffer += audioBandBuffer[i];
			}
			if (currentAmplitude > amplitudeHighest) {
				amplitudeHighest = currentAmplitude;
			}
			amplitude = currentAmplitude / amplitudeHighest;
			amplitudeBuffer = currentAmplitudeBuffer / amplitudeHighest;
		}
	}
}