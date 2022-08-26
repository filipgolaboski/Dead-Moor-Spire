using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPlaylist : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] clips;

    // Update is called once per frame
    void Update()
    {
        if(!audioSource.isPlaying)
        {
            audioSource.Stop();
            audioSource.clip = clips[Random.Range(0, clips.Length)];
            audioSource.Play();
        }
    }
}
