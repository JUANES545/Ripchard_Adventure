using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioValueController : MonoBehaviour
{
    private AudioSource _audioSource;
    private float currentAudioLevel;
    [Range(0f, 1f)] public float defaultAudioLevel;
    void Start()
    {
        _audioSource = FindObjectOfType<AudioSource>();
    }

    public void SetAudioLevel(float newVolume)
    {
        if (_audioSource == null)
        {
            _audioSource = GetComponent<AudioSource>();
        }

        currentAudioLevel = defaultAudioLevel * newVolume;
        _audioSource.volume = currentAudioLevel;
    }
}
