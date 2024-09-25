using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private Slider musicVolume;
    [SerializeField] private AudioSource sFXSource;
    [SerializeField] private Slider sFXVolume;

    [Header("---- Player Clips ----")]
    public AudioClip backgroundMusic;
    public AudioClip walk;
    public AudioClip jump;
    public AudioClip attack;
    public AudioClip damage;
    public AudioClip death;
    
    [Header("---- Enemies Clips ----")]
    public AudioClip enemyAttack;
    public AudioClip enemyHit;
    public AudioClip enemyDeath;
    
    [Header("---- NPC Clips ----")]
    public AudioClip nPCInteract;
    public AudioClip questComplete;

    private void Awake()
    {
        Instance = this;
        musicSource.loop = true;
        musicVolume.value = musicVolume.maxValue / 2;
        sFXVolume.value = sFXVolume.maxValue / 2;
    }

    private void Update()
    {
        musicSource.volume = musicVolume.value/10;
        sFXSource.volume = sFXVolume.value/10;
    }

    public void PlayPauseMusic(bool play)
    {
        if(play) musicSource.Play();
        else musicSource.Stop();
    }

    public void PlaySFX(AudioClip clip)
    {
        sFXSource.PlayOneShot(clip);
    }
}
