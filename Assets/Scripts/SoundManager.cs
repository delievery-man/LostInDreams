using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    private static AudioClip _fireSound, _runSound, _enemySound, _doorSound, _keySound, _enemyDeath, _bossDeath;
    private static AudioSource _audioSource;
    
    private void Start()
    {
        _fireSound = Resources.Load<AudioClip>("fire 3");
        _runSound = Resources.Load<AudioClip>("run_sound_1");
        _enemySound = Resources.Load<AudioClip>("enemySound");
        _doorSound = Resources.Load<AudioClip>("doorSound");
        _keySound = Resources.Load<AudioClip>("key 1");
        _enemyDeath = Resources.Load<AudioClip>("sound");
        _bossDeath = Resources.Load<AudioClip>("bossDeath");
        _audioSource = GetComponent<AudioSource>();
    }

    public static void PlaySound(string clip)
    {
        switch (clip)   
        {
            case "fire":
            {
                _audioSource.PlayOneShot(_fireSound);
                break;
            }
            case "run":
            {
                _audioSource.PlayOneShot(_runSound);
                break;
            }
            case "ghost":
            {
                _audioSource.PlayOneShot(_enemySound);
                break;
            }
            case "door":
            {
                _audioSource.PlayOneShot(_doorSound);
                break;
            }
            case "key":
            {
                _audioSource.PlayOneShot(_keySound);
                break;
            }
            case "enemy":
            {
                _audioSource.PlayOneShot(_enemyDeath);
                break;
            }
            case "boss":
            {
                _audioSource.PlayOneShot(_bossDeath);
                break;
            }
        }
    }

    public static void StopSound()
    {
        _audioSource.Stop();
    }
    
}