using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class AudioButton : MonoBehaviour
{
    private Button _button;

    private AudioSource _audioSource;
    private AudioClip _hoverSound;

    private void Awake()
    {
        _button = GetComponent<Button>();

        EventTrigger eventTrigger = gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry hoverEvent = new EventTrigger.Entry()
        {
            eventID = EventTriggerType.PointerEnter
        };

        hoverEvent.callback.AddListener(PlayHoverSound);
        eventTrigger.triggers.Add(hoverEvent);
    }

    public void Init(AudioSource audioSource, AudioClip hoverSound)
    {
        _audioSource = audioSource;
        _hoverSound = hoverSound;
    }

    private void PlayHoverSound(BaseEventData eventData)
    {
        _audioSource.PlayOneShot(_hoverSound);
    }

    public Button GetButtonComponent()
    {
        return _button;
    }
}
