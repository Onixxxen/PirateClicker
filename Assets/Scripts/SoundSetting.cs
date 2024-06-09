using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSetting : MonoBehaviour
{
    [SerializeField] private List<AudioSource> _audioSources;
    [SerializeField] private Sprite _soundOnImage;
    [SerializeField] private Sprite _soundOffImage;

    private bool _soundIsActive = true;
    private float _previousSoundVolume;

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            UpdateSoundVolume();
            PauseSound();
        }
        else
        {
            BackSound();
        }
    }

    private void Awake()
    {
        UpdateSoundVolume();
    }

    public void ChangeSoundEnabled()
    {
        if (_soundIsActive)
        {
            foreach (AudioSource audioSource in _audioSources)
                audioSource.volume = 0;

            _soundIsActive = false;

            gameObject.GetComponent<Image>().sprite = _soundOffImage;

            UpdateSoundVolume();
        }
        else
        {
            foreach (AudioSource audioSource in _audioSources)
                audioSource.volume = 1;

            _soundIsActive = true;

            gameObject.GetComponent<Image>().sprite = _soundOnImage;

            UpdateSoundVolume();
        }
    }

    public void UpdateSoundVolume()
    {
        _previousSoundVolume = _audioSources[1].volume;
    }

    public void PauseSound()
    {
        foreach (AudioSource audioSource in _audioSources)
            audioSource.volume = 0;
    }

    public void BackSound()
    {
        foreach (AudioSource audioSource in _audioSources)
            audioSource.volume = _previousSoundVolume;
    }
}
