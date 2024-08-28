using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    
    [SerializeField] private AudioSource musicSource;
    [SerializeField] [Range(0f,1f)] private int musicVolume;
    [SerializeField] private AudioSource sFXSource;
    [SerializeField] [Range(0f,1f)] private int sFXVolume;

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
    }

    private void Update()
    {
        musicSource.volume = musicVolume;
        sFXSource.volume = sFXVolume;
    }

    public void PlayPauseMusic(bool play)
    {
        if(play) musicSource.Play();
        else musicSource.Stop();
    }

    public void PlaySFX(AudioClip clip )
    {
        sFXSource.PlayOneShot(clip);
    }
}
