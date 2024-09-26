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
    public Slider sFXVolume;

    [Header("---- Player Clips ----")]
    public AudioClip backgroundMusic; //OK
    public AudioClip bossMusic; //OK
    //public AudioClip walk;
    public AudioClip jump; //OK
    public AudioClip attack; //OK
    public AudioClip damage; //OK
    public AudioClip death; //OK
    
    [Header("---- Enemies Clips ----")]
    public AudioClip batAttack; //OK
    //public AudioClip batMove;
    public AudioClip dragonAttack; //OK
    //public AudioClip dragonMove;
    
    [Header("---- NPC Clips ----")]
    public AudioClip nPCInteract; //OK
    public AudioClip collect; //OK
    
    [Header("---- Boss Fight Clips ----")]
    public AudioClip fireBall; //OK
    public AudioClip spikes; //OK
    public AudioClip bossHit; //OK
    public AudioClip bossDie; //OK



    private void Awake()
    {
        Instance = this;
        musicSource.loop = true;
        SetMusic(backgroundMusic);
        PlayPauseMusic(true);
        musicVolume.value = musicVolume.maxValue / 2;
        sFXVolume.value = sFXVolume.maxValue / 2;
    }

    private void Update()
    {
        musicSource.volume = musicVolume.value/50;
        sFXSource.volume = sFXVolume.value/50;
    }
    
    public void SetMusic(AudioClip clip)
    {
        musicSource.clip = clip;
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
