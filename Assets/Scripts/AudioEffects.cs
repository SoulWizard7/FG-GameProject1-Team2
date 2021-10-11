using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEffects : MonoBehaviour
{
    private AudioSource _audioSource;
    public List<AudioClip> soundEffects;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayZombieDeath()
    {
        _audioSource.PlayOneShot(soundEffects[Random.Range(0, 4)]);
    }

    public void PlayPumpkinDeath()
    {
        _audioSource.PlayOneShot(soundEffects[5]);
    }
    
    
}
