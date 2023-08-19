using DG.Tweening;
using UnityEngine;
using UnityEngine.Audio;

namespace ICO321 {
	public class MusicManager : MonoBehaviour {
		public static MusicManager Instance;
		[SerializeField] private AudioMixer audioMixer;
		[SerializeField] private int channelsNumber;
		[Space] [SerializeField] private AudioClip[] tracks;
		private AudioSource[] channels;
		private int previousChannel = -1;
		private int currentChannel = -1;
		private AudioClip currentClipPlaying;
		private BeatManager beatManager;

		private void Awake() {
			if (Instance != null) {
				Destroy(gameObject);
			}
			else {
				Instance = this;
			}
			DontDestroyOnLoad(gameObject);
			beatManager = GetComponent<BeatManager>();
			if (channelsNumber > 0) {
				channels = new AudioSource[channelsNumber];
				GameObject musicChannels = new GameObject("Music Channels");
				musicChannels.transform.SetParent(transform);
				for (int i = 0; i < channelsNumber; i++) {
					var newAudioSource = musicChannels.gameObject.AddComponent<AudioSource>();
					newAudioSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("OST")[0];
					newAudioSource.loop = true;
					newAudioSource.playOnAwake = false;
					channels[i] = newAudioSource;
				}
				currentChannel = 0;
				previousChannel = -1;
			}
		}

		public void PlayTrack(int trackNumber) {
			if (tracks[trackNumber % tracks.Length] != currentClipPlaying) {
				channels[currentChannel].clip = tracks[trackNumber % tracks.Length];
				if (!channels[currentChannel].isPlaying) {
					channels[currentChannel].Play();
				}
				currentClipPlaying = channels[currentChannel].clip;
				channels[currentChannel].DOFade(1, 1);
				if (previousChannel >= 0)
					channels[previousChannel].DOFade(0, 1);
				beatManager.SetAudioSource(channels[currentChannel]);
				previousChannel = currentChannel;
				currentChannel = (currentChannel + 1) % channelsNumber;
			}
		}

		[ContextMenu("Next Track")]
		public void PlayNextTrack() {
			PlayTrack(1);
		}
	}
}