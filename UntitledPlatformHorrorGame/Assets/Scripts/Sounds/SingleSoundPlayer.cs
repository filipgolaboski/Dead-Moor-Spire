using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleSoundPlayer : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip audioClip;

    public void PlaySingleClip()
    {
        audioSource.Stop();
        audioSource.loop = false;
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    public void PlayLooping()
    {
        audioSource.Stop();
        audioSource.loop = true;
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    public void StopAudioSource()
    {
        audioSource.Stop();
    }
}
