using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightningSystem : MonoBehaviour
{
    public ParticleSystem[] lightningParticleSystems;
    public Light2D[] enviornmentLights;
    float[] originalLightIntensity;
    public float lightIntensity;
    public float flickerWaitTime;
    
    [Range(0,1)]
    public float lightningChance;


    // Start is called before the first frame update
    void Start()
    {
        originalLightIntensity = new float[enviornmentLights.Length];
        for (int i = 0; i < enviornmentLights.Length; i++) 
        {
            originalLightIntensity[i] = enviornmentLights[i].intensity;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Random.Range(0f,1f) < lightningChance)
        {
            StrikeLightning();
            StartCoroutine(FlickerLights());
        }
    }

    void StrikeLightning()
    {
        for(int i = 0; i < lightningParticleSystems.Length; i++)
        {
            if (lightningParticleSystems[i].isPlaying)
            {
                lightningParticleSystems[i].Stop();
            }
            lightningParticleSystems[i].Play();
        }
    }

    IEnumerator FlickerLights()
    {
        for (int i = 0; i < enviornmentLights.Length; i++)
        {
            enviornmentLights[i].intensity = lightIntensity;
        }
        yield return new WaitForSeconds(flickerWaitTime);
        for (int i = 0; i < enviornmentLights.Length; i++)
        {
            enviornmentLights[i].intensity = originalLightIntensity[i];
        }
    }
}
