using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class LightFlicker : MonoBehaviour
{
    public new UnityEngine.Rendering.Universal.Light2D light;
    public float min = 2;
    public float max = 4;
    private float minDurationofFlicker = 0.1f;
    private float maxDurationofFlicker = 0.2f;
    private float counter;
    

    // Update is called once per frame

    void Start()
    {
        light = GetComponent<UnityEngine.Rendering.Universal.Light2D>();
        counter = Random.Range(minDurationofFlicker, maxDurationofFlicker);
    }
    void Update()
    {
        counter -= Time.deltaTime;

        if (counter <= 0)
        {
            counter = Random.Range(minDurationofFlicker, maxDurationofFlicker);
            light.intensity = Random.Range(min, max);
        } 
    }
}
