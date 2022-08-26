using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomScaryNoises : MonoBehaviour
{
    public AudioClip[] scaryNoisesClips;
    public AudioSource[] audioSources;

    public float appeareanceChances;

    List<AudioClip> currentClips;

    private void Start()
    {
        currentClips = new List<AudioClip>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Random.Range(0f,1f) < appeareanceChances)
        {
            for(int i = 0; i < audioSources.Length; i++)
            {
                
                if (!audioSources[i].isPlaying)
                {
                    if(audioSources[i].clip != null)
                    {
                        currentClips.Remove(audioSources[i].clip);
                    }
                    AudioClip clip = scaryNoisesClips[Random.Range(0, scaryNoisesClips.Length)];
                    if (!currentClips.Contains(clip)) 
                    {
                        audioSources[i].clip = clip;
                        audioSources[i].Play();
                        currentClips.Add(clip);
                    }
                    
                }
            }
        }
    }
}
