using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioOptions : MonoBehaviour
{
    private const string MasterVolume = nameof(MasterVolume);
    private const string MusicVolume = nameof(MusicVolume);
    private const string UIVolume = nameof(UIVolume);

    [SerializeField] private AudioMixerGroup _mixer;
    [SerializeField] private List<Slider> _volumeSliders;

    [SerializeField] private GameObject _optionsPanel;

    [SerializeField] private float _volumeReductionLogarithmicCoefficient = 20;
    [SerializeField, Range(0.0001f, 1f)] private float _startAudioValue = 0.5f;

    private float _maxAudioValue = 0;
    private float _minAudioValue = -80;

    private float previousMasterVolume;

    private void Start()
    {
        _volumeSliders.ForEach(slider => slider.value = _startAudioValue);
        _optionsPanel.SetActive(false);
    }

    public void ToggleAudio(Toggle toggle)
    {
        bool enabled = toggle.isOn;

        if (enabled)
        {
            _mixer.audioMixer.SetFloat(MasterVolume, previousMasterVolume);
        }
        else
        {
            _mixer.audioMixer.GetFloat(MasterVolume, out previousMasterVolume);
            _mixer.audioMixer.SetFloat(MasterVolume, _minAudioValue);
        }

        _volumeSliders.ForEach(slider => slider.interactable = enabled);
    }

    public void ChangeMasterVolume(Slider masterSlider)
    {
        _mixer.audioMixer.SetFloat(MasterVolume, ValueToVolume(masterSlider.value));
    }

    public void ChangeMusicVolume(Slider musicSlider)
    {
        _mixer.audioMixer.SetFloat(MusicVolume, ValueToVolume(musicSlider.value));
    }

    public void ChangeUIVolume(Slider UISlider)
    {
        _mixer.audioMixer.SetFloat(UIVolume, ValueToVolume(UISlider.value));
    }

    private float ValueToVolume(float value)
    {
        return Mathf.Log10(value) * _volumeReductionLogarithmicCoefficient;
    }
}
