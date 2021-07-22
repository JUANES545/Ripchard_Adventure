using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioNewTrack : MonoBehaviour
{
    private AudioManager _audioManager;
    public int newTrackID;
    public bool playOnStart;

    void Start()
    {
        _audioManager = FindObjectOfType<AudioManager>();
        if (playOnStart)
        {
            _audioManager.PlayNewTrack(newTrackID);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            _audioManager.PlayNewTrack(newTrackID);
            gameObject.SetActive(false);
        }
    }
}