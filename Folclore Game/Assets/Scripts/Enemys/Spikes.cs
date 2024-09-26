using System;
using System.Collections;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(Sound());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player")) PlayerHealth.Instance.TakeDamage(1);
    }

    IEnumerator Sound()
    {
        yield return new WaitForSeconds(1f);
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.volume = SoundManager.Instance.sFXVolume.value/10;
        audioSource.PlayOneShot(SoundManager.Instance.spikes);
        Destroy(audioSource);
    }
}
