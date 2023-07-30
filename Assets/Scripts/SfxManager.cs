using System;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

namespace ICO321 {
	public class SfxManager : MonoBehaviour {
		public static SfxManager Instance;
		[SerializeField] private AudioMixer audioMixer;
		[SerializeField] private int channelsNumber;
		private AudioSource[] channels;
		private int currentChannel;

		private void Awake() {
			if (Instance != null) {
				Destroy(this);
			}
			else {
				Instance = this;
			}
			if (channelsNumber <= 0) channelsNumber = 1;
			channels = new AudioSource[channelsNumber];
			GameObject musicChannels = new GameObject("Sfx Channels");
			musicChannels.transform.SetParent(transform);
			for (int i = 0; i < channelsNumber; i++) {
				var newAudioSource = musicChannels.gameObject.AddComponent<AudioSource>();
				newAudioSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("SFX")[0];
				newAudioSource.loop = false;
				newAudioSource.playOnAwake = false;
				channels[i] = newAudioSource;
			}
		}

		public void PlayClip(AudioClip clip, float pitchRange = 0) {
			channels[currentChannel].Stop();
			channels[currentChannel].clip = clip;
			if (pitchRange > 0)
				channels[currentChannel].pitch = 1 + Random.Range(-pitchRange, pitchRange);
			channels[currentChannel].Play();
			currentChannel = (currentChannel + 1) % channelsNumber;
		}
	}
}