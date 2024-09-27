using System;
using System.Collections;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    private AudioSource audioSource;
    
    private void OnEnable()
    {
        StartCoroutine(Sound());
    }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player")) PlayerHealth.Instance.TakeDamage(1);
    }

    IEnumerator Sound()
    {
        yield return new WaitForSeconds(1f);
        
        audioSource.volume = SoundManager.Instance.sFXVolume.value/50;
        audioSource.PlayOneShot(SoundManager.Instance.spikes);
    }
}
