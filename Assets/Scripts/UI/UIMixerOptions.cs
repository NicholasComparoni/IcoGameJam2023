using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UIMixerOptions : MonoBehaviour {
	[SerializeField] private AudioMixer audioMixer;

	[SerializeField] private Slider ostSlider;
	[SerializeField] private Slider sfxSlider;

	private void Start() {
		ostSlider.value = PlayerPrefs.GetFloat("OST", 0.5f);
		audioMixer.SetFloat("OST", Mathf.Log10(ostSlider.value) * 20);
		sfxSlider.value = PlayerPrefs.GetFloat("SFX", 0.75f);
		audioMixer.SetFloat("SFX", Mathf.Log10(sfxSlider.value) * 20);
	}

	public void SetOSTLevel(float sliderValue) {
		audioMixer.SetFloat("OST", Mathf.Log10(sliderValue) * 20);
		PlayerPrefs.SetFloat("OST", sliderValue);
	}

	public void SetSFXLevel(float sliderValue) {
		audioMixer.SetFloat("SFX", Mathf.Log10(sliderValue) * 20);
		PlayerPrefs.SetFloat("SFX", sliderValue);
	}
}