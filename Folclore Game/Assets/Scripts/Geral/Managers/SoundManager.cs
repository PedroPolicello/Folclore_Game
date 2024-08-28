using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private int musicVolume;
    
    [SerializeField] private AudioSource sFXSource;
    [SerializeField] private int sFXVolume;

    [SerializeField] private AudioClip[] sFXClips;

    private void Awake()
    {
        Instance = this;
        
        musicSource.volume = musicVolume;
        sFXSource.volume = sFXVolume;
        
        musicSource.loop = true;
    }

    public void PlayPauseMusic(bool play)
    {
        if(play) musicSource.Play();
        else musicSource.Stop();
    }

    public void playSFX(int sfxIndex)
    {
        sFXSource.clip = sFXClips[sfxIndex];
        sFXSource.Play();
    }
}
