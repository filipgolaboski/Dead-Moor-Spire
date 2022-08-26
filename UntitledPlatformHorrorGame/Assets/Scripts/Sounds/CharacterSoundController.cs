using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSoundController : MonoBehaviour
{
    public Character character;
    public AudioSource audioSource;

    public AudioClip runningClip;
    public AudioClip dashingClip;
    public AudioClip jumpingClip;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!character.characterDeath)
        {
            if (character.characterSpeed > 0 && !character.characterJump && !character.characterDashing)
            {
                PlayRepeating(runningClip);
            }
            else
            {
                StopClip(runningClip);
            }

            if (character.characterJump)
            {
                PlayOneOff(jumpingClip);
            }

            if (character.characterDashing)
            {
                PlayOneOff(dashingClip);
            }
        }
        else 
        {
            StopClip(runningClip);
        }
    }

    public void PlayOneOff(AudioClip audioClip)
    {
        if (!audioSource.isPlaying)
        {
            audioSource.loop = false;
            audioSource.clip = audioClip;
            audioSource.Play();
        }

    }

    public void PlayRepeating(AudioClip audioClip)
    {
        if (!audioSource.isPlaying)
        {
            audioSource.loop = true;
            audioSource.clip = audioClip;
            audioSource.Play();
        }
    }

    public void StopClip(AudioClip audioClip)
    {
        if(audioSource.clip == audioClip)
        {
            audioSource.Stop();
        }
        
    }
}
