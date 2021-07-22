using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVolumeManager : MonoBehaviour
{
    private AudioValueController[] audios;
    [Range(0f, 1f)] public float maxVolumeLevel;
    [Range(0f, 0.6f)]public float currentVolumeLevel;
    private void Start()
    {
        audios = FindObjectsOfType<AudioValueController>();
        ChangeGlobalAudioVolume();
    }

    private void FixedUpdate()
    {
        ChangeGlobalAudioVolume();
    }


    private void ChangeGlobalAudioVolume()
    {
        if (currentVolumeLevel >= maxVolumeLevel)
        {
            currentVolumeLevel = maxVolumeLevel;
        }

        foreach (AudioValueController avc in audios)
        {
            avc.SetAudioLevel(currentVolumeLevel);
        }
    }
}
