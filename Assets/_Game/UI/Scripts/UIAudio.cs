using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class UIAudio : MonoBehaviour
{
    [SerializeField] private AudioClip _hoverSound;
    [SerializeField] private AudioClip _clickSound;

    [SerializeField] private List<AudioButton> _audioButtons;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();

        _audioButtons.ForEach(button => button.Init(_audioSource, _hoverSound));
    }

    private void OnEnable()
    {
        _audioButtons.ForEach(button => button.GetButtonComponent().onClick.AddListener(PlayClickSound));
    }

    private void OnDisable()
    {
        _audioButtons.ForEach(button => button.GetButtonComponent().onClick.RemoveListener(PlayClickSound));
    }

    private void PlayClickSound()
    {
        _audioSource.PlayOneShot(_clickSound);
    }
}
