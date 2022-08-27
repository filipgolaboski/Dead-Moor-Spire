using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RandomParticleSystem : MonoBehaviour
{
    public float chance;
    public float interval;
    public ParticleSystem particleSystem;
    public UnityEvent onParticleSystemPlay;
    public UnityEvent onParticleSystemStop;

    float currentTime;
    // Start is called before the first frame update
    void Start()
    {
        currentTime = interval;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentTime < 0)
        {
            if(Random.Range(0f,1f) < chance)
            {
                if (particleSystem.isPlaying)
                {
                    particleSystem.Stop();
                    onParticleSystemStop.Invoke();
                }
                else
                {
                    particleSystem.Play();
                    onParticleSystemPlay.Invoke();
                }
            }
            currentTime = interval;
        }
        else
        {
            currentTime -= Time.deltaTime;
        }

    }
}
